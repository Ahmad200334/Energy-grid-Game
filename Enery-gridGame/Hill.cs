using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enery_gridGame
{
    class Hill
    {
        public GameLogic logic { get; set; }
        public (int, int) Goal;
        
        public Hill(GameLogic game)
        {
            logic = game;
            Goal = GetGoal();
        }

    
            return Math.Abs (state.Player.row - Goal.Item1  ) +  Math.Abs(state.Player.col -Goal.Item2);   
        }



        public void Search()
        {
            state current = logic.CurrentState;
            current.cost = 0;

            while (true)
            {

                if (current.IsItGoal())
                {
                  PrintPath(current);
                    return;
                }


                state ?bestNeighbor = null;
                int bestValue = int.MaxValue;

                foreach (var neighbor in current.neighborCells)
                {
                    int row = neighbor.row - current.Player.row;
                    int col = neighbor.col - current.Player.col;

                    var next = current.CreateNextState(row, col);
                    
                    
                    if (next == null) 
                        continue;


                    if (value < bestValue)
                    {
                        bestValue = value;
                        bestNeighbor = next;
                    }
                }

                if (bestNeighbor == null)
                {
                    Console.WriteLine("Stopped");
                    PrintPath(current);
                    return;
                }

                bestNeighbor.Parent = current;
                current = bestNeighbor;
            }
        }




        public (int,int) GetGoal()
        {
            state current = logic.CurrentState;

            for (int r = 0; r < current.Grid.rows; r++)
                for (int c = 0; c < current.Grid.columns; c++)
                    if (current.Grid.cells[r, c].typeCell == enTypeCell.GoalCell)
                             return (r,c);

            return (0,0);
        }


























        private void PrintPath(state goal)
        {
            List<state> path = new List<state>();
            state curr = goal;

            while (curr != null)
            {
                path.Add(curr);
                curr = curr.Parent;
            }

            path.Reverse();

            Console.WriteLine("\n==============\n");

            foreach (var item in path)
            {

                logic.CurrentState = item;
                Console.Clear();
                PrintGridStep();
                Thread.Sleep(50);


            }
            Console.WriteLine();
            string pathGoal = "";

            foreach (var item in path)
            {
                pathGoal += item.ToString() + "=>";

            }


            pathGoal = pathGoal.Substring(0, pathGoal.Length - 2);

            Console.WriteLine($"\n\nPath :{pathGoal}");
            Console.WriteLine($"\nTotal Cost = {goal.cost}");
        }












        public void PrintGridStep()
        {
            var grid = logic.CurrentState.Grid;
            var player = logic.CurrentState.Player;

            for (int i = 0; i < grid.rows; i++)
            {
                for (int j = 0; j < grid.columns; j++)
                {
                    if (i == player.row && j == player.col)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("??     ");
                        Console.ResetColor();
                        continue;
                    }

                    switch (grid.cells[i, j].typeCell)
                    {
                        case enTypeCell.GoalCell:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("G     ");
                            break;
                        case enTypeCell.WallCell:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("WW   ");
                            break;
                        default:
                            Console.ResetColor();
                            Console.Write("░     ");
                            break;
                    }

                    Console.ResetColor();
                }
                Console.WriteLine("\n");
            }
        }
    }
}
