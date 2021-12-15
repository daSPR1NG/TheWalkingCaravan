using TMPro;
using UnityEngine;

public class GeneralCombatInfosHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI characterNameText;
    private float timeValue = 0.0f;
    private TeamManager.Unit currentPlayingUnitRef;

    private void OnEnable()
    {
        TurnOrderManager.OnTurnBegun += UpdateCurrentPlayingCharacterName;
        TurnOrderManager.OnTurnEnded += UpdateCurrentPlayingCharacterName;
    }

    private void OnDisable()
    {
        TurnOrderManager.OnTurnBegun -= UpdateCurrentPlayingCharacterName;
        TurnOrderManager.OnTurnEnded += UpdateCurrentPlayingCharacterName;
    }

    void Start() => UpdateCombatTimer();

    void LateUpdate() => UpdateCombatTimer();

    void UpdateCombatTimer()
    {
        if (!GameManager.Instance.GameIsPlaying()) return;

        timeValue += Time.deltaTime;

        string minutes = Mathf.Floor(timeValue / 60).ToString("0");
        string seconds = Mathf.Floor(timeValue % 60).ToString("00");

        timerText.SetText(minutes + " : " + seconds);
    }

    void UpdateCurrentPlayingCharacterName()
    {
        currentPlayingUnitRef = TurnOrderManager.Instance.GetCurrentlyPlayingUnit();

        characterNameText.SetText(" - Tour de " + currentPlayingUnitRef.name + " .");
    }
}