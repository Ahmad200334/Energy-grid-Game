namespace Enery_gridGame
{
    public class Report
    {
        

        public BFS BFS { get; set; }
        public DFS DFS { get; set; }

        public Hill Hill { get; set; }

        public Disjstra Disjstra { get; set; }

        public GameLogic gameLogic { get; private set; }

        public AStar AStar { get; set; }

        public Report(BFS bFS, DFS dFS, Hill hill, Disjstra disjstra, AStar aStar, GameLogic gameLogic)
        {
            BFS = bFS;
            DFS = dFS;
            Hill = hill;
            Disjstra = disjstra;
            AStar = aStar;
            this.gameLogic = gameLogic;
        }

        private void PrintHeader()
        {
            Console.Clear();

            Console.WriteLine("\n\n\n");
            for (int i = 0; i < 11; i++)
            {
                Console.Write("--------");

            }
            Console.WriteLine();

            Console.Write("| Alogrithm Name     ");
            Console.Write("| Path                                                                                                             ");
            Console.Write("|Cost            ");
            Console.Write("|state count       ");
            Console.Write("|all state ");

            Console.WriteLine("\n");
            for (int i = 0; i < 11; i++)
            {
                Console.Write("--------");

            }
            Console.WriteLine("\n");
        }

        public void Print()
        {
            PrintHeader();
            if (BFS != null)
                PrintSpecific(BFS);
            if (DFS != null)
                PrintSpecific(DFS);
            if (Disjstra != null)
                PrintSpecific(Disjstra);
            if (Hill != null)
                PrintSpecific(Hill);
            if (AStar != null)
                PrintSpecific(AStar);

        }
        private void PrintSpecific(BaseReport algorithm)
        {


            Console.Write($"|{algorithm.GetType().Name}         ");
            Console.Write($"|{algorithm.Path}                                                                                                                      ");
            Console.Write($"|{algorithm.Cost}");
            Console.Write($"|{algorithm.Count}");
            Console.Write($"|{algorithm.allState}");
            

            Console.WriteLine("\n");
        }

    }
}
