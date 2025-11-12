 

public class GameController
{
    private readonly GameLogic game;

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
            var key = Console.ReadKey(true).Key;

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
                Console.WriteLine("🚫 Invalid move!");
                continue;
            }

            if (game.IsGameFinished())
            {
                PrintGrid();
                Console.WriteLine("🎯 Goal reached! Total Cost: " + game.CurrentState.Player.TotalCost);
                break;
            }
        }
    }



    private void PrintGrid()
    {
        Console.Clear();
        Console.OutputEncoding = System.Text.Encoding.UTF8; // ضروري لعرض الرموز بشكل صحيح

        var grid = game.CurrentState.Grid;
        var player = game.CurrentState.Player;

        for (int r = 0; r < grid.rows; r++)
        {
            for (int c = 0; c < grid.columns; c++)
            {
                // موقع الروبوت
                if (r == player.row && c == player.col)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("🤖     "); // مسافات أكثر (5) للمحاذاة
                    Console.ResetColor();
                    continue;
                }

                var cellType = grid.cells[r, c].typeCell;
                switch (cellType)
                {
                    case enTypeCell.StartCell:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("S     "); // مسافات إضافية
                        break;

                    case enTypeCell.GoalCell:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("G     ");
                        break;

                    case enTypeCell.WallCell:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("███   "); // شكل مربع أعرض للفصل البصري
                        break;

                    case enTypeCell.Visited:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("V     "); // رمز الزيارة
                        break;

                    case enTypeCell.EnergyCell:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("E     ");
                        break;

                    default:
                        Console.ResetColor();
                        Console.Write("░     "); // استبدال النقطة برمز فراغ جميل
                        break;
                }

                Console.ResetColor();
            }
            Console.WriteLine("\n"); // سطر فارغ بين الصفوف لمظهر شبكي أجمل
        }

        Console.ResetColor();
        Console.WriteLine($"\n🔋 Total Cost: {player.TotalCost}");
    }


     
}

 


