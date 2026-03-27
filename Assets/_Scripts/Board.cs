using System.Collections.Generic;
using UnityEngine;

public class Board
{

    public const int width = 7, height = 6;
    public int[,] grid = new int[height, width];

    public bool IsColumnFull(int column)
    {
        return grid[0, column] != 0;
    }

    public int PlacePiece(int column, int player)
    {
        for (int row = height - 1; row >= 0; row--)
        {
            if (grid[row, column] == 0)
            {
                grid[row, column] = player;
                return row; //Return the row where the piece was placed
            }
        }
        return -1; //Column is full, and thus the move is invalid
    }

    public void UndoMove(int column)
    {
        for (int row = 0; row < height; row++)
        {
            if (grid[row, column] != 0)
            {
                grid[row, column] = 0;
                return;
            }
        }
    }

    public List<int> GetValidMoves()
    {
        List<int> validMoves = new List<int>();
        for (int col = 0; col < width; col++)
        {
            if (!IsColumnFull(col))
                validMoves.Add(col);
        }
        return validMoves;
    }

    public bool CheckWin(int player)
    {
        //Checks horizontal, vertical, and diagonal for a row of 4 pieces
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (grid[row, col] == player)
                {
                    //Check horizontal
                    if (col <= width - 4 && grid[row, col + 1] == player && grid[row, col + 2] == player && grid[row, col + 3] == player)
                        return true;
                    //Check vertical
                    if (row <= height - 4 && grid[row + 1, col] == player && grid[row + 2, col] == player && grid[row + 3, col] == player)
                        return true;
                    //Check diagonal (bottom-left to top-right)
                    if (row >= 3 && col <= width - 4 && grid[row - 1, col + 1] == player && grid[row - 2, col + 2] == player && grid[row - 3, col + 3] == player)
                        return true;
                    //Check diagonal (top-left to bottom-right)
                    if (row <= height - 4 && col <= width - 4 && grid[row + 1, col + 1] == player && grid[row + 2, col + 2] == player && grid[row + 3, col + 3] == player)
                        return true;
                }
            }
        }
        return false;
    }
}
