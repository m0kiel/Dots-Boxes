using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    TMP_Text textBox;

    [SerializeField] Team team;

    private void Events_ChangeTeamScoreDisplay(object sender, ScoreManager.ChangeTeamScoreDisplayEventArgs e)
    {
        if (e.team == team)
        {
            textBox.text = e.score.ToString();
        }
    }

    void Start()
    {
        textBox = GetComponentInChildren<TMP_Text>();
        ScoreManager.ChangeTeamScoreDisplay += Events_ChangeTeamScoreDisplay;
    }
}
