#pragma once
#include "BaseAlgorithm.h"
#include "Fwd.h"

class Carlier : public BaseAlgorithm
{
public:
	Carlier();
	~Carlier() = default;

	// Inherited via BaseAlgorithm
	virtual RPQTasks OrderRPQs(RPQTasks rawTasks, int numberOfTasks) override;

private:
	RPQTaskUPtr DesignateA();
	RPQTaskUPtr DesignateB();
	RPQTaskUPtr DesignateC();

	int upperBound;
	RPQTasks optimalTaskOrder;
};

