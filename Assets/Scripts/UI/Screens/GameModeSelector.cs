using UnityEngine;
using UnityEngine.UI;

public class GameModeSelector : BaseScreen
{
    private GameObject mainButtons;

    private UIScreen currentScreen;

    private void Awake()
    {
        currentScreen = GetComponent<UIScreen>();

        mainButtons = transform.Find("MainButtons").gameObject;


        #region MainButtons
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "PlayFriend").onClick.AddListener(() =>
        {
            GameManager.Instance.SetCurrentGameMode(GameMode.FRIEND);
            DefaultSettings();
        });

        UtilitiesUI.GetComponentByName<Button>(mainButtons, "PlayAI").onClick.AddListener(() =>
        {
            GameManager.Instance.SetCurrentGameMode(GameMode.AI);
            DefaultSettings();
            GameObject ai = new GameObject("AI");
            ai.AddComponent<VersusAI>();
            ai.GetComponent<VersusAI>().InitAI();
        });

        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Back").onClick.AddListener(() =>
        {
            currentScreen.ChangeScreens(Screens.MainMenu);
        });
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Easy").onClick.AddListener(() =>
        {
            GameManager.Instance.SetCurrentDifficulty(GameDifficulty.EASY);
        });
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Normal").onClick.AddListener(() =>
        {
            GameManager.Instance.SetCurrentDifficulty(GameDifficulty.NORMAL);
        });
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Hard").onClick.AddListener(() =>
        {
            GameManager.Instance.SetCurrentDifficulty(GameDifficulty.HARD);
        });
        #endregion

        //OnGameObjectEnabled();
    }

    public override void OnGameObjectEnabled()
    {

    }
    public override void OnGameObjectDisabled()
    {

    }

    private void DefaultSettings()
    {
        // Easier access and less Find() searches
        GameManager gameManager = GameManager.Instance;
        TurnManager turnManager = TurnManager.Instance;
        ScoreManager scoreManager = ScoreManager.Instance;
        BoardManager boardManager = BoardManager.Instance;

        int boardWidth = (int)UtilitiesUI.GetComponentByName<Slider>(transform.Find("Sliders").gameObject, "BoardWidth").value;
        int boardHeight = (int)UtilitiesUI.GetComponentByName<Slider>(transform.Find("Sliders").gameObject, "BoardHeight").value;

        // Verify values arent from previous games
        scoreManager.ResetScore();
        turnManager.ResetTurn();
        gameManager.ResetGame();

        // Start Initializing variables
        gameManager.SetBoardSize(boardWidth, boardHeight);
        boardManager.InitBoard();
        turnManager.StartTurn();

        currentScreen.ChangeScreens(Screens.Game);
    }
}
