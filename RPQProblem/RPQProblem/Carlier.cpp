#include "pch.h"
#include "Carlier.h"

#include "Schrage.h"
#include "SchragePmtn.h"
#include "RPQSolver.h"

#include <algorithm>
#include <iostream>
//Google OR-tools mixed-integer programming////
Carlier::Carlier() : upperBound(INT32_MAX), schrage(std::make_unique<Schrage>()), schragePmtn(std::make_unique<SchragePmtn>())
{
}

RPQTasks Carlier::OrderRPQs(RPQTasks& rawTasks, int numberOfTasks)
{
	//do normal Schrage and calculate upper bound
	//SchrageUPtr schrage = std::make_unique<Schrage>();
	schrage.reset();
	schrage = std::make_unique<Schrage>();
	schrage->OrderRPQs(rawTasks, numberOfTasks);
	schrageOrdered = schrage->GetOrderedRPQs();
	CalculateEndTimes(schrageOrdered);
	
	//if upper bound is better, then we have new better permutation
	if (schrage->GetCmax() < upperBound)
	{
		cmax = upperBound = schrage->GetCmax();
		optimalTaskOrderSchrage = schrage->GetOrderedRPQs();
		//CalculateEndTimes(optimalTaskOrderSchrage);
	}
	// find element C, which is going to be the more optimal node (if exists)
	//int C = DesignateC(optimalTaskOrderSchrage, cmax);
	int C = DesignateC(schrageOrdered, schrage->GetCmax());

	//if hasn't found better element 
	if (C == -1) 
	{
		//cmax = schrage->GetCmax();
		return optimalTaskOrderSchrage; // if can't find better solutions, it cuts off node
	} 

	//calculate r(K), p(K), q(K) , K = { C+1..C+2..B }
	int newR = INT32_MAX, newP = 0, newQ = INT32_MAX;
	for (int i = this->C + 1; i <= this->A; i++)
	{
		newR = std::min(newR, optimalTaskOrderSchrage[i].r);
		newP += optimalTaskOrderSchrage[i].p;
		newQ = std::min(newQ, optimalTaskOrderSchrage[i].q);
	}

	//change R 
	int oldR = optimalTaskOrderSchrage[C].r;
	int oldR_ID = optimalTaskOrderSchrage[C].ID;
	optimalTaskOrderSchrage[C].r = std::max(optimalTaskOrderSchrage[C].r, newR + newP);


	// perform SchragePMTN with the changed R
	schragePmtn.reset();
	schragePmtn = std::make_unique<SchragePmtn>();
	schragePmtn->OrderRPQs(optimalTaskOrderSchrage, numberOfTasks);
	
	int H = newR + newP + newQ; // H(K) = r(K), p(K), q(K)
	int appendedH = getAppendedH(newR, C, newP, newQ); // appendeH = H( Ku{C} )

	lowerBound = schragePmtn->GetCmax();
	lowerBound = std::max( std::max(H, appendedH), lowerBound);

	// if satisfied, do recursive Carlier
	if ( (lowerBound < upperBound) && (oldR != optimalTaskOrderSchrage[C].r) )
	{
		optimalTaskOrderSchrage = OrderRPQs(optimalTaskOrderSchrage, numberOfTasks);
	}
	
	//recreate R from before the change
	//optimalTaskOrderSchrage[C].r = oldR;
	int indexR = FindElementByID(oldR_ID);
	optimalTaskOrderSchrage[indexR].r = oldR;

	//now change Q
	int oldQ = optimalTaskOrderSchrage[C].q;
	int oldQ_ID = optimalTaskOrderSchrage[C].ID;
	optimalTaskOrderSchrage[C].q = std::max(optimalTaskOrderSchrage[C].q, newQ + newP);
	
	//do ScragePMTN with the changed Q
	schragePmtn.reset();
	schragePmtn = std::make_unique<SchragePmtn>();
	schragePmtn->OrderRPQs(optimalTaskOrderSchrage, numberOfTasks);

	lowerBound = schragePmtn->GetCmax();
	lowerBound = std::max( std::max(H, appendedH), lowerBound);

	// if satisfied, do recursive Carlier
	if ( (lowerBound < upperBound) && (oldQ != optimalTaskOrderSchrage[C].q) )
	{
		optimalTaskOrderSchrage = OrderRPQs(optimalTaskOrderSchrage, numberOfTasks);
	}

	//recreate Q from before the change
	//optimalTaskOrderSchrage[C].q = oldQ;
	int indexQ = FindElementByID(oldQ_ID);
	optimalTaskOrderSchrage[indexQ].q = oldQ;
	
	//cmax = CalculateCmax(optimalTaskOrderSchrage, numberOfTasks);
	PrintResult(optimalTaskOrderSchrage);
	return optimalTaskOrderSchrage;
}

int Carlier::FindElementByID(int old_ID)
{
	for (int i = 0; i < this->optimalTaskOrderSchrage.size(); i++)
	{
		if (optimalTaskOrderSchrage[i].ID == old_ID)
		{
			return i;
		}
	}
}

int Carlier::getAppendedH(int &newR, int C, int &newP, int &newQ)
{
	int R = std::min(newR, optimalTaskOrderSchrage[C].r);
	int P = newP + optimalTaskOrderSchrage[C].p;
	int Q = std::min(newQ, optimalTaskOrderSchrage[C].q);

	return R + P + Q;
}

int Carlier::DesignateA(RPQTasks optimalTaskOrder, int upperBound)
{
	int tempID = -1;
	RPQTasks::iterator it;
	int i = 0;
	for (it = optimalTaskOrder.begin(); it != optimalTaskOrder.end(); it++, i++)
	{
		if ( (it->endTime + it->q) == upperBound)
		{
			tempID = i;
		}
	}

	return tempID;
}

int Carlier::DesignateB(RPQTasks optimalTaskOrder, int A, int upperBound)
{
	int tempID = INT32_MAX;
	for (int i = 0; i < optimalTaskOrder.size(); i++)
	{
		if ( (optimalTaskOrder[i].r + CalculatePreviousP(optimalTaskOrder, i, A) + optimalTaskOrder[A].q) == upperBound)
		{
			return i;
		}
	}

	return -1;
}

int Carlier::CalculatePreviousP(RPQTasks optimalTasksOrder, int begin, int end)
{
	int summedP = 0;
	for (int i = begin; i <= end; i++)
	{
		summedP += optimalTaskOrderSchrage[i].p;
	}
	return summedP;
}

int Carlier::DesignateC(RPQTasks optimalTaskOrder, int upperBound)
{
	this->A = DesignateA(optimalTaskOrder, upperBound);
	this->B = DesignateB(optimalTaskOrder, A, upperBound);
	
	int tempID = -1;
	for (int i = B; i <= A; i++)
	{
		if (optimalTaskOrder[i].q < optimalTaskOrder[A].q)
		{
			tempID = i;
		}
	}

	return this->C = tempID;
}

RPQTasks Carlier::CalculateEndTimes(RPQTasks& optimalTaskOrder)
{
	RPQTasks::iterator it;
	for (it = optimalTaskOrder.begin(); it != optimalTaskOrder.end(); it++)
	{
		if (it == optimalTaskOrder.begin())
		{
			it->endTime = it->r + it->p;
		}
		else
		{
			int startTime = std::max(it->r, (it - 1)->endTime);
			it->endTime = startTime + it->p;
		}
	}
	
	return RPQTasks();
}


int Carlier::CalculateCmax(const RPQTasks & data, const int numberOfTasks)
{
	{
		int m = 0, c = 0;
		for (int i = 0; i < numberOfTasks; ++i)
		{
			m = std::max(m, data[i].r) + data[i].p;
			c = std::max(c, m + data[i].q);
		}
		return cmax = c;
	}
}