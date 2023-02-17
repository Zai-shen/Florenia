using System;
using Florenia;
using Florenia.Managers;
using UnityEngine;

public class PlayerManager : UnitySingleton<PlayerManager>
{
    public GameObject Player;

    public void SpawnPlayer()
    {
        Debug.Log("Spawning Player");
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
