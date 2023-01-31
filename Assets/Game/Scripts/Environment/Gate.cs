using UnityEngine;

namespace Game.Scripts.Environment
{
    public class Gate : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject relatedPlatform;
        [SerializeField] private int moneyToPass = 5;

        public bool CanInteract { get; set; }

        public void Interact()
        {
            moneyToPass--;
            // notify money manager
            // update world space money UI
            if (moneyToPass != 0) return;
            // unlock new platform part
            AreaManager.Instance.ActivatePlatform(relatedPlatform, this);
        }
    }
}
