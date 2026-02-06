using UnityEngine;
using UnityEngine.UI;

public class GameOptionsScreen : BaseScreen
{
    private GameObject mainButtons;

    private UIScreen currentScreen;

    private void Awake()
    {
        currentScreen = GetComponent<UIScreen>();

        mainButtons = transform.Find("MainButtons").gameObject;


        #region MainButtons
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Resume").onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);

            currentScreen.ChangeScreens(Screens.Game);
            GameObject grid = GameObject.Find("Grid");
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                grid.transform.GetChild(i).gameObject.SetActive(true);
            }    
        });
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Exit").onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);

            BoardManager.Instance.DeleteBoard();
            currentScreen.ChangeScreens(Screens.MainMenu);
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
