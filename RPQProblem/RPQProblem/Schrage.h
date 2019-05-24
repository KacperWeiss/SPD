#pragma once
#include "BaseAlgorithm.h"

#include "Fwd.h"

class Schrage : public BaseAlgorithm
{
public:
	Schrage() = default;
	~Schrage() = default;

	// Inherited via BaseAlgorithm
	virtual RPQTasks OrderRPQs(RPQTasks& rawTasks, int numberOfTasks) override;

	int CalculateCmax(const RPQTasks & data, const int numberOfTasks);

private:
	RPQTasks notReady, ready;
};

