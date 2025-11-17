public class GameLogic
{

    public state CurrentState;

    public GameLogic(state initialstate)
    {
       CurrentState = initialstate;
    }

    public bool TryMove(int dRow, int dCol)
    {
        var nextState = CurrentState.CreateNextState(dRow, dCol);
        if (nextState == null)
            return false;

        CurrentState = nextState;  
        return true;
    }


    public bool IsGameFinished()
    {
        return CurrentState.IsItGoal();
    }


    }
