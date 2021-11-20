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

    void Start()
    {
        crossFadeObject.SetActive(false);
    }

    #region Combat
    private void EnterCombat()
    {
        StartCoroutine(FadeInCoroutine(ImageRef, 1, 0, fadeDuration, true));
    }

    private void ExitCombat()
    {
        StartCoroutine(FadeInCoroutine(ImageRef, 1, 0, fadeDuration, false));
    }

    IEnumerator FadeInCoroutine(Image imageRef, float midAlphaValue, float endAlphaValue, float fadeDuration, bool displayContent)
    {
        yield return new WaitUntil(() => GameManager.Instance.GameIsPlaying());

        crossFadeObject.SetActive(true);

        yield return imageRef.DOFade(midAlphaValue, fadeDuration).WaitForCompletion();

        _SceneManager.Instance.LoadASceneByName(_SceneManager.Instance.CombatScene, UnityEngine.SceneManagement.LoadSceneMode.Single);

        yield return new WaitUntil(() => _SceneManager.Instance.IsThisSceneLoaded(_SceneManager.Instance.CombatScene));

        yield return new WaitForSeconds(fadeTransitionDuration);
        yield return imageRef.DOFade(endAlphaValue, fadeDuration).WaitForCompletion();

        crossFadeObject.SetActive(false);
    }
    #endregion
}