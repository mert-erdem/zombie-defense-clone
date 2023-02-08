using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Core
{
    public class GameManager : Singleton<GameManager>
    {
        public static UnityAction ActionGameStart, ActionLevelStart, ActionGameOver, ActionLevelPassed;

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
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentLevelIndex);
        }
    }
}