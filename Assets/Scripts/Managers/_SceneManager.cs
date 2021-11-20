using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    [SerializeField] private string combatScene;
    public string CombatScene { get => combatScene; }

    #region Singleton
    public static _SceneManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public void LoadASceneByName(string sceneName, LoadSceneMode loadSceneMode)
    {
        SceneManager.LoadScene(sceneName, loadSceneMode);
    }

    public bool IsThisSceneLoaded(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == sceneName)
        {
            return true;
        }

        return false;
    }
}
