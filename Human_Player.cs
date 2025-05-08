public class Human_Player : Player
{
    private string label;
    public int Symbol { get; }

    public Human_Player(string label, int symbol)
    {
        this.label = label;
        this.Symbol = symbol;
    }

    public Move GetMove(Board board)
    {
        while (true)
        {
            Console.Write($"\n{label}-player's turn. Enter row and column (0-2) -> row,col: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.Clear();
                Console.WriteLine(board.ToString());
                Console.WriteLine("Invalid input.");
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
                    Console.WriteLine(board.ToString());
                    Console.WriteLine("Cell already occupied.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine(board.ToString());
                Console.WriteLine("Invalid input. Use format 'row,col' with values 0 to 2.");
            }
        }
    }
}