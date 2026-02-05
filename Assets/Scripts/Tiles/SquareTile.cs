using System.Collections.Generic;
using UnityEngine;

public enum SquareLineSide { TOP = 1, BOTTOM = -1, LEFT = 2, RIGHT = -2 };

public class SquareTile : MonoBehaviour
{
    Dictionary<SquareLineSide, SquareLine> squareLineSides = new();
    Dictionary<SquareLineSide, bool> squareLineSidesOccupied = new();

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteSquare(Color teamColor)
    {
        SoundManager.Instance.PlaySound(SoundType.CompleteSquare);

        GetComponent<SpriteRenderer>().color = teamColor; // Green
    }    

    public void AddSquareLine(SquareLineSide key, SquareLine line)
    {
        squareLineSides.Add(key, line);
        squareLineSidesOccupied.Add(key, false);
    }

    public int GetNumRemainingLineSides()
    {
        int counter = squareLineSidesOccupied.Count;

        foreach (bool isOccupied in squareLineSidesOccupied.Values)
        {
            if (isOccupied)
            {
                counter--;
            }
        }
        return counter;
    }

    public List<SquareLine> GetRemainingLineSides()
    { 
        List<SquareLine> remainingSides = new List<SquareLine>();

        foreach (SquareLineSide lineSide in squareLineSidesOccupied.Keys)
        {
            if (squareLineSidesOccupied[lineSide] == false)
            {
                remainingSides.Add(squareLineSides[lineSide]);
            }
        }
        return remainingSides;
    }    

    public SquareLine GetSquareLine(SquareLineSide side)
    {
        return squareLineSides[side];
    }


    public void Debug_PrintStatus()
    {
        string logMessage = "";

        logMessage += squareLineSides[SquareLineSide.TOP].gameObject.transform.parent.name;
        foreach (SquareLineSide side in squareLineSides.Keys)
        {
            
            logMessage += " Key: " + side + ", SquareTilesCount: " + squareLineSides[side].GetSquareTiles().Count;

            logMessage += " = ( ";
            for (int i = 0; i < squareLineSides[side].GetSquareTiles().Count; i++)
            {
                logMessage += squareLineSides[side].GetSquareTiles()[i].gameObject.transform.parent.name + ", ";
            }
            logMessage += " )";
            logMessage += " | ";
        }

        Debug.Log(logMessage);
    }

    public void SetOccupiedSide(SquareLine line, bool state)
    {
        foreach (var lines in squareLineSides)
        {
            if (lines.Value == line)
            {
                squareLineSidesOccupied[lines.Key] = state;
            }
        }
    }
}
