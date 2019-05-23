#include "pch.h"
#include "Carlier.h"

#include "Schrage.h"
#include "SchragePmtn.h"
#include "RPQSolver.h"

Carlier::Carlier() : upperBound(INT32_MAX)
{
}

RPQTasks Carlier::OrderRPQs(RPQTasks rawTasks, int numberOfTasks)
{
	SchrageUPtr schrage = std::make_unique<Schrage>(new Schrage());
	schrage->OrderRPQs(rawTasks, numberOfTasks);

	if (schrage->GetCmax() < upperBound)
	{
		upperBound = schrage->GetCmax();
		optimalTaskOrder = schrage->GetOrderedRPQs();
	}
	RPQTaskUPtr c = DesignateC();
}

RPQTaskUPtr Carlier::DesignateA()
{
	return RPQTaskUPtr();
}

RPQTaskUPtr Carlier::DesignateB()
{
	return RPQTaskUPtr();
}

RPQTaskUPtr Carlier::DesignateC()
{
	return RPQTaskUPtr();
}
