using System.Collections;
using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using DungeonArchitect.Flow.Impl.GridFlow;
using UnityEngine;

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

        private void SetLoadingTextVisible(bool visible) {
            // var container = loadingText.gameObject.transform.parent.gameObject;
            // container.SetActive(visible);
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
            SetLoadingTextVisible(true);
            // loadingText.text = "";
            AppendLoadingText("Generating Level... ");
            FloreniaDungeon.DestroyDungeon();
            NotifyDestroyed();
            yield return 0;	
    
            FloreniaDungeon.Build();
            yield return 0;
            NotifyBuild();
            yield return 0;	
            AppendLoadingText("Building Navigation... ");
            yield return 0;		// Wait for a frame to show our loading text
    
            RebuildNavigation();
            AppendLoadingText("Spawning NPCs...");
            yield return 0;		// Wait for a frame to show our loading text
    
            // npcSpawner.RebuildNPCs();
            AppendLoadingText("DONE!\n");
            SetLoadingTextVisible(false);
            yield return null;
        }

        private void AppendLoadingText(string text) {
            // loadingText.text += text;
        }

        private void RebuildNavigation() {
            // navMesh.Build();
        }
    }
}
