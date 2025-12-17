class Disjstra
{
    public PriorityQueue<state, int> pq;

    public Dictionary<string, int> costs;
    public GameLogic Game { get; }

    public Disjstra(GameLogic game)
    {
        pq = new PriorityQueue<state, int>();
        costs = new Dictionary<string, int>();
        Game = game;
    }

    public void Search()
    {
        state startState = Game.CurrentState;
        startState.cost = 0;

        pq.Enqueue(startState, 0);

        while (pq.Count > 0)
        {
            var current = pq.Dequeue();

             
            string key = current.Player.row + "," + current.Player.col;

            if (costs.ContainsKey(key) && costs[key] <= current.cost)
                continue;

            costs[key] = current.cost;


            if (current.IsItGoal())
            {
                printPath(current);
                return;
            }


            foreach (var state in current.neighborCells)
            {

                int dRow = state.row - current.Player.row;
                int dCol = state.col - current.Player.col;

                var next = current.CreateNextState(dRow, dCol);

                if (next == null)
                    continue;

                string nextKey = next.Player.row + "," + next.Player.col;

                if (costs.ContainsKey(nextKey) && costs[nextKey] <= next.cost)
                    continue;

                 
                pq.Enqueue(next, next.cost);
            }
        }

        Console.WriteLine("Not Found");
    }

}
