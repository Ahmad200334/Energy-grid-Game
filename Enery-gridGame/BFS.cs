using Enery_gridGame;

public class BFS : BaseReport
{
    private GameLogic game;
    private bool[,] visited;

    public Queue<state> queue;
    public state Startstate;

    public BFS(GameLogic gameLogic)
    {
        game = new GameLogic(gameLogic);

        int rows = game.CurrentState.Grid.rows;
        int cols = game.CurrentState.Grid.columns;

        visited = new bool[rows, cols];
        queue = new Queue<state>();
        Startstate = game.CurrentState;
    }

    public override void start()
    {
        queue.Enqueue(Startstate);
        visited[Startstate.Player.row, Startstate.Player.col] = true;

        state goalState = null;

        while (queue.Count > 0)
        {
            state current = queue.Dequeue();


            game.CurrentState = current;
            game.PrintGridStep(current.Player.TotalCost);
            Thread.Sleep(200);


            if (current.IsItGoal())
            {
                goalState = current;
                break;
            }


            foreach (var nextCell in current.neighborCells)
            {
                int nr = nextCell.row;
                int nc = nextCell.col;


                if (visited[nr, nc])
                    continue;


                state nextState = current.CreateNextState(
                    nr - current.Player.row,
                    nc - current.Player.col
                );

                if (nextState == null)
                    continue;

                allState++;
                nextState.Parent = current;

                visited[nr, nc] = true;
                queue.Enqueue(nextState);
            }
        }


        if (goalState != null)
        {
            Count = game.CountSteps(goalState);
            Console.WriteLine($"Steps = {Count}");
            Cost = goalState.cost;
            Path = game.PrintPath(goalState);
            Console.WriteLine(Path); ;
        }
        else
        {
            Console.WriteLine("No path found!");
        }

    }





}
