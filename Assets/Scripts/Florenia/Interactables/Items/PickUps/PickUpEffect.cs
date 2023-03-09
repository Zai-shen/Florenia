using System.Collections;
using UnityEngine;

namespace Florenia.Interactables.Items.PickUps
{ 
    public class PickUpEffect : MonoBehaviour {
        public Vector2 wobbleDirection = new Vector2(0, 0.125f);
        public float speed = 0.4f;
        public float randomStartAngle = 15f;	// So they don't all wobble in the same sequence
        public bool animate = true;
        
        private Vector3 originalPosition;

        private void Start () {
            originalPosition = transform.position;
            randomStartAngle = Random.value * 100;
        }

        public void SetLateAnimate(float duration, Vector3 finalPosition)
        {
            StartCoroutine(StopAnimation(duration, finalPosition));
        }
        
        private IEnumerator StopAnimation(float duration, Vector3 finalPosition)
        {
            animate = false;
            yield return new WaitForSeconds(duration);
            originalPosition = finalPosition;
            animate = true;
        }

        private void Update () {
            if (!animate) return;
            
            float t = Time.time * Mathf.PI + randomStartAngle;
            t *= speed;
            Vector2 offset = Mathf.Sin(t) * wobbleDirection;
            transform.position = originalPosition + new Vector3(offset.x, offset.y, 0);
        }
    }
}