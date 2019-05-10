#include "pch.h"
#include "TaskRPQ.h"


TaskRPQ::TaskRPQ(int ID, int R, int P, int Q)
{
	this->ID = ID;
	preparationTime = R;
	executionTime = P;
	deliveryTime = Q;
}
