using Khynan_Survival;
using UnityEngine;
using UnityEngine.AI;

public class InteractionHandler : MonoBehaviour
{
    [Header("DETECTION PARAMETERS")]
    public LayerMask EntityLayer;
    public Transform TargetDetected;
    public bool isInteracting = false; // Debug
    public bool HasATarget => TargetDetected != null;
    CollectableRessource collectableRessource;
    float agentStartingStoppingDist;

    #region Components
    NavMeshAgent NavMeshAgent => GetComponent<NavMeshAgent>();
    #endregion

    private void Start()
    {
        agentStartingStoppingDist = NavMeshAgent.stoppingDistance;
    }

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

                    collectableRessource = TargetDetected.GetComponent<CollectableRessource>();
                    collectableRessource.interactingObject = transform;

                    TryToInteract();
                }
            }
            //Click on the ground
            else
            {
                ResetInteractionDatas();
            }
        }

        CheckRemainingDistanceWithTarget();
    }

    void TryToInteract()
    {
        float distanceBetweenObjects = Vector3.Distance(transform.position, TargetDetected.position);

        if (distanceBetweenObjects > collectableRessource.minimumDistanceToInteract)
        {
            agentStartingStoppingDist = NavMeshAgent.stoppingDistance;
            UtilityClass.SetAgentStoppingDistance(NavMeshAgent, collectableRessource.minimumDistanceToInteract * 2);

            UtilityClass.SetAgentDestination(NavMeshAgent, TargetDetected.position);
        }
    }

    void CheckRemainingDistanceWithTarget()
    {
        if (!NavMeshAgent.hasPath) return;

        float distanceBetweenObjects = Vector3.Distance(transform.position, TargetDetected.position);

        if (distanceBetweenObjects <= collectableRessource.minimumDistanceToInteract * 2 && !isInteracting)
        {
            NavMeshAgent.stoppingDistance = agentStartingStoppingDist;

            collectableRessource.Interaction(transform);

            //Call UI Event and set the informations - Display

            isInteracting = true;
        }
    }

    public void ResetInteractionDatas()
    {
        if (TargetDetected)
        {
            collectableRessource.ExitInteraction(transform);
            TargetDetected = null;
            isInteracting = false;

            //Call UI Event and set the informations - Hide
        }
    }
}