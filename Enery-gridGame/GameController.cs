 using Enery_gridGame;

 
public class GameController
{
    public   GameLogic game;

    public GameController(GameLogic game)
    {
        this.game = game;
    }

    public void Start()
    {
        while (true)
        {
            PrintGrid();

            Console.WriteLine("Use arrows  to move:");
            var key= Console.ReadKey().Key;

            if (key == ConsoleKey.A)
            {

                Hill hill = new Hill(game);
                hill.Search();
                Console.WriteLine("\nfinished .");
                Console.ReadKey();
                continue;
            }
            if (key == ConsoleKey.B)
            {
                 
                BFS bfs = new BFS(game);
                bfs.Begin(); 
                Console.WriteLine("\nfinished .");
                Console.ReadKey();
                continue;
            }
            
            if (key == ConsoleKey.D)
            {
                 
                DFS dfs = new DFS(game);
                dfs.begin(); 
                Console.WriteLine("\nfinished .");
                Console.ReadKey();
                continue;
            }
            if (key == ConsoleKey.U)
            {
                 
                var dij = new Disjstra(game);
                dij.Search();
                Console.WriteLine("\nfinished .");
                Console.ReadKey();
                continue;
            }



            bool moved = key switch
            {
                ConsoleKey.UpArrow => game.TryMove(-1, 0),
                ConsoleKey.DownArrow => game.TryMove(1, 0),
                ConsoleKey.LeftArrow => game.TryMove(0, -1),
                ConsoleKey.RightArrow => game.TryMove(0, 1),
                _ => false
            };

            if (!moved)
            {
                Console.WriteLine("Invalid move   !");
                continue;
            }

            if (game.IsGameFinished())
            {
                PrintGrid();
                Console.WriteLine("Goal reached! Total Cost: " + game.CurrentState.Player.TotalCost);
                break;
            }
        }
    }



    public void PrintGrid()
    {
        Console.Clear();
         
        var grid = game.CurrentState.Grid;
        var player = game.CurrentState.Player;

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

        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($" Total Cost: {player.TotalCost}");
    }

}

 