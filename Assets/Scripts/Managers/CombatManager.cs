using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [Space]
    [Header("COMBAT CROSSFADE")]
    [SerializeField] private GameObject crossFadeObject;
    Image ImageRef => crossFadeObject.GetComponentInChildren<Image>();
    [SerializeField] private float fadeDuration = .75f;
    [SerializeField] private float fadeTransitionDuration = .75f;

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
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    private void OnEnable()
    {
        GameManager.OnEnteringCombat += EnterCombat;
        GameManager.OnExitingCombat += ExitCombat;
    }

    private void OnDisable()
    {
        GameManager.OnEnteringCombat -= EnterCombat;
        GameManager.OnExitingCombat -= ExitCombat;
    }

    void Start() => DesactivateCrossFadeObject();

    #region Combat transition
    private void EnterCombat()
    {
        StartCoroutine(FadeInCoroutine(ImageRef, 1, 0, fadeDuration, _SceneManager.Instance.CombatSceneName));
        //Save some player informations such as position...
    }

    private void ExitCombat()
    {
        StartCoroutine(FadeInCoroutine(ImageRef, 1, 0, fadeDuration, _SceneManager.Instance.DefaultSceneName));
        //Restore some player informations such as position...
    }

    #region Summary
    /// <summary>
    /// Execute a fade transition between the current scene and the wanted scene we want to load.
    /// </summary>
    /// <param name="imageRef"></param>
    /// <param name="midAlphaValue"></param>
    /// <param name="endAlphaValue"></param>
    /// <param name="fadeDuration"></param>
    /// <param name="sceneToLoad"></param>
    /// <returns></returns>
    #endregion
    IEnumerator FadeInCoroutine(Image imageRef, float midAlphaValue, float endAlphaValue, float fadeDuration, string sceneToLoad)
    {
        yield return new WaitUntil(() => GameManager.Instance.GameIsPlaying());

        ActivateCrossFadeObject();

        yield return imageRef.DOFade(midAlphaValue, fadeDuration).WaitForCompletion();

        _SceneManager.Instance.LoadASceneByName(sceneToLoad);

        yield return new WaitUntil(() => _SceneManager.Instance.IsThisSceneLoaded(_SceneManager.Instance.CombatSceneName));

        yield return new WaitForSeconds(fadeTransitionDuration);
        yield return imageRef.DOFade(endAlphaValue, fadeDuration).WaitForCompletion();

        DesactivateCrossFadeObject();
    }

    private void ActivateCrossFadeObject()
    {
        crossFadeObject.SetActive(true);
    }

    private void DesactivateCrossFadeObject()
    {
        crossFadeObject.SetActive(false);
    }
    #endregion
}