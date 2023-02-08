using System;
using System.Collections.Generic;
using Game.Core;
using Game.Scripts.Core;
using UnityEngine;

namespace Game.Scripts.Environment
{
    /// <summary>
    /// Main class that saves fields using SaveSystem.cs
    /// </summary>
    public class AreaManager : Singleton<AreaManager>
    {
        [SerializeField] private int areaLevelCount = 1;
        [SerializeField] private List<GameObject> platforms;
        [SerializeField] private List<Gate> gates;

        public int CurrentLevel { get; private set; }
        public bool[] PlatformActives { get; private set; }
        public bool[] GateActives { get; private set; }

        private void Start()
        {
            GameManager.ActionLevelPassed += GameManager_ActionLevelPassed;
        
            CurrentLevel = 1;
            PlatformActives = new bool[platforms.Count];
            GateActives = new bool[gates.Count];

            for (int i = 0; i < GateActives.Length; i++)
            {
                GateActives[i] = true;
            }

            // first platform always active
            PlatformActives[0] = true;
            //SaveSystem.ResetArea();
            ActivateSavedPlatforms();
        }

        public void ActivatePlatform(GameObject platform, Gate relatedGate)
        {
            if (!platforms.Contains(platform)) return;
        
            platform.SetActive(true);
            int unlockedPlatformIndex = platforms.IndexOf(platform);
            PlatformActives[unlockedPlatformIndex] = true;
            int relatedGateIndex = gates.IndexOf(relatedGate);
            GateActives[relatedGateIndex] = false;
            relatedGate.gameObject.SetActive(false);
        }

        private void SaveArea()
        {
            SaveSystem.SaveArea(this);
        }

        private void LoadSavedArea()
        {
            var areaData = SaveSystem.LoadArea();
        
            if (areaData == null) return;
        
            CurrentLevel = areaData.Level;
            PlatformActives = areaData.PlatformActives;
            GateActives = areaData.GateActives;
        }

        private void ActivateSavedPlatforms()
        {
            LoadSavedArea();
        
            // handle with platforms
            for (int i = 0; i < PlatformActives.Length; i++)
            {
                platforms[i].SetActive(PlatformActives[i]);
            }
        
            // handle with gates
            for (int i = 0; i < GateActives.Length; i++)
            {
                // to prevent activate non explored platforms' gates
                if (!GateActives[i])
                {
                    gates[i].gameObject.SetActive(false);
                }
            }
        }

        private void GameManager_ActionLevelPassed()
        {
            CurrentLevel++;
        }

        private void OnDestroy()
        {
            GameManager.ActionLevelPassed -= GameManager_ActionLevelPassed;
            SaveArea();
        }
    }
}
