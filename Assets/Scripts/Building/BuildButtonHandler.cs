using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public BuildingSO associatedBuilding;
    private GameObject buildingInstance;

    [Header("UI COMPONENTS")]
    public Image detectionFeedbackImage;
    public Image darkerImage;
    public Image disponibilityFeedbackImage;
    public Image buildingIconImage;
    Color color = Color.white;

    //ADD THE UI FEEDBACK BY CHECKING IF RESSOURCES ARE ENOUGH TO BUILD THE BUILDING USED BY THIS BUTTON

    private void Start()
    {
        buildingIconImage.sprite = associatedBuilding.buildingIcon;

        color.a = 0f;
        detectionFeedbackImage.color = color;
    }

    public void InstantiateBuildingAtCursorPos()
    {
        buildingInstance = Instantiate(associatedBuilding.buildingPrefab, UtilityClass.GetCursorClickedPosition(LayerMask.NameToLayer("Ground")), associatedBuilding.buildingPrefab.transform.rotation);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        color.a = 0.15f;
        detectionFeedbackImage.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        color.a = 0f;
        detectionFeedbackImage.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click on this button");

        CursorHandler.Instance.SetCursorAppearance(CursorType.Building);
        InstantiateBuildingAtCursorPos();

        DraggingBuilding.Instance.SetBuildingPrefab(buildingInstance);
    }
}