using System;
using System.Collections;
using System.Collections.Generic;
using Florenia.Characters;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public ParticleSystem HitEffect;
    
    [HideInInspector]public float ShotForce;
    [HideInInspector]public int ShotDamage;
    public float MaxLifeTime = 4f;
    private Rigidbody _rb;
    private bool _didHit;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void Shoot()
    {
        StartCoroutine(DelayedDestroy(MaxLifeTime));
        //_rb.AddForce(direction * ShotForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colliderGo = collision.gameObject;
        if ((((1 << colliderGo.layer) == Globals.PlayerMask)
            || ((1 << colliderGo.layer) == Globals.EnemyMask))
            && !_didHit)
        {
            _didHit = true;
            
            DealDamage(colliderGo);
            CreateHitFX();
            Destroy(this.gameObject);
        }
    }

    private void CreateHitFX()
    {
        if (HitEffect)
        {
            Instantiate(HitEffect, transform.position, Quaternion.identity);
        }
    }

    private void DealDamage(GameObject colliderGo)
    {
        Health hp = colliderGo.GetComponent<Health>();
        if (hp)
        {
            hp.TryTakeDamage(ShotDamage);
        }
        else
        {
            hp = GetComponentInParents(colliderGo.transform);
            hp.TryTakeDamage(ShotDamage);
        }
    }

    private Health GetComponentInParents(Transform current)
    {
        Health hp = null;
        while (!hp)
        {
            hp = current.GetComponentInParent<Health>(true);
            if (!hp)
            {
                current = current.parent;
            }
        }
        return hp;
    }

    private IEnumerator DelayedDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}