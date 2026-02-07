using UnityEngine;

public enum GameDifficulty { EASY = 3, NORMAL = 2, HARD = 1}
public enum GameMode { AI, FRIEND }

public enum Team { NONE = 0, BLUE = 1, RED = 2 }
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameDifficulty currentDifficulty = GameDifficulty.NORMAL;
    public GameDifficulty CurrentDifficulty { get { return currentDifficulty; } }

    private GameMode currentGameMode = GameMode.AI;
    public GameMode CurrentGameMode { get { return currentGameMode; } }

    private Vector2 boardSize = new(6, 6);
    public Vector2 BoardSize { get { return boardSize; } }

    [SerializeField] private Color blueTeamColor;
    [SerializeField] private Color redTeamColor;

    [SerializeField] private Color blueCompleteTeamColor;
    [SerializeField] private Color redCompleteTeamColor;

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

            GameObject blueLinesRemaining = GameObject.Find("UI").transform.Find("Game").Find("Texts").Find("BlueRemoveLines").gameObject;
            for (int i = 0; i < maxRemoveLine; i++)
            {
                blueLinesRemaining.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < blueRemoveLineRemaining; i++)
            {
                blueLinesRemaining.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else if (team == Team.RED) 
        {
            redRemoveLineRemaining--;

            GameObject redLinesRemaining = GameObject.Find("UI").transform.Find("Game").Find("Texts").Find("RedRemoveLines").gameObject;
            for (int i = 0; i < maxRemoveLine; i++)
            {
                redLinesRemaining.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < redRemoveLineRemaining; i++)
            {
                redLinesRemaining.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public bool GetTeamCanSkipTurn(Team team)
    {
        return team == Team.BLUE ? blueCanSkipTurn : redCanSkipTurn;
    }

    public bool GetBlueCanSkipTurn()
    { 
        return blueCanSkipTurn; 
    }

    public bool GetRedCanSkipTurn()
    { return redCanSkipTurn; }

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

    public int GetMaxPoints()
    {
        return (int)(boardSize.x * boardSize.y);
    }
    public Color GetTeamColor(Team team)
    {
        return team == Team.BLUE ? blueTeamColor : redTeamColor;
    }
    public Color GetCompleteTeamColor(Team team)
    {
        return team == Team.BLUE ? blueCompleteTeamColor : redCompleteTeamColor;
    }

    public void SetBoardSize(int width, int height)
    {
        boardSize = new(width, height);
    }

    public void SetCurrentDifficulty(GameDifficulty difficulty)
    {
        currentDifficulty = difficulty;
        blueRemoveLineRemaining = (int)difficulty;
        redRemoveLineRemaining = (int)difficulty;
    }

    public void SetCurrentGameMode(GameMode gameMode)
    { currentGameMode = gameMode; }

    public void ResetGame()
    {
        GameObject ai = GameObject.Find("AI");
        if (ai != null)
        {
            Destroy(ai);
        }

        blueRemoveLineRemaining = (int)currentDifficulty;
        redRemoveLineRemaining = (int)currentDifficulty;

        GameObject blueLinesRemaining = GameObject.Find("UI").transform.Find("Game").Find("Texts").Find("BlueRemoveLines").gameObject;
        GameObject redLinesRemaining = GameObject.Find("UI").transform.Find("Game").Find("Texts").Find("RedRemoveLines").gameObject;
        for (int i = 0; i < maxRemoveLine; i++)
        {
            blueLinesRemaining.transform.GetChild(i).gameObject.SetActive(false);
            redLinesRemaining.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < (int)currentDifficulty; i++)
        {
            blueLinesRemaining.transform.GetChild(i).gameObject.SetActive(true);
            redLinesRemaining.transform.GetChild(i).gameObject.SetActive(true);
        }

        blueCanSkipTurn = true;
        redCanSkipTurn = true;
    }
}
