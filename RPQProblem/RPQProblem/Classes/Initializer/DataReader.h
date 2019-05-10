#pragma once
#include <string>
#include <vector>
#include "../TaskRPQ.h"

using TaskVector = std::vector<TaskRPQ>;

class DataReader
{
public:
	DataReader() = default;
	TaskVector GetDataFromFile(std::string fileName);

private:
	void GetDataFromFile(std::fstream &file, std::vector<TaskRPQ> &Tasks);
};

