using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Core
{
    public class GameManager : Singleton<GameManager>
    {
        public static UnityAction ActionGameStart, ActionLevelStart, ActionGameOver, ActionGameOverPost, ActionLevelPassed;

        private void Start()
        {
            ActionGameOver += RestartLevel;
        }

        public void LoadNextLevel()
        {
            var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if(activeSceneIndex == SceneManager.sceneCountInBuildSettings - 1)// last scene
            {
                SceneManager.LoadScene(activeSceneIndex);
                return;
            }

            int nextLevel = activeSceneIndex + 1;

            PlayerPrefs.SetInt("LEVEL", nextLevel);
            SceneManager.LoadScene(nextLevel);
        }

        public void RestartLevel()
        {
            StartCoroutine(RestartLevelRoutine());
        }
        private IEnumerator RestartLevelRoutine()
        {
            yield return new WaitForSeconds(1.5f);
            
            ActionGameOverPost?.Invoke();

            yield return new WaitForSeconds(1.5f); 
            
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentLevelIndex);
        }

        private void OnDestroy()
        {
            ActionGameOver -= RestartLevel;
        }
    }
}