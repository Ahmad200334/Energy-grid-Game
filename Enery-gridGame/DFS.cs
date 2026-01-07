using Enery_gridGame;
using System;
using System.Collections.Generic;
using System.Threading;

public class DFS :BaseReport
{
    private GameLogic game;
    private bool[,] visited;

    public Stack<state> stack;
    public state Startstate;
    public int counter { set; get; } = 0;

    public DFS(GameLogic gameLogic)
    {
        game = new GameLogic(gameLogic);

        int rows = game.CurrentState.Grid.rows;
        int cols = game.CurrentState.Grid.columns;

        visited = new bool[rows, cols];
        stack = new Stack<state>();
        Startstate = game.CurrentState;
    }
     
         
    public override void start()
    {
        stack.Push(Startstate);
        visited[Startstate.Player.row, Startstate.Player.col] = true;

        state goalState = null;

        while (stack.Count > 0)
        {
            state current = stack.Pop();

            game.CurrentState = current;
            game.PrintGridStep(current.Player.TotalCost);
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
                allState++;
                nextState.Parent = current;

                visited[nr, nc] = true;
                stack.Push(nextState);
            }
        }

        if (goalState != null)
        {
            Count = game.CountSteps(goalState);
            Cost = goalState.cost;
            Console.WriteLine($"Steps = {Count}");

            Path = game.PrintPath(goalState);
            Console.WriteLine(Path);
        }
        else
        {
            Console.WriteLine("No path found!");
        }
    }
}
