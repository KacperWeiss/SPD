#pragma once
#include <vector>

#include "Fwd.h"

class BaseAlgorithm
{
public:
	BaseAlgorithm() = default;
	~BaseAlgorithm() = default;

	virtual RPQTasks OrderRPQs(RPQTasks rawTasks, int numberOfTasks) = 0;
	int GetCmax();

protected:
	void PrintResult(RPQTasks& ordered);

	int cmax;
};

