using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCombatState
{
    OutOfCombat,
    InCombat,
}

public class CombatManager : MonoBehaviour
{
    public PlayerCombatState playerGameState = PlayerCombatState.OutOfCombat;

    #region Singleton
    public static CombatManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region Combat game states methods
    public void SetOutOfCombatState()
    {
        playerGameState = PlayerCombatState.OutOfCombat;
    }

    public void SetCombatState()
    {
        playerGameState = PlayerCombatState.InCombat;
    }

    public bool IsOutOfCombat()
    {
        if (playerGameState == PlayerCombatState.OutOfCombat)
        {
            return true;
        }

        return false;
    }

    public bool IsInCombat()
    {
        if (playerGameState == PlayerCombatState.InCombat)
        {
            return true;
        }

        return false;
    }
    #endregion

    public void EnterCombat()
    {
        SetCombatState();
    }

    public void ExitCombat()
    {
       SetOutOfCombatState();
    }
}