using UnityEngine;
using UnityEngine.AI;

namespace Khynan_Survival
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class Character_MovementHandler : StateManager
    {
        [Header("MOVEMENT SETTINGS")]
        public float MovementSpeed = 5f;
        public float RotationSpeed = 30f;
        [HideInInspector] public Vector3 DirectionToMove = Vector3.zero;

        #region Components
        [Header("COMPONENTS")]
        public Animator CharacterAnimator;
        public NavMeshAgent NavMeshAgent => GetComponent<NavMeshAgent>();
        public Rigidbody Rb => GetComponent<Rigidbody>();
        #endregion

        private void Start()
        {
            SetCharacterSpeed(10f);
            SetInitialStateAtStart(IdleState);
        }

        void FixedUpdate() => ProcessMovement();

        private void ProcessMovement()
        {
            DirectionToMove = Vector3.zero;

            //Vertical Axis
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKey(KeyCode.Z))
            {
                DirectionToMove.z = 1;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
            {
                DirectionToMove.z = -1;
            }
            //Horizontal Axis
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKey(KeyCode.Q))
            {
                DirectionToMove.x = -1;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
            {
                DirectionToMove.x = 1;
            }

            //Need a current position, the actual position of the object,
            //Then you add a direction to move towards to multiplied by a speed represented by the movement speed, this one multiplied by time.-fixed-deltaTime
            Rb.position += MovementSpeed * Time.fixedDeltaTime * (UtilityClass.GetMainCameraForwardDirection(0) * DirectionToMove.z + UtilityClass.GetMainCameraRightDirection(0) * DirectionToMove.x).normalized;

            if (DirectionToMove != Vector3.zero)
            {
                InteractionHandler interactionHandler = GetComponent<InteractionHandler>();

                if (interactionHandler.TargetDetected != null)
                {
                    interactionHandler.ResetInteractingState(); 
                }
                    

                UtilityClass.ResetAgentDestination(NavMeshAgent);

                if (CurrentState != MovingState) SwitchState(MovingState);
            }
        }

        public void SetCharacterSpeed(float newSpeed)
        {
            MovementSpeed = newSpeed;
            NavMeshAgent.speed = newSpeed;
        }
    }
}