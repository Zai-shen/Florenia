using System;
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
        GameManager.Instance.NextDungeon();
    }
}
