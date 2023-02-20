using System;
using DungeonArchitect.Builders.GridFlow;
using DungeonArchitect.Navigation;
using UnityEngine;

namespace Florenia.Managers
{
    public class NavigationManager : MonoBehaviour
    {
        private DungeonNavMesh _dNavMesh;

        private void Awake()
        {
            _dNavMesh = GetComponent<DungeonNavMesh>();
        }

        private void OnEnable()
        {
            DungeonManager.Instance.OnBuildingNavigation += DoBuildNavmesh;
        }

        private void OnDisable()
        {
            DungeonManager.Instance.OnBuildingNavigation -= DoBuildNavmesh;
        }
        
        private void DoBuildNavmesh()
        {
            _dNavMesh.Build();
        }
    }
}
