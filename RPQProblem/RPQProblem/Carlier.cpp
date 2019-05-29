#include "pch.h"
#include "Carlier.h"

#include "Schrage.h"
#include "SchragePmtn.h"
#include "RPQSolver.h"

#include <algorithm>
#include <iostream>
//Google OR-tools mixed-integer programming////
Carlier::Carlier() 
	: upperBound(INT32_MAX)
	, schrage(std::make_unique<Schrage>())
	, schragePmtn(std::make_unique<SchragePmtn>())
{}

RPQTasks Carlier::OrderRPQs(RPQTasks& rawTasks, int numberOfTasks)
{
	DoCarlier(rawTasks, numberOfTasks);
	PrintResult(this->optimalTaskOrder);

	return this->optimalTaskOrder;
}

RPQTasks Carlier::DoCarlier(RPQTasks& rawTasks, int numberOfTasks)
{
	//do normal Schrage and calculate upper bound
	schrage.reset();
	schrage = std::make_unique<Schrage>();
	schrage->OrderRPQs(rawTasks, numberOfTasks);
	
	this->schrageOrdered = schrage->GetOrderedRPQs();
	CalculateEndTimes(schrageOrdered);
	
	//if upper bound is better, then we have new better permutation
	if (schrage->GetCmax() < this->upperBound)
	{
		this->cmax = this->upperBound = schrage->GetCmax();
		this->optimalTaskOrder = schrage->GetOrderedRPQs();
	}

	// find element C, which is going to be the more optimal node (if exists)
	int C = DesignateC(this->schrageOrdered, schrage->GetCmax());

	//if hasn't found better element 
	if (C == -1) { return this->optimalTaskOrder; } // if can't find better solutions, it cuts off node

	//calculate r(K), p(K), q(K) , K = { C+1..C+2..B }
	int newR = INT32_MAX, newP = 0, newQ = INT32_MAX;
	for (int i = this->C + 1; i <= this->A; i++)
	{
		newR = std::min(newR, this->schrageOrdered[i].r);
		newP += this->schrageOrdered[i].p;
		newQ = std::min(newQ, this->schrageOrdered[i].q);
	}

	//change R 
	int oldR = this->schrageOrdered[C].r;
	int oldR_ID = this->schrageOrdered[C].ID;
	this->schrageOrdered[C].r = std::max(this->schrageOrdered[C].r, newR + newP);


	// perform SchragePMTN with the changed R
	schragePmtn.reset();
	schragePmtn = std::make_unique<SchragePmtn>();
	schragePmtn->OrderRPQs(this->schrageOrdered, numberOfTasks);

	int H = newR + newP + newQ;												 // H(K) = r(K), p(K), q(K)
	int appendedH = getAppendedH(this->schrageOrdered, newR, C, newP, newQ); // appendeH = H( Ku{C} )

	this->lowerBound = schragePmtn->GetCmax();
	this->lowerBound = std::max( std::max(H, appendedH), this->lowerBound);

	// if satisfied, do recursive Carlier
	if ( (this->lowerBound < this->upperBound) && (oldR != this->schrageOrdered[C].r) )
	{
		this->optimalTaskOrder = DoCarlier(this->schrageOrdered, numberOfTasks);
	}
	
	//recreate R from before the change
	int indexR = FindElementByID(this->schrageOrdered, oldR_ID);
	this->schrageOrdered[indexR].r = oldR;

	//now change Q
	int oldQ = this->schrageOrdered[C].q;
	int oldQ_ID = this->schrageOrdered[C].ID;
	this->schrageOrdered[C].q = std::max(this->schrageOrdered[C].q, newQ + newP);
	
	//do ScragePMTN with the changed Q
	schragePmtn.reset();
	schragePmtn = std::make_unique<SchragePmtn>();
	schragePmtn->OrderRPQs(this->schrageOrdered, numberOfTasks);

	this->lowerBound = schragePmtn->GetCmax();
	this->lowerBound = std::max( std::max(H, appendedH), this->lowerBound);

	// if satisfied, do recursive Carlier
	if ( (this->lowerBound < this->upperBound) && (oldQ != this->schrageOrdered[C].q) )
	{
		this->optimalTaskOrder = DoCarlier(this->schrageOrdered, numberOfTasks);
	}

	//recreate Q from before the change
	int indexQ = FindElementByID(this->schrageOrdered, oldQ_ID);
	this->schrageOrdered[indexQ].q = oldQ;
	
	return this->optimalTaskOrder;
}

int Carlier::FindElementByID(RPQTasks optimalTaskOrder, int old_ID)
{
	for (int i = 0; i < optimalTaskOrder.size(); i++)
	{
		if (optimalTaskOrder[i].ID == old_ID)
		{
			return i;
		}
	}
}

int Carlier::getAppendedH(RPQTasks optimalTaskOrder, int &newR, int C, int &newP, int &newQ)
{
	int R = std::min(newR, optimalTaskOrder[C].r);
	int P = newP + optimalTaskOrder[C].p;
	int Q = std::min(newQ, optimalTaskOrder[C].q);

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

int Carlier::CalculatePreviousP(RPQTasks optimalTaskOrder, int begin, int end)
{
	int summedP = 0;
	for (int i = begin; i <= end; i++)
	{
		summedP += optimalTaskOrder[i].p;
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
