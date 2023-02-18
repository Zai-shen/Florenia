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

        private bool playerSpawned;

        public void SpawnPlayer()
        {
            if (playerSpawned)
                return;

            Vector3 spawnPos = DungeonManager.Instance.FindFirstPositionOf("SpawnPoint");
            // Debug.Log($"Spawning Player at {spawnPos}");
            GameObject inGamePlayer = GameObject.Instantiate(Player, spawnPos, quaternion.identity);
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
