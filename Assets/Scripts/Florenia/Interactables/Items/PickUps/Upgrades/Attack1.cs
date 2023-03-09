using Florenia.Characters.Player;
using UnityEngine;

namespace Florenia.Interactables.Items.PickUps.Temporary
{
    [CreateAssetMenu(menuName = "Florenia/PickUps/Attack01")]
    public class Attack1 : PickUp
    {
        public int Damage;
        public override void Apply(GameObject target)
        {
            target.GetComponent<Weapon>().Damage = Damage;
        }
    }
}