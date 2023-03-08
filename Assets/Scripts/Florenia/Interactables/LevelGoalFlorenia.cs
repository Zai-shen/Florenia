using System;
using System.Collections;
using System.Collections.Generic;
using Florenia.Characters.Player;
using Florenia.Managers;
using UnityEngine;

public class LevelGoalFlorenia : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartNextLevel();
        }
    }

    [ContextMenu("Start next Level")]
    private static void StartNextLevel()
    {
        PlayerManager.Instance.InGamePlayer.GetComponent<Player>().Die(1);
    }
}
