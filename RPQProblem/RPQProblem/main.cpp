#include "pch.h"
#include <iostream>
#include <string>

#include "RPQSolver.h"
#include "Schrage.h"
#include "SchragePmtn.h"
#include "Carlier.h"

int main()
{
	std::string selectedFileName = "data001.txt";
	RPQSolverUPtr solver(new RPQSolver());

	solver->InitializeWithFile(selectedFileName);
	solver->WithAlgorithm(new Carlier())->GetOrderedRPQs();

}
