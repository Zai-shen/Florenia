using Florenia.Characters;
using UnityEngine;

namespace Florenia.Interactables.Items.PickUps.Temporary
{
    [CreateAssetMenu(menuName = "Florenia/PickUps/Heal")]
    public class HealPickup : PickUp
    {
        public int Health;
        
        public override void Apply(GameObject target)
        {
            target.GetComponent<Health>().RegenerateHealth(Health);
        }
    }
}