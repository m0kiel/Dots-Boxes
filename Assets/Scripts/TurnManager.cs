using UnityEngine;


public enum TeamTurn { BLUE = 1, RED = 2 }
public class TurnManager : Singleton<TurnManager>
{
    private TeamTurn currentTurn = TeamTurn.BLUE;
    public TeamTurn CurrentTurn { get { return currentTurn; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTurn()
    {
        currentTurn = currentTurn == TeamTurn.BLUE ? TeamTurn.RED : TeamTurn.BLUE;
    }
}
