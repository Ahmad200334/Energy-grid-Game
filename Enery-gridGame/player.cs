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

     

    public Player Clone()
    {
        return new Player(this.row, this.col)
        {
            TotalCost = this.TotalCost
        };
    }
    public override bool Equals(object obj)
    {
        Player player = obj as Player;

        if (player == null)
            return false;

        return this.row == player.row && this.col == player.col;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(row, col);
    }
}
