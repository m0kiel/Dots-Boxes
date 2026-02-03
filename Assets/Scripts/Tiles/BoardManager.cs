using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] List<GameObject> tilePrefabs = new();


    List<List<GameObject>> board = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateBoard(6,6);
        LinkBoard();
    }

    public void CreateBoard(int width, int height)
    {
        float squareSize = 1.2f;
        float squareScale = tilePrefabs[0].transform.localScale.x;

        for (int y = 0; y < height; y++)
        {
            List<GameObject> boardTiles = new();

            for (int x = 0; x < width; x++)
            {
                GameObject tile;

                if (x == 0 && y == 0) // Bottom Left Corner Tile
                {
                    tile = Instantiate(tilePrefabs[0], new Vector3(x*squareSize*squareScale, y * squareSize * squareScale, 0), Quaternion.identity);
                }
                else if (y == 0) // Bottom Row Tiles
                {
                    tile = Instantiate(tilePrefabs[1], new Vector3(x * squareSize * squareScale, y * squareSize * squareScale, 0), Quaternion.identity);
                }
                else if (x == 0) // Left Column Tiles
                {
                    tile = Instantiate(tilePrefabs[2], new Vector3(x * squareSize * squareScale, y * squareSize * squareScale, 0), Quaternion.identity);
                }
                else // Default Tiles
                {
                    tile = Instantiate(tilePrefabs[3], new Vector3(x * squareSize * squareScale, y * squareSize * squareScale, 0), Quaternion.identity);
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

                if (x== 0 && y == 0) { continue; } // First Pass

                // Remaining Links
                if (y == 0) // Bottom Row Tiles
                {
                    tile.AddSquareLine(SquareLineSide.LEFT, board[y][x - 1].GetComponentInChildren<SquareTile>().GetSquareLine(SquareLineSide.RIGHT));
                    tile.GetSquareLine(SquareLineSide.LEFT).AddSquareTile(tile);
                }
                else if (x == 0) // Left Column Tiles
                {
                    tile.AddSquareLine(SquareLineSide.BOTTOM, board[y -1][x].GetComponentInChildren<SquareTile>().GetSquareLine(SquareLineSide.TOP));
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

        for (int y = 0; y < board.Count; y++)
        {
            for (int x = 0; x < board[0].Count; x++)
            {
                //board[y][x].GetComponentInChildren<SquareTile>().Debug_PrintStatus();
            }
        }
    }
}
