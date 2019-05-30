#include "pch.h"
#include <iostream>
#include <string>
#include <memory>

#include <chrono>

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
		"test000.txt",

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
		auto start = std::chrono::system_clock::now();
		
		std::cout << selectedFileName[i] << ":\n";
		solver->InitializeWithFile(selectedFileName[i]);
		solver->WithAlgorithm(new Carlier())->GetOrderedRPQs();
		std::cout << "\n";

		auto end = std::chrono::system_clock::now();

		std::chrono::duration<double> elapsed_seconds = end - start;
		std::cout << "Elapsed Time: " << elapsed_seconds.count() << "\n\n\n";
	}
}
