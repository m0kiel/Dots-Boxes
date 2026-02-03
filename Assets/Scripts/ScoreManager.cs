using JetBrains.Annotations;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    int blueTeamPoints = 0;
    int redTeamPoints = 0;

    public void ResetGame()
    {
        blueTeamPoints = 0;
        redTeamPoints = 0;
    }

    public void AddPoint(LineOwner player)
    {
        if (player == LineOwner.BLUE) 
        { blueTeamPoints++; }
        else if (player == LineOwner.RED) 
        { redTeamPoints++; }
    }
}
