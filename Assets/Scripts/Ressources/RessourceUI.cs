using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RessourceUI : MonoBehaviour
{
    public RessourceType ressourceType;
    public TextMeshProUGUI valueText;
    public Image ressourceIconImage;

    public void SetThisUI(RessourceType ressourceType, float value, Sprite sprite)
    {
        this.ressourceType = ressourceType;
        valueText.text = value.ToString("0");
        ressourceIconImage.sprite = sprite;
    }

    public void UpdateRessourceValue(float newValue)
    {
        valueText.text = newValue.ToString("0");
    }
}
