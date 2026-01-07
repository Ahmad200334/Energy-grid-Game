using System;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static void Main()
    {


        Console.WriteLine("Grid Energy Game\n\n");
        Console.Write("Enter number of rows: ");
        int rows = int.Parse(Console.ReadLine());

        Console.Write("Enter number of columns: ");
        int cols = int.Parse(Console.ReadLine());



        Grid grid = new Grid(rows, cols);


        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Set special cells:");



        var Random =new GenerateRandomCell(rows, cols,grid);
       //var ( srow, scol)=  Random.GenerateCells();
       var startPosition=  Random.GenerateCells();
         


        Player player = new Player(startPosition.sRow,startPosition.sCol);

        state initialState = new state(grid, player);


        GameLogic logic = new GameLogic(initialState);


        GameController controller = new GameController(logic);


        Console.WriteLine("\nPress enter to start the game :");
        Console.ReadLine();


        while (true)
        {
            Console.Clear();
            controller.PrintGrid();
            Console.WriteLine("\n\n\n");
            Console.WriteLine("If you want to edit Cells ? click (Y)");
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Enter the Position Cell (row col)");
                var parts = Console.ReadLine().Split();

                if (parts.Length != 2)
                {
                    Console.WriteLine("Invalid input format. Required: row col");
                    Console.ReadKey();
                    continue;
                }

                int r = int.Parse(parts[0]);
                int c = int.Parse(parts[1]);

                if (r < 0 || r >= rows || c < 0 || c >= cols)
                {
                    Console.WriteLine("Invalid cell position.");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("Enter the type Cell (EmptyCell, WallCell, EnergyCell, StartCell, GoalCell)");
                string type = Console.ReadLine();

                 if (!Enum.TryParse<enTypeCell>(type, true, out var cellType))
                {
                    Console.WriteLine("Invalid cell type.");
                    Console.ReadKey();
                    continue;
                }

                 grid.SetCell(r, c, cellType);

                Console.WriteLine("Cell updated!");
                Console.ReadKey();
            }
            else if(key.Key == ConsoleKey.N)
            {
                Console.Clear();
                break;
            }
        }

        controller.Start();



    }
}

 