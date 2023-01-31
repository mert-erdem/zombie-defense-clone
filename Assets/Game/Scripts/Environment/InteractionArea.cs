using UnityEngine;

namespace Game.Scripts.Environment
{
    public class InteractionArea : MonoBehaviour
    {
        [SerializeField] 
        [Tooltip("The root object that has a component which implements IInteractable interface")] 
        private Transform root;
        [SerializeField] private float interactionDeltaTime = 0.5f;

        private IInteractable interactable;
        private float lastInteractionTime = Mathf.Infinity;

        private void Start()
        {
            interactable = root.GetComponent<IInteractable>();
        }

        private void HandleWithInteraction()
        {
            lastInteractionTime += Time.deltaTime;

            if (!(lastInteractionTime >= interactionDeltaTime)) return;
        
            interactable.Interact();
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
