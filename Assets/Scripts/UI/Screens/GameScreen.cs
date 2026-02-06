using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BaseScreen
{
    private GameObject mainButtons;

    private UIScreen currentScreen;

    private void Awake()
    {
        currentScreen = GetComponent<UIScreen>();

        mainButtons = transform.Find("MainButtons").gameObject;


        #region MainButtons
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "GameOptions").onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);

            currentScreen.ChangeScreens(Screens.GameOptions);
            GameObject grid = GameObject.Find("Grid");
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                grid.transform.GetChild(i).gameObject.SetActive(false);
            }
        });
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "SkipTurn").onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
            TurnManager.InvokeSkipTurnPressed(gameObject);
            TurnManager.Instance.ChangeTurn();

            if (GameManager.Instance.CurrentGameMode == GameMode.AI)
            {
                VersusAI.Instance.PlaceLine();
            }
        });
        #endregion
    }

    public override void OnGameObjectEnabled()
    {
        
    }
    public override void OnGameObjectDisabled()
    {
        
    }
}
