#include "pch.h"
#include "BaseAlgorithm.h"

#include <iostream>
#include <algorithm>

#include "RPQSolver.h"

void BaseAlgorithm::PrintResult(RPQTasks& ordered, const int numberOfTasks)
{
	std::cout << "Order of tasks:\n";
	for (auto task : ordered)
		std::cout << task.ID + 1 << " ";
	std::cout << std::endl;

	std::cout << "Cmax:\n";
	std::cout << CalculateCmax(ordered, numberOfTasks) << std::endl;
}
