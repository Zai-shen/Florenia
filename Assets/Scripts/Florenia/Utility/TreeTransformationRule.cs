using UnityEngine;
using DungeonArchitect;

namespace Florenia.Utility
{
    public class TreeTransformationRule : TransformationRule {
        public float angleVariation = 20;
        public float positionVariation = 0.1f;

        public override void GetTransform(PropSocket socket, DungeonModel model, Matrix4x4 propTransform, System.Random random, out Vector3 outPosition, out Quaternion outRotation, out Vector3 outScale) {
            base.GetTransform(socket, model, propTransform, random, out outPosition, out outRotation, out outScale);

            var randomVal = random.value() * 2 - 1;
            var angle = randomVal * angleVariation;
            var rotation = Quaternion.Euler(0, angle, 0);
            outRotation = rotation;
            outPosition += new Vector3((random.value() * 2 - 1) * positionVariation,0,0);
            outScale -= new Vector3(randomVal > 0 ? 0 : outScale.x * 2, 0, 0);
        }
    }

}