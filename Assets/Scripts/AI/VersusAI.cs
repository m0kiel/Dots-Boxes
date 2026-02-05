using System.Collections.Generic;
using UnityEngine;

public class VersusAI : Singleton<VersusAI>
{
    GameDifficulty difficulty;

    Dictionary<GameDifficulty, List<float>> choiceIntervals = new();

    public void InitAI()
    {
        difficulty = GameManager.Instance.CurrentDifficulty;

        // 0 to first value will target tiles with 2 lines remaining
        // first value to second value will target tiles with 3 lines remaining
        // second value to 1 will target tiles with 4 lines remaining
        choiceIntervals.Add(GameDifficulty.EASY, new() { 0.4f, 0.6f });
        choiceIntervals.Add(GameDifficulty.NORMAL, new() { 0.2f, 0.6f });
        choiceIntervals.Add(GameDifficulty.HARD, new() { 0.05f, 0.8f });
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

        // Find an available tile to place a line
        while (true)
        {
            squareTile = boardManager.GetRandomSquareTile(index);

            if (squareTile != null)
            {
                Debug.Log("Done");
                remainingSquareLines = squareTile.GetRemainingLineSides();
                remainingSquareLines[Random.Range(0, remainingSquareLines.Count)].ToggleLine(TeamInteraction.RED);
                break;
            }
            else
            {
                index--;

                if (index < 1)
                {
                    index = 4;
                }
            }
        }
    }
}
