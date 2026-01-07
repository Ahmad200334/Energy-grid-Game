using System;
using System.Collections.Generic;
using System.Threading;

public class D
{
    private GameLogic game;
    private bool[,] visited;

    public PriorityQueue<state, int> queue;
    public state Startstate;

    public D(GameLogic gameLogic)
    {
        game = gameLogic;

        int rows = game.CurrentState.Grid.rows;
        int cols = game.CurrentState.Grid.columns;

        visited = new bool[rows, cols];
        queue = new PriorityQueue<state, int>();
        Startstate = game.CurrentState;
    }

    public void SearchInD()
    {
        queue.Enqueue(Startstate, 0);
        visited[Startstate.Player.row, Startstate.Player.col] = true;

        state goalState = null;

        while (queue.Count > 0)
        {
            state current = queue.Dequeue();


            game.CurrentState = current;
            printGrid(current.Player.TotalCost);
            Thread.Sleep(200);


            if (current.IsItGoal())
            {
                goalState = current;
                break;
            }


            foreach (var nextCell in current.neighborCells)
            {
                int nr = nextCell.row;
                int nc = nextCell.col;


                if (visited[nr, nc])
                    continue;


                state nextState = current.CreateNextState(
                    nr - current.Player.row,
                    nc - current.Player.col
                );

                if (nextState == null)
                    continue;


                nextState.Parent = current;

                visited[nr, nc] = true;
                queue.Enqueue(nextState,nextState.cost);
            }
        }


        if (goalState != null)
        {
            PrintPath(goalState);
        }
        else
        {
            Console.WriteLine("No path found!");
        }
    }


    private void PrintPath(state goal)
    {
        List<(int r, int c)> path = new List<(int r, int c)>();

        state current = goal;

        while (current != null)
        {
            path.Add((current.Player.row, current.Player.col));
            current = current.Parent;
        }

        path.Reverse();

        Console.WriteLine("\n\nPath:");

        for (int i = 0; i < path.Count; i++)
        {
            var p = path[i];
            if (i == path.Count - 1)
                Console.Write($"[{p.r},{p.c}]");
            else
                Console.Write($"[{p.r},{p.c}] => ");
        }

        Console.WriteLine("\n");
    }


    private void printGrid(int cost)
    {
        var grid = game.CurrentState.Grid;
        var player = game.CurrentState.Player;

        Console.Clear();

        for (int i = 0; i < grid.rows; i++)
        {
            for (int j = 0; j < grid.columns; j++)
            {
                if (i == player.row && j == player.col)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("P     ");
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
                        Console.Write("WW    ");
                        break;
                    case enTypeCell.Visited:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("V     ");
                        break;
                    case enTypeCell.EnergyCell:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("E     ");
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

        Console.WriteLine($"Current Cost: {cost}");
    }
}
