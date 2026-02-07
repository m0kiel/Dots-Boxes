using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsScreen : BaseScreen
{
    private GameObject mainButtons;

    private UIScreen currentScreen;

    private GameObject achievementDisplayList;

    [SerializeField] private GameObject achievementDisplayPrefab;

    private void Awake()
    {
        currentScreen = GetComponent<UIScreen>();

        mainButtons = transform.Find("MainButtons").gameObject;

        achievementDisplayList = transform.Find("AchievementList").gameObject;

        DisplayAchievements();

        #region MainButtons
        UtilitiesUI.GetComponentByName<Button>(mainButtons, "Back").onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);

            currentScreen.ChangeScreens(Screens.MainMenu);
        });
        #endregion
    }

    public override void OnGameObjectEnabled()
    {
        CheckForAchievementCompleted();

    }
    public override void OnGameObjectDisabled()
    {

    }

    private void DisplayAchievements()
    {
        AchievementsManager achievementsManager = AchievementsManager.Instance;

        List<AchievementsData> achievementsList = achievementsManager.GetAchievementsList();

        for (int i = 0; i < achievementsList.Count; i++)
        {
            GameObject achievement = Instantiate(achievementDisplayPrefab, achievementDisplayList.transform);
            achievement.transform.Find("Title").GetComponent<TMP_Text>().text = achievementsManager.GetAchievementToString(achievementsList[i].title);
            achievement.transform.Find("Description").GetComponent<TMP_Text>().text = achievementsList[i].description;

            bool isTickVisible = achievementsManager.IsAchievementCompleted(achievementsList[i].title) == 1 ? true : false;

            achievement.transform.Find("Tick").gameObject.SetActive(isTickVisible);
        }
    }

    private void CheckForAchievementCompleted()
    {
        AchievementsManager achievementsManager = AchievementsManager.Instance;

        List<AchievementsData> achievementsList = achievementsManager.GetAchievementsList();

        for (int i = 0; i < achievementDisplayList.transform.childCount; i++)
        {
            GameObject achievement = achievementDisplayList.transform.GetChild(i).gameObject;
            bool isTickVisible = achievementsManager.IsAchievementCompleted(achievementsList[i].title) == 1 ? true : false;

            achievement.transform.Find("Tick").gameObject.SetActive(isTickVisible);
        }
    }
}