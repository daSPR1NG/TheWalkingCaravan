using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Play,
    Pause,
}

public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.Play;

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

    void Start()
    {
        ResumeTheGame();
    }

    void Update()
    {
        if (UtilityClass.IsKeyPressed(KeyCode.Escape))
        {
            PauseTheGame();
            SetTimeScale(0);
        }
    }

    public void PauseTheGame()
    {
        gameState = GameState.Pause;
    }

    public void ResumeTheGame()
    {
        gameState = GameState.Play;
    }

    public void QuitTheGame()
    {
        UtilityClass.DebugMessage("QUIT THE GAME !");
        Application.Quit();
    }

    public void SetTimeScale(float newValue)
    {
        Time.timeScale = newValue;
    }
}
