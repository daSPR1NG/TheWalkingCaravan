using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Play,
    Pause
}

public class GameManager : MonoBehaviour
{
    public delegate void GameStateHandler();
    public static event GameStateHandler OnGameStateChanged;

    public GameState gameState = GameState.Play;

    #region Singleton
    public static GameManager Instance;

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
        ResumeGame();
    }

    void Update()
    {
        if (UtilityClass.IsKeyPressed(KeyCode.Escape))
        {
            ToggleGameState();
        }
    }

    #region Game states methods
    private void ToggleGameState()
    {
        if (GameIsPlaying())
        {
            PauseTheGame();
        }
        else
        {
            ResumeGame();
        }

        OnGameStateChanged?.Invoke();
    }

    public void PauseTheGame()
    {
        gameState = GameState.Pause;
        SetTimeScaleTo(0);
    }

    public void ResumeGame()
    {
        gameState = GameState.Play;
        SetTimeScaleTo(1);
    }

    public void QuitTheGame()
    {
        UtilityClass.DebugMessage("QUIT THE GAME !");
        Application.Quit();
    }

    public bool GameIsPaused()
    {
        if (gameState == GameState.Pause)
        {
            return true;
        }

        return false;
    }

    public bool GameIsPlaying()
    {
        if (gameState == GameState.Play)
        {
            return true;
        }

        return false;
    }
    #endregion

    public void SetTimeScaleTo(float newValue)
    {
        Time.timeScale = newValue;
    }
}
