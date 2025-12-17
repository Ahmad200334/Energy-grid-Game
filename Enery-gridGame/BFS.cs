


namespace Enery_gridGame
{
    public class BFS
    {

        public Queue<state> Queue;
        public GameLogic Logic;
        public bool[,] visited;
        public state startState;
        public state goalState;

        public BFS(GameLogic logic)
        {
            Logic = logic;
            Queue = new Queue<state>();
            visited = new bool[logic.CurrentState.Grid.rows, logic.CurrentState.Grid.columns];
            startState = logic.CurrentState;

        }

        public void Begin()
        {
            Queue.Enqueue(startState);
            visited[startState.Player.row, startState.Player.col] = true;

            if (startState.IsItGoal())
            {
                return;
            }

            while (Queue.Count > 0)
            {
                state state = Queue.Dequeue();

                PrintGridStep(state.Player.TotalCost);
                Thread.Sleep(500);

                if (state.IsItGoal())
                {
                    return;
                }

                foreach (var child in state.neighborCells)
                {
                    int Rowschild = child.row;
                    int Colschild = child.col;


                    if (visited[Rowschild, Colschild])
                    {
                        continue;
                    }

                    var nextState = state.CreateNextState(Rowschild - state.Player.row, Colschild - state.Player.col);

                    if (nextState == null)
                    {
                        continue;
                    }

                    visited[Rowschild, Colschild] = true;
                     
                    Queue.Enqueue(nextState);
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

}

 









 
