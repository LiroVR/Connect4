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
            return ScoreBoard(board); //AI looks for best score
    }
    private static int ScoreBoard(Board board)
    {
        int score = 0;
        int centerCount = 0;
        for (int row = 0; row < Board.height; row++) //Checks the center for pieces, as the center in Connect 4 is important to control
        {
            if (board.grid[row, Board.width / 2] == 1)
                centerCount++;
        }
        score += centerCount * 3; //Makes the center column more valuable to the AI than edge columns
        return score;
    }
}
