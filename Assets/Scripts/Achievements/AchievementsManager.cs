using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum Achievement { WinEasyAI = 1, WinNormalAI = 2, WinHardAI = 3, WinFlawlessAI = 4, WinPerfectHardAI = 5 }
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
        return PlayerPrefs.GetInt(achievementTitle.ToString());
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
