using Microsoft.VisualBasic;
using System;

class Program
{
    static void Main()
    {
        //Console.OutputEncoding = System.Text.Encoding.UTF8; // لدعم الرموز مثل 🤖

        Console.WriteLine("🔋 Grid Energy Game");
        Console.Write("Enter number of rows: ");
        int rows = int.Parse(Console.ReadLine());

        Console.Write("Enter number of columns: ");
        int cols = int.Parse(Console.ReadLine());

        // إنشاء الشبكة
        Grid grid = new Grid(rows, cols);

        // --- تحديد الخلايا الخاصة ---
        Console.WriteLine("\nSet special cells:");

        // 🟩 Start
        Console.Write("Enter Start position (row col): ");
        var startPos = Console.ReadLine().Split();
        int startRow = int.Parse(startPos[0]);
        int startCol = int.Parse(startPos[1]);
        grid.SetCell(startRow, startCol, enTypeCell.StartCell);

        // 🟥 Goal
        Console.Write("Enter Goal position (row col): ");
        var goalPos = Console.ReadLine().Split();
        int goalRow = int.Parse(goalPos[0]);
        int goalCol = int.Parse(goalPos[1]);
        grid.SetCell(goalRow, goalCol, enTypeCell.GoalCell);

        // 🧱 Walls
        Console.Write("How many walls? ");
        int wallCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < wallCount; i++)
        {
            Console.Write($"Enter Wall #{i + 1} position (row col): ");
            var wallPos = Console.ReadLine().Split();
            int wRow = int.Parse(wallPos[0]);
            int wCol = int.Parse(wallPos[1]);
            grid.SetCell(wRow, wCol, enTypeCell.WallCell);
        }

        // ⚡ Energy Cells
        Console.Write("How many Energy cells? ");
        int energyCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < energyCount; i++)
        {
            Console.Write($"Enter Energy Cell #{i + 1} position (row col): ");
            var ePos = Console.ReadLine().Split();
            int eRow = int.Parse(ePos[0]);
            int eCol = int.Parse(ePos[1]);
            grid.SetCell(eRow, eCol, enTypeCell.EnergyCell);
        }

        // --- إنشاء اللاعب ---
        Player player = new Player(startRow, startCol);

        // --- الحالة الأولى ---
        state initialState = new state(grid, player);

        // --- منطق اللعبة ---
        GameLogic logic = new GameLogic(initialState);

        // --- وحدة التحكم ---
        GameController controller = new GameController(logic);

        // --- بدء اللعبة ---
        Console.WriteLine("\nPress ENTER to start the game!");
        Console.ReadLine();

        controller.Start();  
    }
}
