#pragma once
#include <vector>
#include <string>

#include "Fwd.h"

struct RPQ
{
	int r, p, q, ID, endTime;
};

class RPQSolver
{
public:
	RPQSolver() = default;
	~RPQSolver() = default;

	void InitializeWithFile(std::string fileName);

	RPQSolver* WithAlgorithm(BaseAlgorithm* AlgorithmToGetResultWith);
	RPQTasks GetOrderedRPQs();

private:
	int numberOfTasks;
	RPQTasks RPQTasksRaw;
	BaseAlgorithmUPtr selectedAlgorithm;
};

