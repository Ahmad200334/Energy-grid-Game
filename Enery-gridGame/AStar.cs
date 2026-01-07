using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enery_gridGame
{
    public class AStar : BaseReport
    {

        public PriorityQueue<state, int> pq;
        public Dictionary<string, int> costs;

        public GameLogic Game { get; }

        public (int, int) Goal;


        public AStar(GameLogic game)
        {
            pq = new PriorityQueue<state, int>();
            costs = new Dictionary<string, int>();
            Game = new GameLogic(game);
            Goal = GetGoal();

        }


        public int Heuristic(state state)
        {
            return Math.Abs(state.Player.row - Goal.Item1)
                 + Math.Abs(state.Player.col - Goal.Item2);
        }

        public override void start()
        {
            state startState = Game.CurrentState;
            startState.cost = 0;

            pq.Enqueue(startState, startState.cost + Heuristic(startState));

            state goalState = null;

            while (pq.Count > 0)
            {
                var current = pq.Dequeue();

                Game.CurrentState = current;

                Game.PrintGridStep(current.cost);
                Thread.Sleep(200);


                if (current.IsItGoal())
                {
                    goalState = current;
                    break;
                }

                 
                string key = current.Player.row + "," + current.Player.col;

                if (costs.ContainsKey(key) && costs[key] <= current.cost)
                    continue;

                costs[key] = current.cost;
                 
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


                    pq.Enqueue(next, next.cost + Heuristic(current));
                    
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


        public (int, int) GetGoal()
        {
            state current = Game.CurrentState;

            for (int r = 0; r < current.Grid.rows; r++)
            {
                for (int c = 0; c < current.Grid.columns; c++)
                {
                    if (current.Grid.cells[r, c].typeCell == enTypeCell.GoalCell)
                        return (r, c);
                }
            }

            return (0, 0);
        }

    }
}
