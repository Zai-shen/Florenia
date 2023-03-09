using System;
using Florenia.Utility;
using UnityEngine;

namespace Florenia.Managers
{
    public class GameManager : UnitySingleton<GameManager>
    {
        // public PauseMenu PauseMenu;
        public Action ResetPause;
        private bool _gamePaused;

        private int deathCount = 1;

        private void OnEnable()
        {
            ResetPause += ResetPauseState;
        }

        private void OnDisable()
        {
            ResetPause -= ResetPauseState;
        }

        private void Start()
        {
            AddDeath(0);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        
            if (Input.GetKeyDown(KeyCode.R)) {
                AddDeath(0);
            }
        }

        private void ResetPauseState()
        {
            _gamePaused = false;
        }
    
        private void PauseGame()
        {
            Debug.Log("Pausing");
        
            if (!_gamePaused)
            {
                _gamePaused = true;
                // PauseMenu?.Pause();
            }
            else
            {
                _gamePaused = false;
                // PauseMenu?.Continue();
            }
        }

        [ContextMenu("AddDeath0")]
        private void AddDeath0()
        {
            AddDeath(0);
        }

        [ContextMenu("AddDeath1")]
        private void AddDeath1()
        {
            AddDeath(1);
        }
        
        public void AddDeath(int death){
            PlayerManager.Instance.InGamePlayer?.ResetInventory();
            
            deathCount += death;
            switch(deathCount){
                case 1:
                {
                    DungeonManager.Instance.BuildSmall();
                    break;
                }
                case 2:
                {
                    DungeonManager.Instance.BuildMedium();
                    break;
                }                
                case 3:
                {
                    DungeonManager.Instance.BuildBig();
                    break;
                }
                default:
                {
                    DungeonManager.Instance.BuildSmall();
                    break;
                }
            }
        }
    }
}
