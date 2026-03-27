using UnityEngine;

public class Evaluator
{
    public static int EvaluateBoard(Board board)
    {
        if (board.CheckWin(1))
            return 1000; //AI wins :(
        else if (board.CheckWin(2))
            return -1000; //Player wins :)
        else
            return 0; //Nobody wins :P
    }
    private static int ScoreBoard(Board board)
    {
        int score = 0;
        int centerCount = 0;
        for (int row = 0; row < Board.height; row++)
        {
            if (board.grid[row, Board.width / 2] == 1)
                centerCount++;
        }
        score += centerCount * 3; //Center column is more valuable to the AI than edge columns
        return score;
    }
}
