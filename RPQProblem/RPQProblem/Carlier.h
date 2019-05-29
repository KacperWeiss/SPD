#pragma once
#include "BaseAlgorithm.h"
#include "Fwd.h"

class Carlier : public BaseAlgorithm
{
public:
	Carlier();
	~Carlier() = default;

	// Inherited via BaseAlgorithm
	virtual RPQTasks OrderRPQs(RPQTasks& rawTasks, int numberOfTasks) override;

	int FindElementByID(int oldR_ID);

	

private:
	int DesignateA(RPQTasks optimalTaskOrder, int upperBound);
	int DesignateB(RPQTasks optimalTaskOrder, int A, int upprBound);
	int DesignateC(RPQTasks optimalTaskOrder, int upperBound);
	
	int getAppendedH(int &newR, int C, int &newP, int &newQ);
	int CalculatePreviousP(RPQTasks optimalTasksOrder, int begin, int end);
	int CalculateCmax(const RPQTasks& data, const int numberOfTasks);

	RPQTasks CalculateEndTimes(RPQTasks& optimalTaskOrder);
	
	SchrageUPtr schrage;
	SchragePmtnUPtr schragePmtn;

	int upperBound, lowerBound;
	
	int A = -1, B = -1, C = -1;
	RPQTasks optimalTaskOrderSchrage, schrageOrdered;
};

