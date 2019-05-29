#include "pch.h"
#include "BaseAlgorithm.h"

#include <iostream>
#include <algorithm>

#include "RPQSolver.h"

void BaseAlgorithm::PrintResult(RPQTasks& ordered)
{
	std::cout << "Order of tasks:\n";
	for (auto task : ordered)
		std::cout << task.ID << " ";
	std::cout << std::endl;

	std::cout << "Cmax:\n";
	std::cout << GetCmax() << std::endl;
}

int BaseAlgorithm::GetCmax()
{
	return cmax;
}

RPQTasks BaseAlgorithm::GetOrderedRPQs()
{
	return ordered;
}