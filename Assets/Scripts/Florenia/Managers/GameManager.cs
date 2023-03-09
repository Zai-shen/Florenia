using System;
using Florenia.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Florenia.Managers
{
    public class GameManager : UnitySingleton<GameManager>
    {
        // public PauseMenu PauseMenu;
        public Action ResetPause;
        private bool _gamePaused;

        private int dungeonLevel = 1;

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
            StartDungeonLevel(1);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        
            if (Input.GetKeyDown(KeyCode.R)) {
                StartDungeonLevel(0);
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

        [ContextMenu("Restart Game")]
        public void RestartGame()
        {
            dungeonLevel = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        [ContextMenu("Restart Dungeon Full")]
        public void RestartDungeonFull()
        {
            StartDungeonLevel(1);
        }

        public void RestartDungeonLevel()
        {
            StartDungeonLevel(dungeonLevel);
        }

        [ContextMenu("Next Dungeon")]
        public void NextDungeon()
        {
            dungeonLevel ++;
            StartDungeonLevel(dungeonLevel);
        }
        
        private void StartDungeonLevel(int level){
            PlayerManager.Instance.InGamePlayer?.ResetInventory();
            
            switch(level){
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
                default:
                {
                    DungeonManager.Instance.BuildBig();
                    break;
                }
            }
        }
    }
}
