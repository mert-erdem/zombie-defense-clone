using UnityEngine;

namespace Game.Scripts.Environment
{
    public class InteractionArea : MonoBehaviour
    {
        [SerializeField] 
        [Tooltip("The root object that has a component which implements IInteractable interface")] 
        private Transform root;
        [SerializeField] private int maxInteractions = 1;
        [SerializeField] private float interactionDeltaTime = 0.5f;

        private IInteractable interactable;
        private int currentInteractionCount = 0;
        private float lastInteractionTime = Mathf.Infinity;

        private void Start()
        {
            interactable = root.GetComponent<IInteractable>();
        }

        private void HandleWithInteraction()
        {
            if (currentInteractionCount >= maxInteractions) return;

            lastInteractionTime += Time.deltaTime;

            if (!(lastInteractionTime >= interactionDeltaTime)) return;
        
            interactable.Interact();
            currentInteractionCount++;
            lastInteractionTime = 0;
        }
    
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                HandleWithInteraction();
            }
        }
    }
}
