public class Agent_Player : Player
{
    private int agentSymbol;
    private int humanSymbol;

    public Agent_Player(int agentSymbol, int humanSymbol)
    {
        this.agentSymbol = agentSymbol;
        this.humanSymbol = humanSymbol;
    }

    public Move GetMove(Board board)
    {
        return MinimaxClass.FindBestMove(board, agentSymbol, humanSymbol);
    }
}