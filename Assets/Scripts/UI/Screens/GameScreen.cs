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
            currentScreen.ChangeScreens(Screens.GameOptions);
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
