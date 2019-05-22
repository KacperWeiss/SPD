#include "pch.h"
#include "Schrage.h"

#include <algorithm>
#include <memory>

#include "RPQSolver.h"

RPQTasks Schrage::orderRPQs(RPQTasks rawTasks, int numberOfTasks)
{
	int t = 0;
	notReady = rawTasks;

	auto compareR = [](RPQ a, RPQ b) {if (a.r == b.r) return a.ID > b.ID; return a.r > b.r; };
	auto compareQ = [](RPQ a, RPQ b) {if (a.q == b.q) return a.ID > b.ID; return a.q < b.q; };

	std::make_heap(notReady.begin(), notReady.end(), compareR);
	while (!notReady.empty() or !ready.empty())
	{
		while (!notReady.empty() and notReady[0].r <= t)
		{
			ready.push_back(notReady[0]);
			std::push_heap(ready.begin(), ready.end(), compareQ);
			std::pop_heap(notReady.begin(), notReady.end(), compareR);
			notReady.pop_back();
		}
		if (ready.empty())
			t = notReady[0].r;
		else
		{
			ordered.push_back(ready[0]);
			std::pop_heap(ready.begin(), ready.end(), compareQ);
			ready.pop_back();
			t = t + ordered.back().p;
		}
	}

	PrintResult(ordered, numberOfTasks);

	return ordered;
}

int Schrage::CalculateCmax(const RPQTasks& data, const int numberOfTasks)
{
	int m = 0, c = 0;
	for (int i = 0; i < numberOfTasks; ++i)
	{
		m = std::max(m, data[i].r) + data[i].p;
		c = std::max(c, m + data[i].q);
	}
	return c;
}
