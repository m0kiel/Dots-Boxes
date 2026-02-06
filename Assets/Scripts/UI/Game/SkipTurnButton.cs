using UnityEngine;
using UnityEngine.UI;

public class SkipTurnButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Events_SkipTurnPressed(object sender, TurnManager.SkipTurnPressedEventArgs e)
    {
        if (e.turn == TeamTurn.BLUE && GameManager.Instance.GetTeamCanSkipTurn(Team.BLUE))
        {
            GameManager.Instance.DisableTeamCanSkipTurn(Team.BLUE);
        }
        else if (e.turn == TeamTurn.RED && GameManager.Instance.GetTeamCanSkipTurn(Team.RED) && GameManager.Instance.CurrentGameMode == GameMode.FRIEND)
        {
            GameManager.Instance.DisableTeamCanSkipTurn(Team.RED);
        }
    }
    private void Events_CheckSkipTurnState(object sender, TurnManager.CheckSkipTurnStateEventArgs e)
    {
        CheckSkipTurnState(e.turn);
        
    }

    private void OnEnable()
    {
        TurnManager.SkipTurnPressedEvent += Events_SkipTurnPressed;
        TurnManager.CheckSkipTurnStateEvent += Events_CheckSkipTurnState;


        CheckSkipTurnState(TurnManager.Instance.CurrentTurn);
        Debug.Log("ENABLED");
    }

    private void OnDisable()
    {
        Debug.Log("DISABLED");
        TurnManager.SkipTurnPressedEvent -= Events_SkipTurnPressed;
        TurnManager.CheckSkipTurnStateEvent -= Events_CheckSkipTurnState;
    }

    private void SetDisplayButton(bool state)
    {
        button.image.enabled = state;
        button.enabled = state;
        button.gameObject.transform.GetChild(0).gameObject.SetActive(state);
    }
    
    private void CheckSkipTurnState(TeamTurn turn)
    {
        if (turn == TeamTurn.BLUE && GameManager.Instance.GetTeamCanSkipTurn(Team.BLUE))
        {
            SetDisplayButton(true);
        }
        else if (turn == TeamTurn.RED && GameManager.Instance.GetTeamCanSkipTurn(Team.RED) && GameManager.Instance.CurrentGameMode == GameMode.FRIEND)
        {
            SetDisplayButton(true);
        }
        else
        {
            SetDisplayButton(false);
        }
    }
}
