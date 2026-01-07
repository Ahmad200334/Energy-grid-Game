using System.Security.Cryptography.X509Certificates;

public class GameLogic
{

    public state CurrentState;

    public GameLogic(state initialstate)
    {
       CurrentState = initialstate;
         
    }

    public GameLogic(GameLogic logic) :this(logic.CurrentState)
    { 
     
    }

    public bool TryMove(int dRow, int dCol)
    {
        var nextState = CurrentState.CreateNextState(dRow, dCol);
        if (nextState == null)
            return false;

        CurrentState = nextState;  
        return true;
    }

    public string PrintPath(state goal)
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

        string fullPath = "";

        for (int i = 0; i < path.Count; i++)
        {
            var p = path[i];
            if (i == path.Count - 1)
                fullPath += ($"[{p.r},{p.c}]");
            else
                fullPath +=($"[{p.r},{p.c}] => ");
        }

        fullPath += "\n";
        return fullPath;
    }


    public void PrintGridStep(int totalCost=0)
    {
        Console.Clear();
        var grid = this.CurrentState.Grid;
        var player = this.CurrentState.Player;

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

                var cellType = grid.cells[i, j].typeCell;

                switch (cellType)
                {
                    case enTypeCell.GoalCell:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("G     ");
                        break;
                    case enTypeCell.WallCell:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("WW   ");
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

        Console.WriteLine($"\nTotal Cost: {totalCost}");
    }

    public int CountSteps(state goalState)
    {
        int steps = 0;
        state current = goalState;

        while (current.Parent != null)
        {
            steps++;
            current = current.Parent;
        }

        return steps;
    }

    public bool IsGameFinished()
    {
        return CurrentState.IsItGoal();
    }


 }
