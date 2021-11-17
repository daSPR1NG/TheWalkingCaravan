using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuComponent;

    #region Singleton
    public static UIManager Instance;

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

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += TogglePauseMenu;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= TogglePauseMenu;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void TogglePauseMenu()
    {
        if (!IsThisComponentDisplayed(pauseMenuComponent))
        {
            DisplayThisUIComponent(pauseMenuComponent);
            return;
        }

        HideThisUIComponent(pauseMenuComponent);
    }

    private void DisplayThisUIComponent(GameObject component)
    {
        if (component is not null)
        {
            component.SetActive(true);
        }
    }

    private void HideThisUIComponent(GameObject component)
    {
        if (component is not null)
        {
            component.SetActive(false);
        }
    }

    private bool IsThisComponentDisplayed(GameObject component)
    {
        if (component.activeInHierarchy)
        {
            return true;
        }

        return false;
    }
}