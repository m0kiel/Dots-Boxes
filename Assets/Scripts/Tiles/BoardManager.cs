using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : Singleton<BoardManager>
{
    #region EVENTS
    public static event EventHandler<ChangeLineStateEventArgs> ChangeLineState;

    public class ChangeLineStateEventArgs : EventArgs { public SquareLine line; public bool state; }

    public static void InvokeChangeLineState(GameObject sender, SquareLine newLine, bool newState)
    {
        ChangeLineState?.Invoke(sender, new ChangeLineStateEventArgs { line = newLine, state = newState });
    }
    private void Events_ChangeLineState(object sender, ChangeLineStateEventArgs e)
    {
        
    }
    #endregion

    [SerializeField] List<GameObject> tilePrefabs = new();

    List<List<GameObject>> board = new();

    float tileZoom = 3.5f;

    void Start()
    {
        
    }

    public void InitBoard()
    {
        ChangeTileScale();
        CreateBoard();
        LinkBoard();
        MoveCameraToCenterBoard();
        ResetPrefabScale();
    }    

    public void DeleteBoard()
    {
        Destroy(board[0][0].transform.parent.gameObject);
        board.Clear();
    }

    public void CreateBoard()
    {
        Vector2 boardSize = GameManager.Instance.BoardSize;
        float squareSize = 1.2f;
        float squareScale = tilePrefabs[0].transform.localScale.x;

        GameObject grid = GameObject.Find("Grid");
        if (grid == null) { grid = new GameObject("Grid"); }

        grid.transform.position = Vector3.zero;

        for (int y = 0; y < boardSize.y; y++)
        {
            List<GameObject> boardTiles = new();

            for (int x = 0; x < boardSize.x; x++)
            {
                GameObject tile;

                if (x == 0 && y == 0) // Bottom Left Corner Tile
                {
                    tile = Instantiate(tilePrefabs[0], new Vector3(x * squareSize * squareScale, y * squareSize * squareScale, 0), Quaternion.identity, grid.transform);
                }
                else if (y == 0) // Bottom Row Tiles
                {
                    tile = Instantiate(tilePrefabs[1], new Vector3(x * squareSize * squareScale, y * squareSize * squareScale, 0), Quaternion.identity, grid.transform);
                }
                else if (x == 0) // Left Column Tiles
                {
                    tile = Instantiate(tilePrefabs[2], new Vector3(x * squareSize * squareScale, y * squareSize * squareScale, 0), Quaternion.identity, grid.transform);
                }
                else // Default Tiles
                {
                    tile = Instantiate(tilePrefabs[3], new Vector3(x * squareSize * squareScale, y * squareSize * squareScale, 0), Quaternion.identity, grid.transform);
                }
                tile.name = "X: " + x + " Y: " + y;

                boardTiles.Add(tile);
            }
            board.Add(boardTiles);
        }
    }

    public void LinkBoard()
    {
        for (int y = 0; y < board.Count; y++)
        {
            for (int x = 0; x < board[0].Count; x++)
            {
                SquareLine[] lines = board[y][x].GetComponentsInChildren<SquareLine>();
                SquareTile tile = board[y][x].GetComponentInChildren<SquareTile>();

                for (int i = 0; i < lines.Length; i++) // Own Links
                {
                    tile.AddSquareLine(lines[i].GetSquareLineSide(), lines[i]);
                    lines[i].AddSquareTile(tile);
                }

                if (x == 0 && y == 0) { continue; } // First Pass

                // Remaining Links
                if (y == 0) // Bottom Row Tiles
                {
                    tile.AddSquareLine(SquareLineSide.LEFT, board[y][x - 1].GetComponentInChildren<SquareTile>().GetSquareLine(SquareLineSide.RIGHT));
                    tile.GetSquareLine(SquareLineSide.LEFT).AddSquareTile(tile);
                }
                else if (x == 0) // Left Column Tiles
                {
                    tile.AddSquareLine(SquareLineSide.BOTTOM, board[y - 1][x].GetComponentInChildren<SquareTile>().GetSquareLine(SquareLineSide.TOP));
                    tile.GetSquareLine(SquareLineSide.BOTTOM).AddSquareTile(tile);
                }
                else // Default Tiles
                {
                    tile.AddSquareLine(SquareLineSide.LEFT, board[y][x - 1].GetComponentInChildren<SquareTile>().GetSquareLine(SquareLineSide.RIGHT));
                    tile.GetSquareLine(SquareLineSide.LEFT).AddSquareTile(tile);
                    tile.AddSquareLine(SquareLineSide.BOTTOM, board[y - 1][x].GetComponentInChildren<SquareTile>().GetSquareLine(SquareLineSide.TOP));
                    tile.GetSquareLine(SquareLineSide.BOTTOM).AddSquareTile(tile);
                }
            }
        }

        //for (int y = 0; y < board.Count; y++)
        //{
        //    for (int x = 0; x < board[0].Count; x++)
        //    {
        //        board[y][x].GetComponentInChildren<SquareTile>().Debug_PrintStatus();
        //    }
        //}
    }

    void ChangeTileScale()
    {
        Vector2 boardSize = GameManager.Instance.BoardSize;
        // Get Bigger Side
        float newScale = Mathf.Max(boardSize.x, boardSize.y) / tileZoom;

        foreach (GameObject tilePrefab in tilePrefabs)
        {
            tilePrefab.transform.localScale /= newScale;
        }
    }

    void ResetPrefabScale()
    {
        foreach (GameObject tilePrefab in tilePrefabs)
        {
            tilePrefab.transform.localScale = new(1, 1, 1);
        }
    }

    void MoveCameraToCenterBoard()
    {
        Vector2 boardSize = GameManager.Instance.BoardSize;

        float newScale = Mathf.Max(boardSize.x, boardSize.y) / tileZoom;

        float squareSize = 1.2f;
        float squareScale = tilePrefabs[0].transform.localScale.x;

        float displacementX = boardSize.x * squareScale * squareSize / 2 - squareSize / 2 / newScale;
        float displacementY = boardSize.y * squareScale * squareSize / 2 - squareSize / 2 / newScale;
        Vector3 newPosition = new(displacementX, displacementY, -10);

        Camera.main.transform.position = newPosition;
    }

    public SquareTile GetRandomSquareTile(int numRemaining)
    {
        List<SquareTile> randomListSquareTiles = new List<SquareTile>();
        foreach (List<GameObject> row in board)
        {
            foreach (GameObject squareTileGo in row)
            {
                SquareTile squareTile = squareTileGo.transform.Find("Tile").GetComponent<SquareTile>();

                if (squareTile.GetNumRemainingLineSides() == numRemaining)
                {
                    randomListSquareTiles.Add(squareTile);
                }
            }
        }

        if (randomListSquareTiles.Count == 0)
        {
            return null;
        }

        return randomListSquareTiles[Random.Range(0, randomListSquareTiles.Count)];
    }
}
