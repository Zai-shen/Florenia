using UnityEngine;

namespace Florenia.Interactables.Items.PickUps.Temporary
{
    [CreateAssetMenu(menuName = "Florenia/PickUps/Key")]
    public class Key : PickUp
    {
        public override void Apply(GameObject target)
        {
            AddToInventory = true;
        }
    }
}