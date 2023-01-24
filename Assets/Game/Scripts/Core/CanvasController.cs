using Game.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.UI
{
    public class CanvasController : Singleton<CanvasController>
    {
        [SerializeField] private GameObject menuButtons;
        [SerializeField] private TextMeshProUGUI textMoney, textWeapon;


        void Start()
        {
            GameManager.ActionLevelStart += SetInGameUI;
            GameManager.ActionGameOver += SetGameOverUI;
            GameManager.ActionLevelPassed += SetLevelPassedUI;
        }

        private void SetInGameUI()
        {
            menuButtons.SetActive(false);
        }

        private void SetGameOverUI()
        {
            
        }

        private void SetLevelPassedUI()
        {
            
        }

        #region UI Buttons' Methods
        public void ButtonStartPressed()
        {
            GameManager.ActionLevelStart?.Invoke();
        }

        public void ButtonNextLevelPressed()
        {
            GameManager.Instance.LoadNextLevel();
        }

        public void ButtonRetryPressed()
        {
            GameManager.Instance.RestartLevel();
        }
        #endregion

        public void SetTextMoney(int amount)
        {
            textMoney.text = amount.ToString();
        }

        public void SetWeaponText(string type)
        {
            textWeapon.text = type;
        }

        private void OnDestroy()
        {
            GameManager.ActionLevelStart -= SetInGameUI;
            GameManager.ActionGameOver -= SetGameOverUI;
            GameManager.ActionLevelPassed -= SetLevelPassedUI;
        }
    }
}