#pragma once
#include <vector>
#include <string>

#include "Fwd.h"

struct RPQ
{
	int r, p, q, ID;
};

class RPQSolver
{
public:
	RPQSolver() = default;
	RPQSolver(const RPQSolver&);
	~RPQSolver() = default;

	void InitializeWithFile(std::string fileName);

	void WithAlgorithm(BaseAlgorithm* AlgorithmToGetResultWith);
	RPQTasks GetOrderedRPQs();

private:
	int numberOfTasks;
	RPQTasks RPQTasksRaw;
	BaseAlgorithmUPtr selectedAlgorithm;
};

