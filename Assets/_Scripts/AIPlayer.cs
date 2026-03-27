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
            int score = Minimax(board, searchDepth - 1, false);
            board.UndoMove(move);
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }
        return bestMove;
    }
    private int Minimax(Board board, int depth, bool isMaximizing)
    {
        int boardScore = Evaluator.EvaluateBoard(board);
        if (boardScore == 1000 || boardScore == -1000 || depth == 0)
            return boardScore;

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            foreach (int move in board.GetValidMoves())
            {
                board.PlacePiece(move, 1); //AI is player 1
                int eval = Minimax(board, depth - 1, false);
                board.UndoMove(move);
                maxEval = Mathf.Max(maxEval, eval);
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (int move in board.GetValidMoves())
            {
                board.PlacePiece(move, 2); //Player is player 2
                int eval = Minimax(board, depth - 1, true);
                board.UndoMove(move);
                minEval = Mathf.Min(minEval, eval);
            }
            return minEval;
        }
    }
}
