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
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Confirm").onClick.AddListener(() =>
        {
            int boardWidth = (int)UtilitiesUI.GetComponentByName<Slider>(transform.Find("Sliders").gameObject, "BoardWidth").value;
            int boardHeight = (int)UtilitiesUI.GetComponentByName<Slider>(transform.Find("Sliders").gameObject, "BoardHeight").value;
            GameManager.Instance.SetBoardSize(boardWidth, boardHeight);

            BoardManager.Instance.InitBoard();

            ScoreManager.Instance.ResetGame();
            TurnManager.Instance.ResetGame();

            currentScreen.ChangeScreens(Screens.Game);
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
}
