using Florenia.Utility;
using TMPro;

namespace Florenia.Managers
{
    public class UIManager : UnitySingleton<UIManager>
    {
        public TMP_Text LoadingText;


        private void OnEnable()
        {
            DungeonManager.Instance.OnGenerate += OnGenerateDungeon;
            DungeonManager.Instance.OnCleanUp += OnCleanUpDungeon;
            DungeonManager.Instance.OnBuilding += OnBuildDungeon;
            DungeonManager.Instance.OnBuildingNavigation += OnBuildNavigation;
            DungeonManager.Instance.OnSpawningNPCs += OnSpawnNPCs;
            DungeonManager.Instance.OnCreatedLevel += OnCreatedDungeon;
        }
    
        private void OnDisable()
        {
            DungeonManager.Instance.OnGenerate -= OnGenerateDungeon;
            DungeonManager.Instance.OnCleanUp -= OnCleanUpDungeon;
            DungeonManager.Instance.OnBuilding -= OnBuildDungeon;
            DungeonManager.Instance.OnBuildingNavigation -= OnBuildNavigation;
            DungeonManager.Instance.OnSpawningNPCs -= OnSpawnNPCs;
            DungeonManager.Instance.OnCreatedLevel -= OnCreatedDungeon;
        }

        private void OnCreatedDungeon()
        {
            ChangeLoadingText("Done!");
            SetLoadingTextVisible(false);
        }

        private void OnSpawnNPCs()
        {
            ChangeLoadingText("Spawning NPCs...");
        }

        private void OnBuildNavigation()
        {
            ChangeLoadingText("Building navigation... ");
        }

        private void OnBuildDungeon()
        {
            ChangeLoadingText("Procedurally generating area...");
        }

        private void OnCleanUpDungeon()
        {
            ChangeLoadingText("Initializing...");
        }

        private void SetLoadingTextVisible(bool visible) {
            LoadingText.gameObject.SetActive(visible);
        }

        private void ChangeLoadingText(string text) {
            LoadingText.text = text;
        }
    
        private void OnGenerateDungeon()
        {
            SetLoadingTextVisible(true);
            ChangeLoadingText("Creating level... ");
        }
    }
}
