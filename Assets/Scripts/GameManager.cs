using System;
using UnityEngine;


public enum GameDifficulty { EASY, NORMAL, HARD }
public enum GameMode { AI, FRIEND }

public enum Team { NONE = 0, BLUE = 1, RED = 2 }
public class GameManager : Singleton<GameManager>
{
    GameDifficulty currentDifficulty = GameDifficulty.NORMAL;
    public GameDifficulty CurrentDifficulty { get { return currentDifficulty; } }

    GameMode currentGameMode = GameMode.AI;
    public GameMode CurrentGameMode { get { return currentGameMode; } }

    Vector2 boardSize = new(6, 6);
    public Vector2 BoardSize { get { return boardSize; } }


    // Original Features

    private int maxRemoveLine = 3;
    private int blueRemoveLineRemaining = 3;
    private int redRemoveLineRemaining = 3;

    private bool blueCanSkipTurn = true;
    private bool redCanSkipTurn = true;

    private void Start()
    {
        blueRemoveLineRemaining = maxRemoveLine;
        redRemoveLineRemaining = maxRemoveLine;
    }

    public int GetTeamRemoveLineRemaining(Team team)
    {
        return team == Team.BLUE ? blueRemoveLineRemaining : redRemoveLineRemaining;
    }
    public void ReduceTeamRemoveLineRemaining(Team team)
    {
        if (team == Team.BLUE)
        {
            blueRemoveLineRemaining--;
        }
        else if (team == Team.RED) 
        {
            redRemoveLineRemaining--;
        }
    }

    public bool GetTeamCanSkipTurn(Team team)
    {
        return team == Team.BLUE ? blueCanSkipTurn : redCanSkipTurn;
    }

    public void DisableTeamCanSkipTurn(Team team)
    {
        if (team == Team.BLUE)
        {
            blueCanSkipTurn = false;
        }
        else if (team == Team.RED)
        {
            redCanSkipTurn = false;
        }
    }

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

        blueRemoveLineRemaining = maxRemoveLine;
        redRemoveLineRemaining = maxRemoveLine;

        blueCanSkipTurn = true;
        redCanSkipTurn = true;
}
}
