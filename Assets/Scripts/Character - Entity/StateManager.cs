using Khynan_Survival;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    #region Character States
    private BasicState currentState;
    public Character_IdleState IdleState = new Character_IdleState();
    public Character_MovingState MovingState = new Character_MovingState();
    public Character_InteractionState InteractionState = new Character_InteractionState();
    [HideInInspector] public bool CharacterIsMoving = false;

    public BasicState CurrentState { get => currentState; set => currentState = value; }
    #endregion

    protected virtual void Update()
    {
        CurrentState.ProcessState(this);
    }

    protected void SetInitialStateAtStart(BasicState baseState)
    {
        CurrentState = baseState;
        CurrentState.EnterState(this);
    }

    public void SwitchState(BasicState newState)
    {
        CurrentState.ExitState(this);

        CurrentState = newState;
        newState.EnterState(this);
    }
}
