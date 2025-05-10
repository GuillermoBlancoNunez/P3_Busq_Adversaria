// Guillermo Blanco Núñez & Fiz Garrido Escudero




/// <summary>
/// Entry point for the Tic-Tac-Toe application.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that displays the menu and starts games.
/// </summary>
/// <param name="args">Command-line arguments (not used).</param>
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("\n\nChoose an option: 1. Player vs Player, 2. Player vs Agent, 3. Agent vs Agent: ");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    new Game(
                        new Human_Player("X", 1),
                        new Human_Player("O", -1)
                    ).Run();
                    break;
                case "2":
                    new Game(
                        new Human_Player("X", 1),
                        new Agent_Player(-1, 1)
                    ).Run();
                    break;
                case "3":
                    new Game(
                        new Agent_Player(1, -1),
                        new Agent_Player(-1, 1),
                        waitBetweenTurns: true
                    ).Run();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option. Please choose 1, 2, or 3.");
                    break;
            }
        }
    }
}
