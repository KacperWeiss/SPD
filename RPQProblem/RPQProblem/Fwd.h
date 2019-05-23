#pragma once
#include <vector>
#include <memory>

struct RPQ;
using RPQTasks = std::vector<RPQ>;
using RPQTaskUPtr = std::unique_ptr<RPQ>;

class RPQSolver;
using RPQSolverUPtr = std::unique_ptr<RPQSolver>;

class BaseAlgorithm;
using BaseAlgorithmUPtr = std::unique_ptr<BaseAlgorithm>;

class Schrage;
using SchrageUPtr = std::unique_ptr<Schrage>;

class SchragePmtn;
using SchragePmtnUPtr = std::unique_ptr<SchragePmtn>;
