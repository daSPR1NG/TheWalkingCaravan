using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectingUIProcess : MonoBehaviour
{
    public Transform content;

    [Space] [Header("UI COMPONENTS")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI actionText;
    public Image filledImage;

    private float timer;
    private float maxTimer;

    private void OnEnable()
    {
        InteractionHandler.OnInteraction += SetUIDatas;
        InteractionHandler.OnEndOfInteraction += ResetUIDatas;
    }

    private void OnDisable()
    {
        InteractionHandler.OnInteraction -= SetUIDatas;
        InteractionHandler.OnEndOfInteraction -= ResetUIDatas;
    }

    private void Update()
    {
        if (content.gameObject.activeInHierarchy) ProcessTimer();
    }

    public void SetUIDatas(float startingTimer, string action)
    {
        content.gameObject.SetActive(true);

        maxTimer = startingTimer;
        timer = maxTimer;

        timerText.text = timer.ToString("0");

        filledImage.fillAmount = startingTimer / maxTimer;

        actionText.text = action;
    }

    private void SetUIDatasAtRuntime(float timerValue)
    {
        if (timerValue > 1) timerText.text = timerValue.ToString("0");

        if (timerValue <= 1) timerText.text = timerValue.ToString("0.0");

        filledImage.fillAmount = timerValue / maxTimer;
    }

    private void ResetUIDatas()
    {
        maxTimer = 0;
        timer = 0;

        content.gameObject.SetActive(false);
    }

    private void ProcessTimer()
    {
        timer -= Time.deltaTime;

        SetUIDatasAtRuntime(timer);

        if (timer <= 0) content.gameObject.SetActive(false);
    }
}