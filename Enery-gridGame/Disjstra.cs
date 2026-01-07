using Enery_gridGame;
using System;
using System.Collections.Generic;
using System.Threading;

public class Disjstra :BaseReport
{
    public PriorityQueue<state, int> pq;
    public Dictionary<string, int> costs;
    
    public GameLogic Game { get; }

    public Disjstra(GameLogic game)
    {
        pq = new PriorityQueue<state, int>();
        costs = new Dictionary<string, int>();
        Game = new GameLogic(game);
    }

    public override void start()
    {
        state startState = Game.CurrentState;
        startState.cost = 0;

        pq.Enqueue(startState, 0);

        state goalState = null;

        while (pq.Count > 0)
        { 
            var current = pq.Dequeue();

             Game.CurrentState = current;

            Game.PrintGridStep(current.cost);
            Thread.Sleep(200);

            string key = current.Player.row + "," + current.Player.col;

             if (costs.ContainsKey(key) && costs[key] <= current.cost)
                continue;

            costs[key] = current.cost;

             if (current.IsItGoal())
            {
                goalState = current;
                break;
            }

             foreach (var cell in current.neighborCells)
            {
                int dRow = cell.row - current.Player.row;
                int dCol = cell.col - current.Player.col;

                var next = current.CreateNextState(dRow, dCol);

                if (next == null)
                    continue;
                allState++;
                string nextKey = next.Player.row + "," + next.Player.col;

                if (costs.ContainsKey(nextKey) && costs[nextKey] <= next.cost)
                    continue;
                 

                  pq.Enqueue(next, next.cost);
            }
        }

         if (goalState != null)
        {
            Count = Game.CountSteps(goalState);
            Console.WriteLine($"Steps = {Count}");
            Cost = goalState.cost;
            Path = Game.PrintPath(goalState);
            Console.WriteLine(Path);
        }
        else
        {
            Console.WriteLine("Not Found");
        }
    }

    
}
