class Disjstra
{
    public PriorityQueue<state, int> pq;

    public Dictionary<string, int> costs;
    public GameLogic Game { get; }

    public Disjstra(GameLogic game)
    {
        pq = new PriorityQueue<state, int>();
        costs = new Dictionary<string, int>();
        Game = game;
    }

    public void Search()
    {
        state startState = Game.CurrentState;
        startState.cost = 0;

        pq.Enqueue(startState, 0);

        while (pq.Count > 0)
        {
            var current = pq.Dequeue();

             
            string key = current.Player.row + "," + current.Player.col;

            if (costs.ContainsKey(key) && costs[key] <= current.cost)
                continue;

            costs[key] = current.cost;


            if (current.IsItGoal())
            {
                printPath(current);
                return;
            }


            foreach (var state in current.neighborCells)
            {

                int dRow = state.row - current.Player.row;
                int dCol = state.col - current.Player.col;

                var next = current.CreateNextState(dRow, dCol);

                if (next == null)
                    continue;

                string nextKey = next.Player.row + "," + next.Player.col;

                if (costs.ContainsKey(nextKey) && costs[nextKey] <= next.cost)
                    continue;

                 
                pq.Enqueue(next, next.cost);
            }
        }

        Console.WriteLine("Not Found");
    }





    private void printPath(state goalState)
    {
        List<state> path = new List<state>();
        state currentState = goalState;

        while (currentState != null)
        {
            path.Add(currentState);

            currentState = currentState.Parent;
        }

        path.Reverse();

        Console.WriteLine("\n==============\n");
      
        foreach (var item in path)
        {

            Game.CurrentState = item;
             Thread.Sleep(50);
            PrintGridStep();


        }
        Console.WriteLine();
        string pathGoal = "";

        foreach (var item in path)
        {
            pathGoal += item.ToString() + "=>";

        }


        pathGoal = pathGoal.Substring(0, pathGoal.Length - 2);

        Console.WriteLine($"\n\nPath :{pathGoal}");
        Console.WriteLine($"\nTotal Cost = {goalState.cost}");
    }


















     




    public void PrintGridStep()
    {
        var grid = Game.CurrentState.Grid;
        var player = Game.CurrentState.Player;

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

    }

}
