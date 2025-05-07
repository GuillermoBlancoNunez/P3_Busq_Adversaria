public class Game 
{
        private readonly Player playerX;
        private readonly Player playerO;
        private readonly Board board;
        private readonly bool waitBetweenTurns;

        public Game(Player playerX, Player playerO, bool waitBetweenTurns = false) 
        {
            this.playerX = playerX;
            this.playerO = playerO;
            this.waitBetweenTurns = waitBetweenTurns;
            board = new Board();
        }

        public void Run() 
        {
            int turn = 1;
            string label = "X";
            while (true) 
            {
                Console.Clear();
                Console.WriteLine(board);

                if (waitBetweenTurns) 
                {
                    Console.WriteLine($"\n\n{label}-agent's turn. Press any key to continue...");
                    Console.ReadKey();
                }

                Move move = (turn == 1) ? playerX.GetMove(board) : playerO.GetMove(board);
                board.ApplyMove(move, turn);

                if (board.IsWin(turn))
                {
                    Console.Clear();
                    Console.WriteLine(board);
                    Console.WriteLine($"\n{label}-player wins!");
                    break;
                }

                if (board.IsTie()) 
                {
                    Console.Clear();
                    Console.WriteLine(board);
                    Console.WriteLine("\nIt's a tie!");
                    break;
                }

                turn = -turn;
                label = (label == "X") ? "O" : "X";
            }
        }
    }