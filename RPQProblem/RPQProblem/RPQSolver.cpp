#include "pch.h"
#include "RPQSolver.h"

#include <fstream>

#include "BaseAlgorithm.h"

RPQSolver::RPQSolver(const RPQSolver &)
{
}

void RPQSolver::InitializeWithFile(std::string fileName)
{
	std::fstream file(fileName);
	int numberOfColumns, r, p, q;
	
	file >> numberOfTasks >> numberOfColumns;
	for (int i = 0; i < numberOfTasks; ++i)
	{
		file >> r >> p >> q;
		RPQTasksRaw.push_back(RPQ{r, p, q, i});
	}
}

RPQSolver* RPQSolver::WithAlgorithm(BaseAlgorithm* AlgorithmToGetResultWith)
{
	selectedAlgorithm.reset(AlgorithmToGetResultWith);
	return this;
}

RPQTasks RPQSolver::GetOrderedRPQs()
{
	return selectedAlgorithm->orderRPQs(RPQTasksRaw, numberOfTasks);
}
