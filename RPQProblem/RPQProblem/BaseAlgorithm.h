#pragma once
#include <vector>

#include "Fwd.h"

class BaseAlgorithm
{
public:
	BaseAlgorithm() = default;
	~BaseAlgorithm() = default;

	virtual RPQTasks orderRPQs(RPQTasks rawTasks, int numberOfTasks) = 0;
	virtual int CalculateCmax(const RPQTasks& data, const int numberOfTasks) = 0;

protected:
	void PrintResult(RPQTasks& ordered, const int numberOfTasks);
};

