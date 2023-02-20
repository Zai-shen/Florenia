using System;
using System.Collections;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using DungeonArchitect.Flow.Impl.GridFlow;
using DungeonArchitect.Utils;
using Florenia.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Florenia.Managers
{
    public class DungeonManager : UnitySingleton<DungeonManager>
    {
        public Dungeon FloreniaDungeon;
        public ActionDungeonEventListener DEventListener;

        public GridFlowAsset SmallDGridFlow;
        public GridFlowAsset MediumDGridFlow;
        public GridFlowAsset BigDGridFlow;
        
        private GridFlowDungeonConfig dConfig;

        public Action OnGenerate;
        public Action OnCleanUp;
        public Action OnDestroyed;
        public Action OnBuilding;
        public Action<DungeonModel, LevelMarkerList> OnBuilt;
        public Action OnBuildingNavigation;
        public Action OnSpawningNPCs;
        public Action OnCreatedLevel;
        

        protected override void Awake()
        {
            base.Awake();
            dConfig = (GridFlowDungeonConfig) FloreniaDungeon.Config;
        }

        [ContextMenu("Build small Dungeon")]
        public void BuildSmall()
        {
            dConfig.flowAsset = SmallDGridFlow;
            CreateNewLevel();
        }
        
        [ContextMenu("Build medium Dungeon")]
        public void BuildMedium()
        {
            dConfig.flowAsset = MediumDGridFlow;
            CreateNewLevel();
        }
        
        [ContextMenu("Build big Dungeon")]
        public void BuildBig()
        {
            dConfig.flowAsset = BigDGridFlow;
            CreateNewLevel();
        }

        [ContextMenu("Destroy Dungeon")]
        public void DestroyCurrent()
        {
            FloreniaDungeon.DestroyDungeon();
        }
        
        public void CreateNewLevel() {
            FloreniaDungeon.Config.Seed = (uint)(Random.value * int.MaxValue);
            StartCoroutine(RebuildLevelRoutine());
        }

        public Vector2 FindFirstPositionOf(string markerName)
        {
            Vector3 gridCellSize = Vector3.one;
            Vector2 reCentering = Vector2.one / 2;
            
            foreach (PropSocket marker in FloreniaDungeon.Markers)
            {
                if (marker.SocketType == markerName) //marker.Equals(markerName))
                {
                    Vector3 worldPos = MathUtils.GridToWorld(gridCellSize, marker.gridPosition);
                    // Debug.Log($"Found marker at {marker.gridPosition} grid coo, resulting in {worldPos} world coo.");
                    
                    return new Vector2(worldPos.x, worldPos.z) + reCentering;
                }
            }
            
            return new Vector2(0, 0);
        }
        
        private IEnumerator RebuildLevelRoutine() {
            OnGenerate?.Invoke();
            
            OnCleanUp?.Invoke();
            FloreniaDungeon.DestroyDungeon();
            OnDestroyed?.Invoke();
            yield return new WaitForSeconds(0.1f);	
    
            OnBuilding?.Invoke();
            FloreniaDungeon.Build();
            yield return new WaitForSeconds(0.1f);	
            OnBuilt?.Invoke(FloreniaDungeon.ActiveModel, FloreniaDungeon.Markers);
            yield return new WaitForSeconds(0.1f);	
            
            OnBuildingNavigation?.Invoke();
            yield return new WaitForSeconds(0.1f);	
            
            OnSpawningNPCs?.Invoke();
            yield return new WaitForSeconds(0.1f);	
    
            // npcSpawner.RebuildNPCs();
            OnCreatedLevel?.Invoke();
            yield return null;
        }


    }
}
