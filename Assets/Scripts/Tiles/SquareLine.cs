using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
        if (GameManager.Instance.CurrentGameMode == GameMode.FRIEND)
        {
            StartCoroutine(ToggleLine((TeamInteraction)TurnManager.Instance.CurrentTurn));
        }
        else
        {
            StartCoroutine(ToggleLine(TeamInteraction.BLUE));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (owner != LineOwner.NONE) { return; }

        ChangeSpriteColorAlpha(0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (owner != LineOwner.NONE) { return; }
        ChangeSpriteColorAlpha(0.7f);
    }

    public IEnumerator ToggleLine(TeamInteraction teamInteraction)
    {
        if (GameManager.Instance.CurrentGameMode == GameMode.AI)
        {
            if (TurnManager.Instance.CurrentTurn != TeamTurn.BLUE && teamInteraction == TeamInteraction.BLUE)
            {
                SpawnWrongInteractionParticle();
                Debug.Log("Not Your Turn");
                yield break;
            }
        }

        if (isSquareCompleted) 
        {
            SpawnWrongInteractionParticle();
            Debug.Log("Square is Completed, Cannot Remove");
            yield break;
        }

        if (owner == (LineOwner)teamInteraction)
        {
            SpawnWrongInteractionParticle();
            Debug.Log("Cannot Erase Your Line");
            yield break;
        }

        bool isPlaceLine = true;

        if (owner == LineOwner.NONE)
        {
            owner = (LineOwner)teamInteraction;
            SoundManager.Instance.PlaySound(SoundType.PlaceLine);
        }
        else
        {
            if (GameManager.Instance.GetTeamRemoveLineRemaining((Team)teamInteraction) > 0)
            {
                owner = LineOwner.NONE;
                isPlaceLine = false;
                GameManager.Instance.ReduceTeamRemoveLineRemaining((Team)teamInteraction);
                SoundManager.Instance.PlaySound(SoundType.RemoveLine);
            }
            else
            {
                SpawnWrongInteractionParticle();
                yield break;
            }
        }

        ChageSpriteTeamColor((TeamInteraction)TurnManager.Instance.CurrentTurn, isPlaceLine);

        squareTiles.ForEach(tile => { tile.SetOccupiedSide(this, isPlaceLine); } );

        UpdateSquareTile((Team)teamInteraction);

        yield return new WaitForSeconds(0.6f);

        if (GameManager.Instance.CurrentGameMode == GameMode.AI && TurnManager.Instance.CurrentTurn == TeamTurn.RED)
        {
            VersusAI.Instance.PlaceLine();
        }
    }

    public void UpdateSquareTile(Team player)
    {
        //bool hasCompletedSquare = false;
        for (int i = 0; i < squareTiles.Count; i++) 
        {
            if (squareTiles[i].GetNumRemainingLineSides() == 0)
            {
                isSquareCompleted = true;
                squareTiles[i].CompleteSquare(GetComponent<SpriteRenderer>().color);

                ScoreManager.Instance.AddPoint(player);
                ScoreManager.Instance.CheckEndGame();
                //hasCompletedSquare = true;
            }
        }

        //if (!hasCompletedSquare)
        if (!isSquareCompleted)
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


    public void ChageSpriteTeamColor(TeamInteraction team, bool isPlaceLine)
    {
        Color teamColor;
        if (!isPlaceLine) 
        {
            teamColor = new Color(0,0,0,0);
            GetComponent<SpriteRenderer>().color = teamColor;
            return;
        }

        if (team == TeamInteraction.BLUE) 
        { teamColor = new Color(0, 0, 255, 255); }

        else // Red 
        { teamColor = new Color(255, 0, 0, 255); }

        GetComponent<SpriteRenderer>().color = teamColor;
    }
    public void ChangeSpriteColorAlpha(float amount)
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, amount);
    }

    private void SpawnWrongInteractionParticle()
    {
        Vector3 mousePos = new Vector3(Mouse.current.position.value.x, Mouse.current.position.value.y, 1);

        ParticleManager.Instance.SpawnWorldSpaceParticle(ParticleType.WrongInteraction, Camera.main.ScreenToWorldPoint(mousePos));
        SoundManager.Instance.PlaySound(SoundType.WrongInteraction);
    }

    public void SetSquareComplete()
    {
        isSquareCompleted = true;
    }
}