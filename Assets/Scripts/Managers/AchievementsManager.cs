using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum Achievement { WinEasyAI = 0, WinNormalAI = 1, WinHardAI = 2, WinFlawlessAI = 3, WinPerfectHardAI = 4 }
public class AchievementsManager : Singleton<AchievementsManager>
{
    [SerializeField] private List<AchievementsData> achievementsList = new();

    [SerializeField] private GameObject achievementPopupPrefab;
    [SerializeField] private GameObject achievementDisplayPrefab;

    public void CompleteAchievement(Achievement achievementTitle)
    {
        if (IsAchievementCompleted(achievementTitle) == 0)
        {
            PlayerPrefs.SetInt(achievementTitle.ToString(), 1);

            GameObject popupAchievements = GameObject.Find("UI").transform.Find("EndGame").Find("PopupAchievements").gameObject;
            GameObject achievementPopup = Instantiate(achievementPopupPrefab, popupAchievements.transform);
            achievementPopup.transform.Find("Title").GetComponent<TMP_Text>().text = achievementsList[(int)achievementTitle].title + "\nUNLOCKED";
        }
    }

    public int IsAchievementCompleted(Achievement achievementTitle)
    {
        int titleint = PlayerPrefs.GetInt(achievementTitle.ToString());
        return titleint;
    }

    public List<AchievementsData> GetAchievementsList()
    { 
        return achievementsList; 
    }

    public GameObject GetAchievementPopupPrefab()
    { 
        return achievementPopupPrefab; 
    }

    public GameObject GetAchievementDisplayPrefab()
    {
        return achievementDisplayPrefab;
    }
}
