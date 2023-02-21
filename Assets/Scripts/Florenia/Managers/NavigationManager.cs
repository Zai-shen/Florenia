using System;
using System.Collections;
using NavMeshPlus.Components;
using UnityEngine;

namespace Florenia.Managers
{
    public class NavigationManager : MonoBehaviour
    {
        public NavMeshSurface _navMeshSurface;
        
        private void OnEnable()
        {
            DungeonManager.Instance.OnBuildingNavigation += DoBuildNavmesh;
        }

        private void OnDisable()
        {
            DungeonManager.Instance.OnBuildingNavigation -= DoBuildNavmesh;
        }

        [ContextMenu("Rebuild Navmesh")]
        private void DoBuildNavmesh()
        {
            StartCoroutine(DoBuildNavmeshCo());
        }
        
        private IEnumerator DoBuildNavmeshCo()
        {
            ClearNavMesh();
            Physics2D.SyncTransforms();

            yield return new WaitForFixedUpdate();
            _navMeshSurface.BuildNavMesh();
            yield return null;
        }

        [ContextMenu("Clear Navmesh")]
        private void ClearNavMesh()
        {
            _navMeshSurface.RemoveData();
        }
    }
}
