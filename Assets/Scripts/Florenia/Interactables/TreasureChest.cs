using System;
using System.Collections;
using DG.Tweening;
using Florenia.Interactables.Items.PickUps;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Florenia.Interactables
{
    public class TreasureChest : MonoBehaviour
    {
        public GameObject ItemPrefab;

        private float _throwDuration = 1.5f;
        private float _spawnYOffset = 0.25f;
        
        private bool _didSpawn;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (_didSpawn) return;
            if (col.gameObject.CompareTag("Player"))
            {
                DropItem();
            }
        }

        [ContextMenu("DropItem")]
        private void DropItem()
        {
            _didSpawn = true;
            GameObject spawnedItem = SpawnItem();
            spawnedItem.GetComponent<PickableItem>().SetLateCollidable(_throwDuration);
            AnimateItem(spawnedItem);
        }

        private void AnimateItem(GameObject spawnedItem)
        {
            if (!RandomPoint(transform.position, 1.5f, out Vector3 rPoint)) return;
            spawnedItem.GetComponent<PickUpEffect>().SetLateAnimate(_throwDuration, rPoint);
            spawnedItem.transform.DOJump(rPoint, 0.8f, 1, _throwDuration - 0.01f).Play();
        }

        private GameObject SpawnItem()
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + _spawnYOffset, 0);
            GameObject spawnedItem = Instantiate(ItemPrefab, spawnPos, ItemPrefab.transform.rotation);
            return spawnedItem;
        }

        private bool RandomPoint(Vector2 center, float radius, out Vector3 result)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + GetUnitOnCircle(Random.Range(0f,180f), radius);
                randomPoint.z = 0;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 0.1f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    result.z = 0;
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
        }
        
        private Vector2 GetUnitOnCircle(float angleDegrees, float radius) {
 
            // initialize calculation variables
            float _x = 0;
            float _y = 0;
            float angleRadians = 0;
            Vector2 _returnVector;
 
            // convert degrees to radians
            angleRadians = angleDegrees * Mathf.PI / 180.0f;
 
            // get the 2D dimensional coordinates
            _x = radius * Mathf.Cos(angleRadians);
            _y = radius * Mathf.Sin(angleRadians);
 
            // derive the 2D vector
            _returnVector = new Vector2(_x, _y);
 
            // return the vector info
            return _returnVector;
        }
    }
}