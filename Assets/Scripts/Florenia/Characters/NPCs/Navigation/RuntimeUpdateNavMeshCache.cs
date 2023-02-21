using NavMeshPlus.Extensions;
using UnityEngine;

namespace Florenia.Characters.NPCs.Navigation
{
    class RuntimeUpdateNavMeshCache: MonoBehaviour
    {
        public bool SetDirty = false;
        public CollectSourcesCache2d cacheSources2D;

        private void Update()
        {
            if (IsDirty())
            {
                Debug.Log("Cache dirty, updating navmesh");
                cacheSources2D.UpdateNavMesh();
                SetDirty = false;
            }
        }

        private bool IsDirty()
        {
            if (SetDirty)
            {
                return true;
            }

            return cacheSources2D.IsDirty;
        }

        [ContextMenu("Set Cache Dirty")]
        public void SetCacheDirty()
        {
            SetDirty = true;
        }
    }
}