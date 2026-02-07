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

        CheckAchievements();
    }
    public override void OnGameObjectDisabled()
    {
        GameObject popupAchievements = transform.Find("PopupAchievements").gameObject;

        for (int i = popupAchievements.transform.childCount-1; i >= 0 ; i--)
        {
            Destroy(popupAchievements.transform.GetChild(i).gameObject);
        }
    }

    public void CheckAchievements()
    {
        if (ScoreManager.Instance.GetWinner() == Team.RED) { return; }

        if (GameManager.Instance.CurrentGameMode == GameMode.FRIEND) { return; }

        AchievementsManager achievementsManager = AchievementsManager.Instance;

        if (GameManager.Instance.CurrentDifficulty == GameDifficulty.EASY)
        {
            achievementsManager.CompleteAchievement(Achievement.WinEasyAI);
        }
        else if (GameManager.Instance.CurrentDifficulty == GameDifficulty.NORMAL)
        {
            achievementsManager.CompleteAchievement(Achievement.WinNormalAI);
        }
        else if (GameManager.Instance.CurrentDifficulty == GameDifficulty.HARD)
        {
            achievementsManager.CompleteAchievement(Achievement.WinHardAI);
            if (GameManager.Instance.GetTeamRemoveLineRemaining(Team.BLUE) > 0 && GameManager.Instance.GetTeamCanSkipTurn(Team.BLUE))
            {
                achievementsManager.CompleteAchievement(Achievement.WinPerfectHardAI);
            }
        }

        // TeamPoints.x = BlueTeam
        if (ScoreManager.Instance.GetTeamPoints().x == GameManager.Instance.GetMaxPoints())
        {
            achievementsManager.CompleteAchievement(Achievement.WinFlawlessAI);
        }
    }    
}
