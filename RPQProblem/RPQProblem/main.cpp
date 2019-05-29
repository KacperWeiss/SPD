#include "pch.h"
#include <iostream>
#include <string>
#include <memory>

#include "RPQSolver.h"
#include "Schrage.h"
#include "SchragePmtn.h"
#include "Carlier.h"

int main()
{
	std::vector<std::string> selectedFileName = { 
		"in50.txt",
		"in100.txt",
		"in200.txt",

		"data000.txt",
		"data001.txt",
		"data002.txt",
		"data003.txt",
		"data004.txt",
		"data005.txt",
		"data006.txt",
		"data007.txt",
		"data008.txt",
	};

	RPQSolverUPtr solver(new RPQSolver());
	
	for (int i = 0; i < selectedFileName.size(); i++)
	{
		std::cout << selectedFileName[i] << ":\n";
		
		solver->InitializeWithFile(selectedFileName[i]);
		solver->WithAlgorithm(new Carlier())->GetOrderedRPQs();
		
		std::cout << "\n\n";
	}
}
