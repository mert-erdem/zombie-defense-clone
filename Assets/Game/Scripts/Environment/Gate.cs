using UnityEngine;

namespace Game.Scripts.Environment
{
    public class Gate : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject relatedPlatform;
        [SerializeField] private int moneyToPass = 5;

        public int InteractionCount { get; set; }
        public bool CanInteract { get; set; }

        private void Start()
        {
            InteractionCount = moneyToPass;
        }

        public void Interact()
        {
            moneyToPass--;
            // notify money manager
            // update world space money UI
            if (moneyToPass == 0)
            {
                // unlock new platform part
                relatedPlatform.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
