using Cinemachine;
using Florenia.Utility;
using Unity.Mathematics;
using UnityEngine;

namespace Florenia.Managers
{
    public class PlayerManager : UnitySingleton<PlayerManager>
    {
        public GameObject Player;
        public CinemachineVirtualCamera CmVcam;

        private GameObject inGamePlayer;
        private bool playerSpawned;

        public void SpawnPlayer()
        {
            Vector3 spawnPos = DungeonManager.Instance.FindFirstPositionOf("SpawnPoint");
            // Debug.Log($"Spawning Player at {spawnPos}");

            if (playerSpawned)
            {
                inGamePlayer.transform.position = spawnPos;
                return;
            }
            
            inGamePlayer = GameObject.Instantiate(Player, spawnPos, quaternion.identity);
            playerSpawned = true;
            CmVcam.Follow = inGamePlayer.transform;
            CmVcam.LookAt = inGamePlayer.transform;
        }

        private void OnEnable()
        {
            DungeonManager.Instance.DEventListener.OnPostDungeonBuildA += SpawnPlayer;
        }

        private void OnDisable()
        {
            DungeonManager.Instance.DEventListener.OnPostDungeonBuildA -= SpawnPlayer;
        }
    }
}
