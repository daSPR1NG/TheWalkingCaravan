using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class CollectableRessource : MonoBehaviour, IInteractive, IDetectable
{
    [Header("INTERACTION SETTINGS")]
    public Transform interactingObject;
    public float minimumDistanceToInteract = 1.25f;

    [Header("RESSOURCE SETTINGS")]
    public RessourceType ressourceType = RessourceType.Unassigned;
    public float ressourceAmount = 1000f;
    public float collectingTimerValue = 5f;

    #region Components
    private Outline OutlineComponent => GetComponent<Outline>();
    #endregion

    public virtual void ExitInteraction(Transform _interactingObject)
    {
        StopCoroutine(InteractionCoroutine());

        interactingObject = null;
    }

    public virtual void Interaction(Transform _interactingObject)
    {
        interactingObject = _interactingObject;

        if (ressourceType != RessourceType.Unassigned && ressourceAmount != 0) StartCoroutine(InteractionCoroutine());

        UtilityClass.DebugMessage("PIPOUPIPOU");
    }

    IEnumerator InteractionCoroutine()
    {
        yield return new WaitForSeconds(collectingTimerValue / 2);
        //Change the sprite to a damaged one to see the advancement.

        yield return new WaitForSeconds(collectingTimerValue);

        if (!interactingObject) yield break;

        interactingObject.GetComponent<RessourcesHandler>().GetThisRessource(ressourceType).AddToCurrentValue(ressourceAmount);

        //Call Destruction Animation or change the sprite.
        //Play the SFX.
    }

    #region Detection
    public void OnMouseEnter()
    {
        IDetectable.IDetectableExtension.SetCursorAppearanceOnDetection(CursorType.Ressource, OutlineComponent, true, transform.name + " has been detected.");
    }

    public void OnMouseExit()
    {
        IDetectable.IDetectableExtension.SetCursorAppearanceOnDetection(CursorType.Default, OutlineComponent, false, transform.name + " is no longer detected.");
    }
    #endregion
}