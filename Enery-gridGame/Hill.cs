using System;
using System.Threading;

namespace Enery_gridGame
{
  public  class Hill :BaseReport
    {
        public GameLogic logic { get; set; }
        public (int, int) Goal;

        public Hill(GameLogic game)
        {
            logic = new GameLogic(game);
            Goal = GetGoal();
        }


        public int Heuristic(state state)
        {
            return Math.Abs(state.Player.row - Goal.Item1)
                 + Math.Abs(state.Player.col - Goal.Item2);
        }

        public override void start()
        {
            state current = logic.CurrentState;
            current.cost = 0;

            while (true)
            {
               
                logic.CurrentState = current;
                logic.PrintGridStep(current.cost);
                Thread.Sleep(200); 
                 

                if (current.IsItGoal())
                {
                    Count = logic.CountSteps(current);
                    Console.WriteLine($"Steps = {Count}");
                    Cost = current.cost;
                    Path = logic.PrintPath(current);
                    Console.WriteLine(Path);
                    return;
                }

                state bestNeighbor = null;
                int bestValue = int.MaxValue;
                int currentValue = Heuristic(current);
                 

                foreach (var neighbor in current.neighborCells)
                {
                    int dRow = neighbor.row - current.Player.row;
                    int dCol = neighbor.col - current.Player.col;

                    var next = current.CreateNextState(dRow, dCol);
                    if (next == null)
                        continue;
                    allState++;
                    int value = Heuristic(next);

                    if (value < bestValue)
                    {
                        bestValue = value;
                        bestNeighbor = next;
                    }
                }
                 
                if (bestNeighbor == null || bestValue >= currentValue)
                {
                    Console.WriteLine("Stop.");
                    Path = logic.PrintPath(current);
                    Console.WriteLine(Path);
                    return;
                }

                 bestNeighbor.Parent = current;
                current = bestNeighbor;
            }
        }
         

        public (int, int) GetGoal()
        {
            state current = logic.CurrentState;

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
