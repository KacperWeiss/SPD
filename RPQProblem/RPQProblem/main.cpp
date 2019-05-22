#include "pch.h"
#include <iostream>
#include <string>

#include "RPQSolver.h"
#include "Schrage.h"
#include "SchragePmtn.h"

int main()
{
	std::string selectedFileName = "in50.txt";
	RPQSolverUPtr solver(new RPQSolver());

	solver->InitializeWithFile(selectedFileName);
	solver->WithAlgorithm(new Schrage())->GetOrderedRPQs();
}
