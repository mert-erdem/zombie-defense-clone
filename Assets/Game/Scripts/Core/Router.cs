using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Core
{
    public class Router : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(LoadLastLevel());
        }

        private IEnumerator LoadLastLevel()
        {
            yield return new WaitForSeconds(1f);

            int lastLevelIndex = PlayerPrefs.GetInt("AREA", 1);
            SceneManager.LoadScene(lastLevelIndex);
        }
    }
}