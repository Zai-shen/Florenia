using System;
using Cinemachine;
using Florenia.Characters.Player;
using Florenia.Utility;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Florenia.Managers
{
    public class PlayerManager : UnitySingleton<PlayerManager>
    {
        public GameObject PlayerPrefab;
        public CinemachineVirtualCamera CmVcam;
        public Player InGamePlayer;

        public static Action PlayerSpawn;
        private bool playerSpawned;

        public void SpawnPlayer()
        {
            Vector3 spawnPos = DungeonManager.Instance.FindFirstPositionOf("SpawnPoint");
            // Debug.Log($"Spawning PlayerPrefab at {spawnPos}");

            if (playerSpawned)
            {
                InGamePlayer.transform.position = spawnPos;
                PlayerSpawn?.Invoke();
                return;
            }
            
            InGamePlayer = GameObject.Instantiate(PlayerPrefab, spawnPos, quaternion.identity).GetComponent<Player>();
            playerSpawned = true;
            PlayerSpawn?.Invoke();
            CmVcam.Follow = InGamePlayer.transform;
            CmVcam.LookAt = InGamePlayer.transform;
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
