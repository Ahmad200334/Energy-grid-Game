public class DFS
{

    public Stack<state> stack;
    public GameLogic Logic;
    public bool[,] visited;
    public state startState;
    public state goalState;

    public DFS(GameLogic logic)
    {
        Logic = logic;
        stack = new Stack<state>();
        visited = new bool[logic.CurrentState.Grid.rows, logic.CurrentState.Grid.columns];
        startState = logic.CurrentState;

    }

    public void begin()
    {



        stack.Push(startState);
        visited[startState.Player.row, startState.Player.col] = true;

        state goalState = null;

        while (stack.Count > 0)
        {
            state current = stack.Pop();
            PrintGridStep(current.Player.TotalCost);
            Thread.Sleep(500);


            if (current.IsItGoal())
            {
                goalState = current;
                break;
            }


            foreach (var child in current.neighborCells)
            {
                int rowsChild = child.row;
                int ColChild = child.col;

                if (visited[rowsChild, ColChild])
                    continue;

                state nextState = current.CreateNextState(rowsChild - current.Player.row, ColChild - current.Player.col);
                if (nextState == null)
                    continue;

                stack.Push(nextState);
                visited[rowsChild, ColChild] = true;


            }
        }



        stack.Push(startState);
        visited[startState.Player.row, startState.Player.col] = true;


        if (startState.IsItGoal())
        {
            return;
        }

        while (stack.Count > 0)
        {
            state state = stack.Peek();

            foreach (var child in state.neighborCells)
            {

                int rowsChild = child.row;
                int colsChild = child.col;

                if (visited[rowsChild, colsChild])
                {
                    continue;
                }

                state nextState = state.CreateNextState(rowsChild - rowsChild, colsChild - colsChild);


                if (nextState == null)
                {
                    continue;
                }

                stack.Push(state);
                visited[rowsChild, colsChild] = true;

                state = nextState;

            }

        }

    }




























    public void PrintGridStep(int totalCost)
    {
        var grid = Logic.CurrentState.Grid;
        var player = Logic.CurrentState.Player;

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

}












 

























