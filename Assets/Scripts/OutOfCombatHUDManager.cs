using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfCombatHUDManager : MonoBehaviour
{
    [SerializeField] private GameObject contentObject;

    private void OnEnable()
    {
        CombatHUDManager.OnTransition += ToggleThisUI;
    }

    private void OnDisable()
    {
        CombatHUDManager.OnTransition -= ToggleThisUI;
    }

    private void ToggleThisUI()
    {
        if (GameManager.Instance.IsInCombat() && contentObject.activeInHierarchy)
        {
            UIManager.Instance.HideThisUIComponent(contentObject);
            return;
        }

        UIManager.Instance.DisplayThisUIComponent(contentObject);
    }
}
