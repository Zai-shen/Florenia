using Florenia.Utility;
using UnityEngine;

namespace Florenia.Managers
{
    public class PlayerManager : UnitySingleton<PlayerManager>
    {
        public GameObject Player;

        public void SpawnPlayer()
        {
            Debug.Log("Spawning Player");
            GameObject.Instantiate(Player);
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
