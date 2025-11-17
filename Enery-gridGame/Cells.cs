public class Cells
{
    public enTypeCell typeCell;

    public int CellCost;

    public int row;

    public int col;


    public Cells(enTypeCell typeCell, int row, int col)
    {
        this.typeCell = typeCell;
        this.row = row;
        this.col = col;
         
        if (typeCell == enTypeCell.StartCell)
        {
            CellCost = 0;
        }
        else if (typeCell == enTypeCell.GoalCell)
        {
            CellCost = 1;
        }
        else if (typeCell == enTypeCell.EmptyCell)
        {
            CellCost = 1;

        }
        else if (typeCell == enTypeCell.EnergyCell)
        {
                CellCost = 5;
        }
        else if(typeCell == enTypeCell.WallCell)
        { 
            CellCost = -1;
        }


    }



    public Cells Clone()
    {
        return new Cells(this.typeCell, this.row, this.col)
        {
            CellCost = this.CellCost
        };
    }

}
