using System.Collections.Generic;
using UnityEngine;

public class VersusAI : Singleton<VersusAI>
{
    GameDifficulty difficulty;

    Dictionary<GameDifficulty, List<float>> choiceIntervals = new();

    public void InitAI()
    {
        difficulty = GameManager.Instance.CurrentDifficulty;

        choiceIntervals.Add(GameDifficulty.EASY, new() { 0.2f, 0.8f });
        choiceIntervals.Add(GameDifficulty.NORMAL, new() { 0.2f, 0.8f });
        choiceIntervals.Add(GameDifficulty.HARD, new() { 0.2f, 0.8f });
    }

    public void PlaceLine()
    {
        BoardManager boardManager = BoardManager.Instance;

        float randomChoiceValue = Random.Range(0.0f, 1.0f);

        List<float> currentInterval = choiceIntervals[difficulty];


        // Check if there are SquareTiles to be claimed
        SquareTile squareTile = boardManager.GetRandomSquareTile(1);

        List<SquareLine> remainingSquareLines = new();

        if (squareTile != null)
        {
            remainingSquareLines = squareTile.GetRemainingLineSides();
            remainingSquareLines[0].ToggleLine(TeamInteraction.RED);
            return;
        }

        int index = 2;

        foreach (float interval in currentInterval)
        {
            // If randomChoiceValue exceeds choiceInterval, it will look for a SquareTile with more remaining Square Lines
            if (randomChoiceValue >= interval) 
            {
                index++;
            }
        }

        int preventStuck = 10;
    Repeat:
        if (preventStuck < 0) { return; }

        Debug.Log(index);
        squareTile = boardManager.GetRandomSquareTile(index);

        if (squareTile != null)
        {
            Debug.Log("Done");
            remainingSquareLines = squareTile.GetRemainingLineSides();
            remainingSquareLines[Random.Range(0, remainingSquareLines.Count)].ToggleLine(TeamInteraction.RED);
        }
        else
        {
            preventStuck--;
            index--;
            Debug.Log("GOTO");
            goto Repeat;
        }
    }
}
