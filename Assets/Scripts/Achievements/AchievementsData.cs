using UnityEngine;

[CreateAssetMenu(fileName = "AchievementsData", menuName = "Scriptable Objects/AchievementsData")]
public class AchievementsData : ScriptableObject
{
    public Achievement achievement;
    public string title;
    public string description;
}
