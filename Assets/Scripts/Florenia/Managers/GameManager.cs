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
            Debug.Log("Building intial dungeon");
            DungeonManager.Instance.BuildSmall();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        
            if (Input.GetKeyDown(KeyCode.R)) {
                DungeonManager.Instance.CreateNewLevel();
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

    }
}
