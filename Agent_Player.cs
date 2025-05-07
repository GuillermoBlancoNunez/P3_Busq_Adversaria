public class Agent_Player : Player 
{
        private readonly int agentSymbol;
        private readonly int humanSymbol;

        public Agent_Player(int agentSymbol, int humanSymbol) 
        {
            this.agentSymbol = agentSymbol;
            this.humanSymbol = humanSymbol;
        }

        public Move GetMove(Board board) 
        {
            return MinimaxEngine.FindBestMove(board, agentSymbol, humanSymbol);
        }
    }