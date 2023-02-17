using System;
using System.Collections;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using DungeonArchitect.Flow.Impl.GridFlow;
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
        public Action OnBuilt;
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



        private void NotifyBuild()
        {
            // waypointGenerator.BuildWaypoints(FloreniaDungeon.ActiveModel, FloreniaDungeon.Markers);
            // specialRoomFinder.FindSpecialRooms(FloreniaDungeon.ActiveModel);
        }

        private void NotifyDestroyed() {
            // waypointGenerator.OnDungeonDestroyed(FloreniaDungeon);
            // specialRoomFinder.OnDungeonDestroyed(FloreniaDungeon);
        }

        private IEnumerator RebuildLevelRoutine() {
            OnGenerate?.Invoke();
            
            OnCleanUp?.Invoke();
            FloreniaDungeon.DestroyDungeon();
            OnDestroyed?.Invoke();//NotifyDestroyed();
            yield return new WaitForSeconds(0.1f);	
    
            OnBuilding?.Invoke();
            FloreniaDungeon.Build();
            yield return new WaitForSeconds(0.1f);	
            OnBuilt?.Invoke();//NotifyBuild();
            yield return new WaitForSeconds(0.1f);	
            
            OnBuildingNavigation?.Invoke();
            yield return new WaitForSeconds(0.1f);	
            RebuildNavigation();
            
            OnSpawningNPCs?.Invoke();
            yield return new WaitForSeconds(0.1f);	
    
            // npcSpawner.RebuildNPCs();
            OnCreatedLevel?.Invoke();
            yield return null;
        }



        private void RebuildNavigation() {
            // navMesh.Build();
        }
    }
}
