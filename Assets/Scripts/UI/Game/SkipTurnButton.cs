using UnityEngine;
using UnityEngine.UI;

public class SkipTurnButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Events_DisplaySkipTurn(object sender, TurnManager.DisplaySkipTurnEventArgs e)
    {
        if (e.turn == TeamTurn.BLUE && GameManager.Instance.GetTeamCanSkipTurn(Team.BLUE))
        {
            SetDisplayButton(true);
            GameManager.Instance.DisableTeamCanSkipTurn(Team.BLUE);
        }
        else if (e.turn == TeamTurn.RED && GameManager.Instance.GetTeamCanSkipTurn(Team.RED) && GameManager.Instance.CurrentGameMode == GameMode.FRIEND)
        {
            SetDisplayButton(true);
            GameManager.Instance.DisableTeamCanSkipTurn(Team.RED);
        }
        else
        {
            SetDisplayButton(false);
        }
    }

    private void OnEnable()
    {
        TurnManager.SkipTurnPressedEvent += Events_DisplaySkipTurn;
        button.image.enabled = true;
        button.enabled = true;
    }

    private void OnDisable()
    {
        TurnManager.SkipTurnPressedEvent -= Events_DisplaySkipTurn;
    }

    private void SetDisplayButton(bool state)
    {
        button.image.enabled = state;
        button.enabled = state;
        button.gameObject.transform.GetChild(0).gameObject.SetActive(state);
    }    
}
