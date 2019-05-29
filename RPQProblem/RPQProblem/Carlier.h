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

	

private:
	int DesignateA(RPQTasks optimalTaskOrder, int upperBound);
	int DesignateB(RPQTasks optimalTaskOrder, int A, int upprBound);
	int DesignateC(RPQTasks optimalTaskOrder, int upperBound);
	
	int getAppendedH(RPQTasks schrageOrdered, int &newR, int C, int &newP, int &newQ);
	int CalculatePreviousP(RPQTasks optimalTasksOrder, int begin, int end);
	int FindElementByID(RPQTasks schrageOrdered, int oldR_ID);
	
	RPQTasks CalculateEndTimes(RPQTasks& optimalTaskOrder);
	RPQTasks DoCarlier(RPQTasks& rawTasks, int numberOfTasks);

	SchrageUPtr schrage;
	SchragePmtnUPtr schragePmtn;

	int upperBound, lowerBound;
	int A = -1, B = -1, C = -1;
	int oldR, oldQ;

	RPQTasks optimalTaskOrder, schrageOrdered;
};

