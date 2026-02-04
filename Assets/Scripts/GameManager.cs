using System;
using UnityEngine;


public enum GameDifficulty { EASY, NORMAL, HARD }
public enum GameMode { AI, FRIEND }

public enum Team { NONE = 0, BLUE = 1, RED = 2 }
public class GameManager : Singleton<GameManager>
{
    GameDifficulty currentDifficulty = GameDifficulty.EASY;
    public GameDifficulty CurrentDifficulty { get { return currentDifficulty; } }

    GameMode currentGameMode = GameMode.AI;
    public GameMode CurrentGameMode { get { return currentGameMode; } }

    Vector2 boardSize = new(6, 6);
    public Vector2 BoardSize { get { return boardSize; } }
    
    public void SetCurrentDifficulty(GameDifficulty difficulty)
    { currentDifficulty = difficulty; }

    public void SetCurrentGameMode(GameMode gameMode)
    { currentGameMode = gameMode; }

    public int GetMaxPoints()
    {
        return (int)(boardSize.x * boardSize.y);
    }

    public void SetBoardSize(int width, int height)
    {
        boardSize = new(width, height);
    }

    public void ResetGame()
    {
        GameObject ai = GameObject.Find("AI");
        if (ai != null)
        {
            Destroy(ai);
        }
    }
}
