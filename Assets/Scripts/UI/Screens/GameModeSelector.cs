using TMPro;
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
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);

            GameManager.Instance.SetCurrentGameMode(GameMode.FRIEND);

            transform.parent.Find("Game").Find("Texts").Find("Difficulty").GetComponentInChildren<TMP_Text>().text = "FRIEND";

            DefaultSettings();
        });

        UtilitiesUI.GetComponentByName<Button>(mainButtons, "PlayAI").onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);

            GameManager.Instance.SetCurrentGameMode(GameMode.AI);

            transform.parent.Find("Game").Find("Texts").Find("Difficulty").GetComponentInChildren<TMP_Text>().text = GameManager.Instance.CurrentDifficulty.ToString();

            DefaultSettings();

            GameObject ai = new GameObject("AI");
            ai.AddComponent<VersusAI>();
            ai.GetComponent<VersusAI>().InitAI();
        });

        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Back").onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);

            currentScreen.ChangeScreens(Screens.MainMenu);
        });
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Easy").onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);

            ToggleDifficultyDisplay(GameDifficulty.EASY);

            GameManager.Instance.SetCurrentDifficulty(GameDifficulty.EASY);
        });
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Normal").onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);

            ToggleDifficultyDisplay(GameDifficulty.NORMAL);

            GameManager.Instance.SetCurrentDifficulty(GameDifficulty.NORMAL);
        });
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Hard").onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);

            ToggleDifficultyDisplay(GameDifficulty.HARD);

            GameManager.Instance.SetCurrentDifficulty(GameDifficulty.HARD);
        });
        #endregion
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

        currentScreen.ChangeScreens(Screens.Game);
    }

    private void ToggleDifficultyDisplay(GameDifficulty difficulty)
    {
        TMP_Text easyText = mainButtons.transform.Find("Easy").GetComponentInChildren<TMP_Text>();
        TMP_Text normalText = mainButtons.transform.Find("Normal").GetComponentInChildren<TMP_Text>();
        TMP_Text hardText = mainButtons.transform.Find("Hard").GetComponentInChildren<TMP_Text>();

        easyText.text = "";
        normalText.text = "";
        hardText.text = "";

        switch (difficulty)
        {
            case GameDifficulty.EASY:
                easyText.text = "X";
                break;
            case GameDifficulty.NORMAL:
                normalText.text = "X";
                break;
            case GameDifficulty.HARD:
                hardText.text = "X";
                break;
        }
    }

    public override void OnGameObjectEnabled()
    {

    }
    public override void OnGameObjectDisabled()
    {

    }
}