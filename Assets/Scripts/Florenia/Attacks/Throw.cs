using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class Throw : RangedAttack
{
    public float flightDuration = 2.5f;
    
    private Sequence highThrow;
    
    protected override void DoAttack()
    {
        base.DoAttack();

        Vector2 direction = Target.position - transform.position;
        direction.Normalize();
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector2 _spawnLocation = (Vector2) (transform.position) + (direction);// + new Vector3(0, _spawnDistance.y, 0));

        GameObject shot = Instantiate(Projectile, _spawnLocation, Quaternion.identity);
        Projectile proj = shot.GetComponent<Projectile>();
        proj.ShotDamage = AttackDamage;
        proj.Shoot();
        
        Vector3 highScale = Projectile.transform.localScale * 1.5f;
        Tween scaling = shot.transform
            .DOScale(highScale, flightDuration / 2f)
            .SetLoops(2, LoopType.Yoyo);
        Tween movement = shot.transform
            .DOMove((Vector2) Target.position, flightDuration);
        
        highThrow = DOTween.Sequence();
        highThrow.Append(scaling)
            .Join(movement)
            .SetEase(Ease.InOutSine);
        
        highThrow.Play();
    }

    private void OnDestroy()
    {
        highThrow.Kill();
    }
}