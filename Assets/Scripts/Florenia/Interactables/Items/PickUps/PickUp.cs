using UnityEngine;

namespace Florenia.Interactables.Items.PickUps
{
    public abstract class PickUp : ScriptableObject
    {
        public bool AddToInventory;
        public abstract void Apply(GameObject target);
    }
}