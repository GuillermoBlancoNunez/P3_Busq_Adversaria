// Guillermo Blanco Núñez & Fiz Garrido Escudero




/// <summary>
/// Represents a human player who enters moves via the console.
/// </summary>
public class Human_Player : Player
{
    private string label;

    /// <summary>
    /// Gets the numeric symbol assigned to the player.
/// </summary>
    public int Symbol { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Human_Player"/> class.
/// </summary>
/// <param name="label">Label to display in the console ("X" or "O").</param>
/// <param name="symbol">Numeric value identifying the player (1 or -1).</param>
    public Human_Player(string label, int symbol)
    {
        this.label = label;
        this.Symbol = symbol;
    }

    /// <summary>
    /// Prompts the user in the console to select a row and column for their move.
/// </summary>
/// <param name="board">The current board state.</param>
/// <returns>A valid <see cref="Move"/> chosen by the player.</returns>
    public Move GetMove(Board board)
    {
        while (true)
        {
            Console.Write($"\n{label}'s turn. Enter row and column (0–2) as row,col: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.Clear();
                Console.WriteLine(board);
                Console.WriteLine("Empty input. Please try again.");
                continue;
            }

            string[] parts = input.Split(',');
            if (parts.Length == 2
                && int.TryParse(parts[0], out int row)
                && int.TryParse(parts[1], out int col)
                && row >= 0 && row < 3 && col >= 0 && col < 3)
            {
                if (board.IsEmpty(row, col))
                {
                    return new Move(row, col);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(board);
                    Console.WriteLine("Cell occupied. Choose another.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine(board);
                Console.WriteLine("Invalid format. Use 'row,col' with values between 0 and 2.");
            }
        }
    }
}
