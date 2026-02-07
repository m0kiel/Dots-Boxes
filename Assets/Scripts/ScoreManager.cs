using System;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    #region EVENTS
    public static event EventHandler<ChangeTeamScoreDisplayEventArgs> ChangeTeamScoreDisplay;

    public class ChangeTeamScoreDisplayEventArgs : EventArgs { public int score; public Team team; }

    public static void InvokeChangeTeamScoreDisplay(GameObject sender, int scoreToSet, Team teamInteraction)
    {
        ChangeTeamScoreDisplay?.Invoke(sender, new ChangeTeamScoreDisplayEventArgs { score = scoreToSet, team = teamInteraction });
    }

    #endregion

    private int blueTeamPoints = 0;
    private int redTeamPoints = 0;


    public void AddPoint(Team player)
    {
        if (player == Team.BLUE) 
        { 
            blueTeamPoints++;
            InvokeChangeTeamScoreDisplay(gameObject, blueTeamPoints, Team.BLUE);
        }
        else if (player == Team.RED) 
        { 
            redTeamPoints++;
            InvokeChangeTeamScoreDisplay(gameObject, redTeamPoints, Team.RED);
        }
    }

    public void CheckEndGame()
    {
        int maxPoints = GameManager.Instance.GetMaxPoints();
        if (blueTeamPoints + redTeamPoints < maxPoints) { return; }

        UIScreenHelper.Instance.GetScreen(Screens.Game).ChangeScreens(Screens.EndGame);
    }
    public Team GetWinner()
    {
        Team team = Team.NONE;
        if (blueTeamPoints > redTeamPoints)
        {
            return Team.BLUE;
        }
        else if (blueTeamPoints < redTeamPoints)
        {
            return Team.RED;
        }
        else
        {
            return team;
        }
    }

    public Vector2 GetTeamPoints()
    { 
        return new Vector2(blueTeamPoints, redTeamPoints);
    }
    public void ResetScore()
    {
        blueTeamPoints = 0;
        redTeamPoints = 0;
        InvokeChangeTeamScoreDisplay(gameObject, blueTeamPoints, Team.BLUE);
        InvokeChangeTeamScoreDisplay(gameObject, redTeamPoints, Team.RED);
    }
}
