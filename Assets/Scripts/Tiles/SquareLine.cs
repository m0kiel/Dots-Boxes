using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TeamInteraction { BLUE = 1, RED = 2}
public enum LineOwner { NONE = 0, BLUE = 1, RED = 2 }

public class SquareLine : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
       
        ToggleLine((TeamInteraction)TurnManager.Instance.CurrentTurn);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (owner != LineOwner.NONE) { return; }

        ChangeSpriteColorAlpha(0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (owner != LineOwner.NONE) { return; }
        ChangeSpriteColorAlpha(0.4f);
    }

    public void ToggleLine(TeamInteraction teamInteraction)
    {
        if (GameManager.Instance.CurrentGameMode == GameMode.AI)
        {
            if (TurnManager.Instance.CurrentTurn != TeamTurn.BLUE)
            {
                Debug.Log("Not Your Turn");
                //return;
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
            case LineOwner.RED:
                {
                    squareTiles.ForEach(tile => { tile.SetOccupiedSide(this, false); });
                    return;
                }
        }

        SoundManager.Instance.PlaySound(SoundType.PlaceLine);

        ChageSpriteTeamColor((TeamInteraction)TurnManager.Instance.CurrentTurn);

        squareTiles.ForEach(tile => { tile.SetOccupiedSide(this, true); } );


        UpdateSquareTile((Team)teamInteraction);
        if (GameManager.Instance.CurrentGameMode == GameMode.AI && TurnManager.Instance.CurrentTurn == TeamTurn.RED)
        {
            VersusAI.Instance.PlaceLine();
        }
    }

    public void UpdateSquareTile(Team player)
    {
        bool hasCompletedSquare = false;
        for (int i = 0; i < squareTiles.Count; i++) 
        {
            if (squareTiles[i].GetNumRemainingLineSides() == 0)
            {
                isSquareCompleted = true;
                squareTiles[i].CompleteSquare(GetComponent<SpriteRenderer>().color);

                ScoreManager.Instance.AddPoint(player);
                ScoreManager.Instance.CheckEndGame();
                hasCompletedSquare = true;
            }
        }

        if (!hasCompletedSquare)
        {
            TurnManager.Instance.ChangeTurn();
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


    public void ChageSpriteTeamColor(TeamInteraction team)
    {
        Color teamColor;
        if (team == TeamInteraction.BLUE) 
        { teamColor = new Color(0, 0, 255, 255); }

        else // Red 
        { teamColor = new Color(255, 0, 0, 255); }

        GetComponent<SpriteRenderer>().color = teamColor;
    }
    public void ChangeSpriteColorAlpha(float amount)
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, amount);
    }
}