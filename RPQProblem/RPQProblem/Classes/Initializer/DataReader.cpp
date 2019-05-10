#include "pch.h"
#include "DataReader.h"

#include <fstream>


TaskVector DataReader::GetDataFromFile(std::string fileName)
{
	std::fstream file;
	std::vector<TaskRPQ> tasks;

	file.open(fileName, std::ios::in);
	if (file.good())
	{
		GetDataFromFile(file, tasks);

		file.close();
	}

	return tasks;
}

void DataReader::GetDataFromFile(std::fstream &file, std::vector<TaskRPQ> &tasks)
{
	int nrOfTasks;
	int columns;
	file >> nrOfTasks;
	file >> columns;

	int r, p, q;
	for (int i = 0; i < nrOfTasks; i++)
	{
		file >> r >> p >> q;
		tasks.push_back(TaskRPQ(i, r, p, q));
	}
}
