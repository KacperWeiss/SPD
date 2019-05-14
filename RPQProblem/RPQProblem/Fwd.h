#pragma once
#include <vector>
#include <memory>

struct RPQ;
using RPQTasks = std::vector<RPQ>;

class RPQSolver;
using RPQSolverUPtr = std::unique_ptr<RPQSolver>;

class BaseAlgorithm;
using BaseAlgorithmUPtr = std::unique_ptr<BaseAlgorithm>;
