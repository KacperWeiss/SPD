#pragma once
class TaskRPQ
{
public:
	TaskRPQ(int ID, int R, int P, int Q);
	~TaskRPQ() = default;

private:
	int ID;
	int preparationTime;	// R
	int executionTime;		// P
	int deliveryTime;		// Q
};

