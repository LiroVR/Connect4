using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Board board = new Board();
    AIPlayer aiPlayer = new AIPlayer();
    int currentPlayer = 1; //1 for AI, 2 for Player
    public GameObject redPiecePrefab, yellowPiecePrefab, loseText, winText, resetButton;

    public Vector3 boardOrigin = new Vector3(0,0,0);
    public Transform boardTransform;
    public float cellSize = 1.0f; //Each cell is 1 metre wide, to make things simple
    private bool gameOver = false;

    public Vector3 GetWorldPosition(int row, int column)
    {
        int flippedRow = Board.height - 1 - row; //Flip the row index, so that row 0 is at the bottom of the board and row 5 is at the top
        return boardOrigin + new Vector3(column * cellSize, flippedRow * cellSize, 0);
    }

    void SpawnPiece(int row, int column, int player)
    {
        GameObject piecePrefab = player == 1 ? redPiecePrefab : yellowPiecePrefab;
        Vector3 targetPos = GetWorldPosition(row, column);
        Debug.Log("Target row: " + row + ", column: " + column + ", target position: " + targetPos); //Debug log to check target position
        Vector3 spawnPos = targetPos + new Vector3(0.5f, 5f, 0); //Spawn the piece 5 units above the target position
        GameObject piece = Instantiate(piecePrefab, spawnPos, Quaternion.Euler(90, 0, 0));
        PieceFall fallScript = piece.AddComponent<PieceFall>();
        fallScript.targetY = targetPos.y;
        //Debug.Log("Player clicked at coordinates: " + Mouse.current.position.ReadValue()); //Debug log to check mouse position
        //Debug.Log($"Spawned piece for player {player} at column {column}, target Y: {targetPos.y}"); //Debug log to check the target Y value
    }

    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void Start()
    {
        var renderer = boardTransform.GetComponentInChildren<MeshRenderer>();
        Debug.Log("Board bounds center: " + renderer.bounds.center);
        Debug.Log("Board bounds size: " + renderer.bounds.size);
    }



    void Update()
    {
        if (currentPlayer == 1)
        {
            int aiMove = aiPlayer.GetBestMove(board);
            int row = board.PlacePiece(aiMove, 1);
            SpawnPiece(row, aiMove, 1);
            if (board.CheckWin(1) && !gameOver)
            {
                Debug.Log("AI wins!");
                loseText.SetActive(true);
                resetButton.SetActive(true);
                //enabled = false; //Stop the game, as the AI won :(
                gameOver = true;
            }
            currentPlayer = 2;
        }
        else if (Mouse.current.leftButton.wasPressedThisFrame) //Player's turn
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Debug.Log("Mouse world position: " + mousePos); //Debug log to check mouse world position
            int column = Mathf.FloorToInt((mousePos.x - boardOrigin.x) / cellSize); //Convert mouse X position to column index
            if (column >= 0 && column < Board.width && !board.IsColumnFull(column))
            {
                int row = board.PlacePiece(column, 2);
                SpawnPiece(row, column, 2);
                if (board.CheckWin(2) && !gameOver)
                {
                    Debug.Log("Player wins!");
                    winText.SetActive(true);
                    resetButton.SetActive(true);
                    //enabled = false; //Stop the game, as the player won :)
                    gameOver = true;
                }
                currentPlayer = 1;
            }
        }   
    }
}
