using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : BaseScreen
{
    private GameObject mainButtons;

    private UIScreen currentScreen;

    private void Awake()
    {
        currentScreen = GetComponent<UIScreen>();

        mainButtons = transform.Find("MainButtons").gameObject;


        #region MainButtons
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Play").onClick.AddListener(() =>
        {
            currentScreen.ChangeScreens(Screens.GameModeSelector);
        });
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Options").onClick.AddListener(() =>
        {
            currentScreen.ChangeScreens(Screens.Options);
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
