using UnityEngine;

public class AIPlayer
{
    public int searchDepth = 6;
    public int GetBestMove(Board board)
    {
        int bestScore = int.MinValue;
        int bestMove = -1;
        foreach (int move in board.GetValidMoves())
        {
            board.PlacePiece(move, 1); //AI is player 1
            int score = Minimax(board, searchDepth - 1, int.MinValue, int.MaxValue, false);
            board.UndoMove(move);
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }
        return bestMove;
    }
    private int Minimax(Board board, int depth, int alpha, int beta, bool isMaximizing)
    {
        int boardScore = Evaluator.EvaluateBoard(board); //Gets the score for the current board state

        if (boardScore == 1000 || boardScore == -1000 || depth == 0) //Returns the score if the game is over, or if search depth was reached
            return boardScore;

        if (isMaximizing)
        {
            int maxEval = int.MinValue;

            foreach (int move in board.GetValidMoves())
            {
                board.PlacePiece(move, 1); //AI is player 1
                int eval = Minimax(board, depth - 1, alpha, beta, false); //Recursively evaluates the board after making the move, and switches to minimizing player
                board.UndoMove(move); //Undoes the move, so the AI can check a different move

                maxEval = Mathf.Max(maxEval, eval); //Updates the maximum evaluation score found so far
                alpha = Mathf.Max(alpha, eval); //Updates alpha to the best score found so far

                if (beta <= alpha) //Alpha-beta pruning for optimization
                    break; //Stops checking branches if the move is worse than a previous move
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue; //If not maximizing, then it's the player's turn, so it minimizes the score for the AI

            foreach (int move in board.GetValidMoves())
            {
                board.PlacePiece(move, 2); //Human is player 2
                int eval = Minimax(board, depth - 1, alpha, beta, true);
                board.UndoMove(move);

                minEval = Mathf.Min(minEval, eval); //Updates the minimum evaluation found for the player's move
                beta = Mathf.Min(beta, eval);

                if (beta <= alpha)
                    break; 
            }
            return minEval;
        }
 
    }
}

