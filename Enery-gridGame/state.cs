using Microsoft.VisualBasic;

public class state
{ 
    public Grid Grid;

    public Player Player;
     
    public List<Cells> neighborCells;
     
    public state Parent { set; get; }
    public int cost { set; get; }

    public state(Grid grid, Player player)
    {
        Grid = grid;
        Player = player;
        neighborCells = new List<Cells>();
        Accessible();
         
    }

    private bool IsAccessible(int row, int col)
    {
        if (row < 0 || col < 0 || row >= Grid.rows || col >= Grid.columns)
            return false;

        return Grid.cells[row, col].typeCell != enTypeCell.WallCell;
    }


    public void Accessible()
    {
        neighborCells.Clear();

        if (IsAccessible(Player.row, Player.col - 1))
            neighborCells.Add(Grid.cells[Player.row, Player.col - 1]);

        if (IsAccessible(Player.row, Player.col + 1))
            neighborCells.Add(Grid.cells[Player.row, Player.col + 1]);

        if (IsAccessible(Player.row - 1, Player.col))
            neighborCells.Add(Grid.cells[Player.row - 1, Player.col]);

        if (IsAccessible(Player.row + 1, Player.col))
            neighborCells.Add(Grid.cells[Player.row + 1, Player.col]);

         
    }

    
     
    public state CreateNextState(int dRow, int dCol)
    {

         
        int newRow = Player.row + dRow;
        int newCol = Player.col + dCol;

         
        if (newRow < 0 || newCol < 0 || newRow >= Grid.rows || newCol >= Grid.columns)
            return null;


        var nextCell = Grid.cells[newRow, newCol];

        if (nextCell.typeCell == enTypeCell.WallCell)
            return null;

       
        Grid newGrid = Grid.Clone();
        Player newPlayer = Player.Clone();

        
        newPlayer.row = newRow;
        newPlayer.col = newCol;
        newPlayer.TotalCost += nextCell.CellCost;

        if (newGrid.cells[newRow, newCol].typeCell == enTypeCell.EmptyCell)
            newGrid.cells[newRow, newCol].typeCell = enTypeCell.Visited;

         
        if (newGrid.cells[Player.row, Player.col].typeCell == enTypeCell.EmptyCell ||
            newGrid.cells[Player.row, Player.col].typeCell == enTypeCell.StartCell)
            newGrid.cells[Player.row, Player.col].typeCell = enTypeCell.Visited;

        var newState = new state(newGrid, newPlayer);

        newState.cost = this.cost + nextCell.CellCost;
         
        newState.Parent = this;

        return newState;
    }

    public bool IsItGoal()
    {
        return Grid.cells[Player.row, Player.col].typeCell == enTypeCell.GoalCell;
    }

     
    public override bool Equals(object obj)
    {

        state state = obj as state;
        if (state ==null)
            return false;

        return this.Player.Equals(state.Player);
    }

    public override int GetHashCode()
    {
        return Player.GetHashCode();
    }

    public override string ToString()
    {
        return $"[" + this.Player.row + "," + this.Player.col + "]";
    }

}
