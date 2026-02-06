using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScreen : BaseScreen
{
    private GameObject mainButtons;

    private UIScreen currentScreen;

    [SerializeField] Color drawColor;
    [SerializeField] Color winBlueColor;
    [SerializeField] Color winRedColor;

    private void Awake()
    {
        currentScreen = GetComponent<UIScreen>();

        mainButtons = transform.Find("MainButtons").gameObject;


        #region MainButtons
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "MainMenu").onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);

            currentScreen.ChangeScreens(Screens.MainMenu);
        });
        #endregion
    }

    public override void OnGameObjectEnabled()
    {
        BoardManager.Instance.DeleteBoard();

        Team winner = ScoreManager.Instance.GetWinner();

        Color winnerColor = drawColor;
        string winnerText = "DRAW";

        switch (winner)
        {
            case Team.NONE:
                {
                    SoundManager.Instance.PlaySound(SoundType.Draw);
                    break;
                }
            case Team.BLUE:
                {
                    SoundManager.Instance.PlaySound(SoundType.Win);
                    winnerColor = winBlueColor;
                    winnerText = "BLUE WON";
                    break;
                }
            case Team.RED:
                {
                    if (GameManager.Instance.CurrentGameMode == GameMode.AI)
                    {
                        SoundManager.Instance.PlaySound(SoundType.Lose);
                    }
                    else if(GameManager.Instance.CurrentGameMode == GameMode.FRIEND)
                    {
                        SoundManager.Instance.PlaySound(SoundType.Win);
                    }

                    winnerColor = winRedColor;
                    winnerText = "RED WON";
                    break;
                }
        }

        UtilitiesUI.GetComponentByName<Image>(gameObject,"Background").color = winnerColor;

        Vector2 teamPoints = ScoreManager.Instance.GetTeamPoints();
        UtilitiesUI.GetComponentByName<TMP_Text>(transform.Find("Texts").gameObject, "Win").text = winnerText;
        UtilitiesUI.GetComponentByName<TMP_Text>(transform.Find("Texts").Find("BlueScore").gameObject, "Text").text = teamPoints[0].ToString();
        UtilitiesUI.GetComponentByName<TMP_Text>(transform.Find("Texts").Find("RedScore").gameObject, "Text").text = teamPoints[1].ToString();
    }
    public override void OnGameObjectDisabled()
    {

    }
}
