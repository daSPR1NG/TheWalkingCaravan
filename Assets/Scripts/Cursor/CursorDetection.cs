using UnityEngine;

public class CursorDetection : MonoBehaviour
{
    [Header("DETECTION PARAMETERS")]
    public LayerMask EntityLayer;
    public Transform TargetDetected;
    public bool HasATarget => TargetDetected != null;
    IInteractive interactiveEntity;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(UtilityClass.GetMainCamera().ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, EntityLayer))
            {
                bool entityIsInteractive = hit.transform.GetComponent<IInteractive>() != null;

                if (entityIsInteractive)
                {
                    UtilityClass.DebugMessage("Entity detected " + hit.transform.name);

                    TargetDetected = hit.transform;
                    interactiveEntity = TargetDetected.GetComponent<IInteractive>();
                }
            }
            //Click on the ground
            else
            {
                ResetInteractionDatas();
            }
        }
    }

    public void ResetInteractionDatas()
    {
        if (TargetDetected)
        {
            interactiveEntity.ExitInteraction(transform);
            TargetDetected = null;
        }
    }
}
