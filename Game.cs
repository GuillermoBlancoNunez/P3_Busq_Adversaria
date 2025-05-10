// Guillermo Blanco Núñez & Fiz Garrido Escudero




/// <summary>
/// Manages the main game loop, alternating turns between two players.
/// </summary>
public class Game
{
    private Player playerX;
    private Player playerO;
    private Board board;
    private bool waitBetweenTurns;

    /// <summary>
    /// Initializes a new instance of the <see cref="Game"/> class.
/// </summary>
/// <param name="playerX">Player instance for 'X'.</param>
/// <param name="playerO">Player instance for 'O'.</param>
/// <param name="waitBetweenTurns">
/// If <c>true</c>, pauses after each move until a key is pressed.
/// </param>
    public Game(Player playerX, Player playerO, bool waitBetweenTurns = false)
    {
        this.playerX = playerX;
        this.playerO = playerO;
        this.waitBetweenTurns = waitBetweenTurns;
        board = new Board();
    }

    /// <summary>
    /// Runs the game until a win or a tie occurs.
/// </summary>
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
                Console.WriteLine($"\n\n{label}'s turn. Press any key to continue...");
                Console.ReadKey();
            }

            Move move = (turn == 1) ? playerX.GetMove(board) : playerO.GetMove(board);
            board.ApplyMove(move, turn);

            if (board.IsWin(turn))
            {
                Console.Clear();
                Console.WriteLine(board);
                Console.WriteLine($"\nPlayer {label} wins!");
                break;
            }

            if (board.IsTie())
            {
                Console.Clear();
                Console.WriteLine(board);
                Console.WriteLine("\nIt's a tie!");
                break;
            }

            // Switch turn
            turn = -turn;
            label = (label == "X") ? "O" : "X";
        }
    }
}
