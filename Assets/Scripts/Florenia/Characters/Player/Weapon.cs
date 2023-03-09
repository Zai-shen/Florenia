using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject attackPrefab;
    public int Damage = 20;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject shot = Instantiate(attackPrefab, firePoint.position, firePoint.rotation);
        Projectile proj = shot.GetComponent<Projectile>();
        proj.ShotDamage = Damage;
        proj.Shoot();
    }
}
