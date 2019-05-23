#pragma once
#include "BaseAlgorithm.h"

#include "Fwd.h"

class Schrage : public BaseAlgorithm
{
public:
	Schrage() = default;
	~Schrage() = default;

	// Inherited via BaseAlgorithm
	virtual RPQTasks OrderRPQs(RPQTasks rawTasks, int numberOfTasks) override;

private:
	void CalculateCmax(const RPQTasks & data, const int numberOfTasks);
	RPQTasks notReady, ready;

};

