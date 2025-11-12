public class Player
{

    public int row;
    public int col;
    public int TotalCost;


    public Player(int row ,int col)
    {
        this.row= row;
        this.col= col;
    }


    public void MoveTo(Cells cell)
    {
        if(cell.typeCell != enTypeCell.WallCell)
        {
            row =cell.row;
            col = cell.col;
            TotalCost += cell.CellCost;

        }
        if (cell.typeCell == enTypeCell.EmptyCell || cell.typeCell == enTypeCell.EnergyCell || cell.typeCell == enTypeCell.StartCell)
        {
            cell.typeCell = enTypeCell.Visited;
        }
    }
    public Player Clone()
    {
        return new Player(this.row, this.col)
        {
            TotalCost = this.TotalCost
        };
    }
    public override bool Equals(object obj)
    {
        if (obj is not Player other)
            return false;
        return this.row == other.row && this.col == other.col;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(row, col);
    }
}
