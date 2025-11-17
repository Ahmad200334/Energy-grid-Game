using Microsoft.VisualBasic;

public class state
{ 
    public Grid Grid;

    public Player Player;

    public int[,] CanMoveIt;


    public state(Grid grid, Player player)
    {
        Grid = grid;
        Player = player;
        CanMoveIt = new int[4, 4];
    }

    public bool isAccesss(int row,int col)
    {
      return  Grid.cells[row, col].typeCell != enTypeCell.WallCell;
    }

    public void Accessible()
    {
        //if(isAccesss())
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


         
        return new state(newGrid, newPlayer);
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


}
