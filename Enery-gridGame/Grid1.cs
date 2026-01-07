public class Grid
{
    public int rows;
    public int columns;
    public Cells[,] cells;


    public Grid(int rows ,int columns)
    {
        this.rows = rows;
        this.columns = columns; 
        cells = new Cells[rows ,columns];

        initial();
    }

    public void initial()
    {

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0;  j < columns;  j++)
            {
                cells[i, j] = new Cells(enTypeCell.EmptyCell, i, j);
            }
        }
    }



    public void SetCell (int row , int column ,enTypeCell typeCell)
    {
        cells[row,column]=new Cells(typeCell,row,column);
    }

    public Grid Clone()
    {
        Grid newGrid = new Grid(rows, columns);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                newGrid.cells[i, j] = this.cells[i, j].Clone();
            }
        }
        return newGrid;
    }


}
