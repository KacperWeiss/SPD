#include "pch.h"
#include "SchragePmtn.h"

#include <algorithm>
#include <memory>

#include "RPQSolver.h"

RPQTasks SchragePmtn::orderRPQs(RPQTasks rawTasks, int numberOfTasks)
{
	int t = 0;
	int cmax = 0;
	RPQ tempRPQ{ 0, 0, INT32_MAX };
	notReady = rawTasks;

	auto compareR = [](RPQ a, RPQ b) {if (a.r == b.r) return a.ID > b.ID; return a.r > b.r; };
	auto compareQ = [](RPQ a, RPQ b) {if (a.q == b.q) return a.ID > b.ID; return a.q < b.q; };

	std::make_heap(notReady.begin(), notReady.end(), compareR);
	while (!notReady.empty() or !ready.empty())
	{
		while (!notReady.empty() and notReady[0].r <= t)
		{
			RPQ a = notReady[0];
			ready.push_back(notReady[0]);
			std::push_heap(ready.begin(), ready.end(), compareQ);
			std::pop_heap(notReady.begin(), notReady.end(), compareR);
			notReady.pop_back();

			if (a.q > tempRPQ.q)
			{
				tempRPQ.p = t - a.r;
				t = a.r;
				if (tempRPQ.p > 0)
				{
					ready.push_back(tempRPQ);
					std::push_heap(ready.begin(), ready.end(), compareQ);
				}
			}
		}
		if (ready.empty())
			t = notReady[0].r;
		else
		{
			RPQ a = ready[0];
			ordered.push_back(ready[0]);
			std::pop_heap(ready.begin(), ready.end(), compareQ);
			ready.pop_back();
			tempRPQ = a;
			t = t + a.p;
			cmax = std::max(cmax, t + a.q);
		}
	}

	PrintResult(ordered, numberOfTasks);

	return ordered;
}

int SchragePmtn::CalculateCmax(const RPQTasks & data, const int numberOfTasks)
{
	int m = 0, c = 0;
	for (int i = 0; i < numberOfTasks; ++i)
	{
		m = std::max(m, data[i].r) + data[i].p;
		c = std::max(c, m + data[i].q);
	}
	return c;
}
