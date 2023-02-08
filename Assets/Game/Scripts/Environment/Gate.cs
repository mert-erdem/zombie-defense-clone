using System;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Environment
{
    public class Gate : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject relatedPlatform;
        [SerializeField] private int moneyToPass = 5;
        [Header("UI")] 
        [SerializeField] private TextMeshPro textMoneyToPass;

        public bool CanInteract { get; set; }

        private void Start()
        {
            textMoneyToPass.text = moneyToPass.ToString();
        }

        public void Interact()
        {
            moneyToPass--;
            // notify money manager
            // update world space money UI
            textMoneyToPass.text = moneyToPass.ToString();
            if (moneyToPass != 0) return;
            // unlock new platform part
            AreaManager.Instance.ActivatePlatform(relatedPlatform, this);
        }
    }
}
