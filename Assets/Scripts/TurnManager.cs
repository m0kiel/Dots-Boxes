using System;
using UnityEngine;


public enum TeamTurn { BLUE = 1, RED = 2 }
public class TurnManager : Singleton<TurnManager>
{
    #region EVENTS
    public static event EventHandler<ChangeTurnEventArgs> ChangeTurnEvent;

    public class ChangeTurnEventArgs : EventArgs { public TeamTurn turn; }

    public static void InvokeChangeTurn(GameObject sender, TeamTurn newTurn)
    {
        ChangeTurnEvent?.Invoke(sender, new ChangeTurnEventArgs { turn = newTurn });
    }
    private void Events_ChangeTurnFriend(object sender, ChangeTurnEventArgs e)
    {
        ChangeTurn();
    }
    private void Events_ChangeTurnAI(object sender, ChangeTurnEventArgs e)
    {
        ChangeTurn();
    }



    public static event EventHandler<SkipTurnPressedEventArgs> SkipTurnPressedEvent;
    public static event EventHandler<CheckSkipTurnStateEventArgs> CheckSkipTurnStateEvent;
    public static event EventHandler<DisplayTurnEventArgs> DisplayTurnEvent;

    public class SkipTurnPressedEventArgs : EventArgs { }
    public class CheckSkipTurnStateEventArgs : EventArgs { }
    public class DisplayTurnEventArgs : EventArgs { }

    public static void InvokeSkipTurnPressed(GameObject sender)
    {
        SkipTurnPressedEvent?.Invoke(sender, new SkipTurnPressedEventArgs { });
    }

    public static void InvokeCheckSkipTurnState(GameObject sender)
    {
        CheckSkipTurnStateEvent?.Invoke(sender, new CheckSkipTurnStateEventArgs { });
    }

    public static void InvokeDisplayTurn(GameObject sender)
    {
        DisplayTurnEvent?.Invoke(sender, new DisplayTurnEventArgs { });
    }

    #endregion

    private TeamTurn currentTurn = TeamTurn.BLUE;
    public TeamTurn CurrentTurn { get { return currentTurn; } }

    public void ChangeTurn()
    {
        currentTurn = currentTurn == TeamTurn.BLUE ? TeamTurn.RED : TeamTurn.BLUE;
        InvokeCheckSkipTurnState(gameObject);
        InvokeDisplayTurn(gameObject);
    }

    public void StartTurn()
    {
        currentTurn = TeamTurn.BLUE;
        

        if (GameManager.Instance.CurrentGameMode == GameMode.FRIEND)
        {
            ChangeTurnEvent += Events_ChangeTurnFriend;
        }
        else if (GameManager.Instance.CurrentGameMode == GameMode.AI)
        {
            ChangeTurnEvent += Events_ChangeTurnAI;
        }
    }

    public void ResetTurn()
    {
        currentTurn = TeamTurn.BLUE;

        if (GameManager.Instance.CurrentGameMode == GameMode.FRIEND)
        {
            ChangeTurnEvent -= Events_ChangeTurnFriend;
        }
        else if (GameManager.Instance.CurrentGameMode == GameMode.AI)
        {
            ChangeTurnEvent -= Events_ChangeTurnAI;
        }
    }
}
