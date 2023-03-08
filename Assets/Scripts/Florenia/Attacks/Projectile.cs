using System;
using System.Collections;
using System.Collections.Generic;
using Florenia.Characters;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask CollisionLayers;
    public ParticleSystem HitEffect;
    
    [HideInInspector]public float ShotForce;
    public int ShotDamage;
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
        if (((CollisionLayers.value & (1 << colliderGo.layer)) > 0)
            && !_didHit)
        {
            _didHit = true;
            
            DealDamage(colliderGo);
            CreateHitFX();
            Destroy(this.gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject colliderGo = collision.gameObject;
        if (((CollisionLayers.value & (1 << colliderGo.layer)) > 0)
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