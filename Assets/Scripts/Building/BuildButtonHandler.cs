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

    private void OnEnable()
    {
        SubscribeRessourceEvent();
    }

    private void OnDisable()
    {
        UnsubscribeRessourceEvent();
    }

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

    private void CheckIfTheBuildingCanBeBuilt(RessourceType neededRessourceType)
    {
        if (associatedBuilding.neededRessourcesToBuild.Count == 0) return;

        for (int i = 0; i < RessourcesHandler.Instance.characterRessources.Count; i++)
        {
            if (associatedBuilding.neededRessourcesToBuild [ i ].ressourceType == RessourcesHandler.Instance.characterRessources[ i ].ressourceType)
            {
                if (associatedBuilding.neededRessourcesToBuild [ i ].neededRessourceValue >= RessourcesHandler.Instance.characterRessources [ i ].CurrentValue)
                {
                    Debug.Log("Can be built");
                }
                else
                {
                    Debug.Log("Cannot be built");
                }
            }
        }
    }

    #region Event
    public void SubscribeRessourceEvent()
    {
        foreach (Ressource thisRessource in RessourcesHandler.Instance.characterRessources)
        {
            thisRessource.OnRessourceValueChanged += CheckIfTheBuildingCanBeBuilt;
        }
    }

    public void UnsubscribeRessourceEvent()
    {
        foreach (Ressource thisRessource in RessourcesHandler.Instance.characterRessources)
        {
            thisRessource.OnRessourceValueChanged -= CheckIfTheBuildingCanBeBuilt;
        }
    }
    #endregion

    #region UI Event - Pointer
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
    #endregion
}