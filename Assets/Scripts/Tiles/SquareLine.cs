using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TeamInteraction { BLUE = 1, RED = 2}
public enum LineOwner { NONE = 0, BLUE = 1, RED = 2 }

public class SquareLine : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] List<SquareTile> squareTiles = new();
    [SerializeField] SquareLineSide side;

    LineOwner owner = LineOwner.NONE;

    bool isSquareCompleted = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleLine(TeamInteraction.BLUE);
    }

    public void ToggleLine(TeamInteraction teamInteraction)
    {
        Debug.Log("TOGGLE");

        for (int i = 0; i < squareTiles.Count; i++)
        {
            squareTiles[i].gameObject.SetActive(false);
        }

        return;
        if (GameManager.Instance.CurrentGameMode == GameMode.IA)
        {
            if (TurnManager.Instance.CurrentTurn != TeamTurn.BLUE)
            {
                Debug.Log("Not Your Turn");
                return;
            }
        }

        if (isSquareCompleted) 
        {
            Debug.Log("Square is Completed, Cannot Remove");
            return;
        }

        if (owner == (LineOwner)teamInteraction)
        {
            Debug.Log("Cannot Erase Your Line");
            return;
        }

        switch (owner)
        {
            case LineOwner.NONE:
                {
                    owner = (LineOwner)teamInteraction;
                    break;
                }
            case LineOwner.BLUE:
                {
                    
                    break;
                }
            case LineOwner.RED:
                {
                    break;
                }
        }
    }

    public void UpdateSquareTile()
    {
        for (int i = 0; i < squareTiles.Count; i++) 
        {
            if (squareTiles[i].GetNumRemainingLineSides() == 0)
            {
                isSquareCompleted = true;
                squareTiles[i].CompleteSquare();
            }
        }
    }

    public void AddSquareTile(SquareTile tile)
    {
        squareTiles.Add(tile);
    }

    public SquareLineSide GetSquareLineSide()
    { return side; }

    public List<SquareTile> GetSquareTiles() 
    { return squareTiles; }
}
