using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using DungeonArchitect.Flow.Impl.GridFlow;
using UnityEngine;
using UnityEngine.Serialization;

namespace Florenia
{
    public class DungeonManager : UnitySingleton<DungeonManager>
    {
        public Dungeon FloreniaDungeon;
        public DungeonEventListener DEventListener;

        public GridFlowAsset SmallDGridFlow;
        public GridFlowAsset MediumDGridFlow;
        public GridFlowAsset BigDGridFlow;
        
        private GridFlowDungeonConfig dConfig;

        protected override void Awake()
        {
            base.Awake();
            dConfig = (GridFlowDungeonConfig) FloreniaDungeon.Config;
        }

        [ContextMenu("Build Dungeon")]
        public void Build()
        {
            dConfig.flowAsset = SmallDGridFlow;
            dConfig.Seed = (uint)(Random.value * int.MaxValue);
            
            FloreniaDungeon.Build();
        }
        
    }
}
