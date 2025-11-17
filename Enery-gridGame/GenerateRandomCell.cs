class GenerateRandomCell
{


    public int rows { set; get; }

    public int cols { set; get; }

    Grid grid { set; get; }

    static readonly Random Random = new Random();

    public GenerateRandomCell(int rows, int cols, Grid grid)
    {
        this.rows = rows;
        this.cols = cols;
        this.grid = grid;
    }


    public (int sRow ,int sCol) GenerateCells()
    {
        int Grid = rows * cols;

        int wallCell, energyCell;

         
         
            wallCell = Grid / 5;
            energyCell = Grid / 7;

        GenerateRandomCellsOfType(enTypeCell.WallCell, wallCell);
        GenerateRandomCellsOfType(enTypeCell.EnergyCell, energyCell);
        GenerateRandomCellsOfType(enTypeCell.GoalCell, 1);
        GenerateRandomCellsOfType(enTypeCell.StartCell, 1);


        void GenerateRandomCellsOfType(enTypeCell typeCell, int count)
        {
            while (count > 0)
            {
                 
                int randomRow =  Random.Next(0, rows);
                int randomCol = Random.Next(0, cols);

                if (grid.cells[randomRow, randomCol].typeCell == enTypeCell.EmptyCell)
                {

                    grid.SetCell(randomRow, randomCol, typeCell);
                    count--;

                }

            }

        }


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {

                if (grid.cells[i, j].typeCell == enTypeCell.StartCell)
                    return (i, j);
            }

        }

        return (0, 0);
    }


    

}



/*
        Console.Write("Enter Start position row col: ");

        Random r = new Random();
        int randomrowS = r.Next(0, rows - 1);
        int randomcolS = r.Next(0, cols - 1);
        grid.SetCell(randomcolS, randomcolS, enTypeCell.StartCell);


        *//*var startPos = Console.ReadLine().Split();
        int startRow = int.Parse(startPos[0]);
        int startCol = int.Parse(startPos[1]);
        grid.SetCell(startRow, startCol, enTypeCell.StartCell);
*//*

        Console.Write("Enter Goal position row col: ");

        while (true)
        {
            int randomrowG = r.Next(0, rows - 1);
            int randomcolG = r.Next(0, cols - 1);

            if (grid.cells[randomrowG, randomcolG].typeCell == enTypeCell.EmptyCell)
            {
                grid.SetCell(randomrowG, randomcolG, enTypeCell.GoalCell);
                break;
            }
        }


        *//*var goalPos = Console.ReadLine().Split();
        int goalRow = int.Parse(goalPos[0]);
        int goalCol = int.Parse(goalPos[1]);
        grid.SetCell(goalRow, goalCol, enTypeCell.GoalCell);
        *//*

        Console.Write("How many walls : ");




        int numberRandomWall = r.Next(2, 4);
        while (numberRandomWall >= 0)
        {
            int randomrowW = r.Next(0, rows - 1);
            int randomcolW = r.Next(0, cols - 1);

            if (grid.cells[randomrowW, randomcolW].typeCell == enTypeCell.EmptyCell)
            {
                grid.SetCell(randomrowW, randomcolW, enTypeCell.WallCell);
                numberRandomWall--;
            }
        }

        *//*int wallCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < wallCount; i++)
        {
            
            Console.Write($"Enter Wall {i + 1} position row col: ");
            
            var wallPos = Console.ReadLine().Split();
            int wRow = int.Parse(wallPos[0]);
        
            int wCol = int.Parse(wallPos[1]);
            grid.SetCell(wRow, wCol, enTypeCell.WallCell);
        }*//*



        Console.Write("How many Energy cells : ");


        int numberRandomEnergy = r.Next(2, 4);
        while (numberRandomEnergy >= 0)
        {
            int randomrowE = r.Next(0, rows - 1);
            int randomcolE = r.Next(0, cols - 1);

            if (grid.cells[randomrowE, randomcolE].typeCell == enTypeCell.EmptyCell)
            {
                grid.SetCell(randomrowE, randomcolE, enTypeCell.EnergyCell);
                numberRandomEnergy--;
            }
        }



        *//* int energyCount = int.Parse(Console.ReadLine());
         for (int i = 0; i < energyCount; i++) 
         {
             Console.Write($"Enter Energy Cell {i + 1} position row col : ");

             var pos = Console.ReadLine().Split();

             int eRow = int.Parse(pos[0]);
             int eCol = int.Parse(pos[1]);
             grid.SetCell(eRow, eCol, enTypeCell.EnergyCell);
         }*/