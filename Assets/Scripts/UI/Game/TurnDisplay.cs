using UnityEngine;
using UnityEngine.UI;

public class TurnDisplay : MonoBehaviour
{
    [SerializeField] private Color blueTeam;
    [SerializeField] private Color redTeam;

    private void Events_DisplayTurn(object sender, TurnManager.DisplayTurnEventArgs e)
    {
        GetComponent<Image>().color = TurnManager.Instance.CurrentTurn == TeamTurn.BLUE ? blueTeam : redTeam;
    }

    private void OnEnable()
    {
        TurnManager.DisplayTurnEvent += Events_DisplayTurn;
    }

    private void OnDisable()
    {
        TurnManager.DisplayTurnEvent -= Events_DisplayTurn;
    }
}
