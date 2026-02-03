using UnityEngine;


public enum GameDifficulty { EASY, NORMAL, HARD }
public enum GameMode { IA, FRIEND }

public class GameManager : Singleton<GameManager>
{
    GameDifficulty currentDifficulty;
    public GameDifficulty CurrentDifficulty { get { return currentDifficulty; } }

    GameMode currentGameMode = GameMode.IA;
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
}
