using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum Achievement { WinEasyAI = 1, WinNormalAI = 2, WinHardAI = 3, WinFlawlessAI = 4, WinPerfectHardAI = 5 }
public class AchievementsManager : Singleton<AchievementsManager>
{
    [SerializeField] List<AchievementsData> achievementsList = new();
    Dictionary<Achievement, string> achievementToString = new();

    [SerializeField] GameObject achievementPopupPrefab;

    private void Awake()
    {
        Debug.Log("BEFORE");
        achievementToString.Add(Achievement.WinEasyAI, "EASY WIN");
        achievementToString.Add(Achievement.WinNormalAI, "NORMAL WIN");
        achievementToString.Add(Achievement.WinHardAI, "HARD WIN");
        achievementToString.Add(Achievement.WinFlawlessAI, "FLAWLESS WIN");
        achievementToString.Add(Achievement.WinPerfectHardAI, "PERFECT HARD WIN");
        Debug.Log("AFTER");
    }

    public List<AchievementsData> GetAchievementsList()
    {
        return achievementsList;
    }
    
    public void CompleteAchievement(Achievement achievementTitle)
    {
        if (IsAchievementCompleted(achievementTitle) == 0)
        {
            PlayerPrefs.SetInt(achievementTitle.ToString(), 1);
            // SoundManager.Instance.PlaySound(SoundType.AchievementCompleted);

            GameObject popupAchievements = GameObject.Find("UI").transform.Find("EndGame").Find("PopupAchievements").gameObject;
            GameObject achievementPopup = Instantiate(achievementPopupPrefab, popupAchievements.transform);
            achievementPopup.transform.Find("Title").GetComponent<TMP_Text>().text = achievementTitle.ToString() + "\nUNLOCKED";
        }
    }

    public int IsAchievementCompleted(Achievement achievementTitle)
    {
        return PlayerPrefs.GetInt(achievementTitle.ToString());
    }

    public string GetAchievementToString(Achievement achievement)
    {
        return achievementToString[achievement];
    }
}
