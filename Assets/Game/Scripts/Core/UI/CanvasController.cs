using Game.Scripts.Environment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Core.UI
{
    public class CanvasController : Singleton<CanvasController>
    {
        [SerializeField] private GameObject menuButtons;
        [SerializeField] private TextMeshProUGUI textMoney, textWeapon, textLevel;
        [SerializeField] private GameObject levelProgressBar;
        [SerializeField] private Image levelProgressBarForeGround;


        void Start()
        {
            GameManager.ActionLevelStart += SetInGameUI;
            GameManager.ActionGameOver += SetGameOverUI;
            GameManager.ActionLevelPassed += SetLevelPassedUI;
        }

        private void SetInGameUI()
        {
            menuButtons.SetActive(false);
            levelProgressBar.SetActive(true);
            SetLevelText();
            SetLevelProgressBar(0f);
        }

        private void SetGameOverUI()
        {
            
        }

        private void SetLevelPassedUI()
        {
            menuButtons.SetActive(true);
            SetLevelText();
            levelProgressBar.SetActive(false);
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

        public void SetLevelText()
        {
            textLevel.text = "level " + AreaManager.Instance.CurrentLevel;
        }

        public void SetLevelProgressBar(float amount)
        {
            levelProgressBarForeGround.fillAmount = amount;
        }

        private void OnDestroy()
        {
            GameManager.ActionLevelStart -= SetInGameUI;
            GameManager.ActionGameOver -= SetGameOverUI;
            GameManager.ActionLevelPassed -= SetLevelPassedUI;
        }
    }
}