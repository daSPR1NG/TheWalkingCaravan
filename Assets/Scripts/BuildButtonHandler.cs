using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButtonHandler : MonoBehaviour
{
    public Building associatedBuilding;

    [Header("UI COMPONENTS")]
    public Image detectionFeedbackImage;
    public Image darkerImage;
    public Image disponibilityFeedbackImage;
    public Image buildingIconImage;
    Color color;

    private void Start()
    {
        buildingIconImage.sprite = associatedBuilding.buildingIcon;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        CursorHandler.Instance.SetCursorAppearance(CursorType.Building);
    }

    private void OnMouseEnter()
    {
        color.a = 255;
        detectionFeedbackImage.color = color;
    }

    private void OnMouseExit()
    {
        color.a = 128;
        detectionFeedbackImage.color = color;
    }
}