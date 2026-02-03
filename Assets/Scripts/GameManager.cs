using UnityEngine;


public enum GameDifficulty { EASY, NORMAL, HARD }
public enum GameMode { IA, FRIEND }

public class GameManager : Singleton<GameManager>
{
    GameDifficulty currentDifficulty;
    public GameDifficulty CurrentDifficulty { get { return currentDifficulty; } }

    GameMode currentGameMode = GameMode.IA;
    public GameMode CurrentGameMode { get { return currentGameMode; } }

    
    public void SetCurrentDifficulty(GameDifficulty difficulty)
    { currentDifficulty = difficulty; }

    public void SetCurrentGameMode(GameMode gameMode)
    { currentGameMode = gameMode; }
}
