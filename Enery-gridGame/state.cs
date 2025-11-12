using Microsoft.VisualBasic;

public class state
{
     

    public Grid Grid;

   public Player Player;

    public state(Grid grid, Player player)
    {
        Grid = grid;
        Player = player;
        
    }
     

    public bool IsItGoal()
    {
      return  Grid.cells[Player.row, Player.col].typeCell == enTypeCell.GoalCell;
    }

    // دالة إنشاء حالة جديدة بعد الحركة
    public state CreateNextState(int dRow, int dCol)
    {
        int newRow = Player.row + dRow;
        int newCol = Player.col + dCol;

        // التحقق من حدود الشبكة
        if (newRow < 0 || newCol < 0 || newRow >= Grid.rows || newCol >= Grid.columns)
            return null;

        var nextCell = Grid.cells[newRow, newCol];
        if (nextCell.typeCell == enTypeCell.WallCell)
            return null;

        // نسخ الشبكة واللاعب
        Grid newGrid = Grid.Clone();
        Player newPlayer = Player.Clone();

        // نقل اللاعب
        newPlayer.row = newRow;
        newPlayer.col = newCol;
        newPlayer.TotalCost += nextCell.CellCost;

        // إرجاع حالة جديدة
        return new state(newGrid, newPlayer);
    }

    public override bool Equals(object obj)
    {
        if (obj is not state other)
            return false;
        return this.Player.Equals(other.Player);
    }

    public override int GetHashCode()
    {
        return Player.GetHashCode();
    }
}
