using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target; // The target to aim at
    public Transform gunBarrel; // The point where projectiles are fired from.
    private float rotationSpeed = 5.0f;
    private float fireRate = 1.0f;
    public GameObject projectilePrefab; // Prefab of the projectile to be fired.
    public GameObject altProjectile;
    private bool mainFire =true;
    private float nextFireTime;

    private void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target.
            Vector3 targetDirection = target.position - transform.position;
            targetDirection.y = 0; // Ensure the turret only rotates horizontally.

            // Calculate the rotation to face the target.
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // Smoothly rotate the turret to face the target.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Fire a projectile if it's time to do so.
            if (Time.time > nextFireTime)
            {
               // Attack();
                nextFireTime = Time.time + 1 / fireRate;
            }
        }
    }
    public void Swap()
    {
        mainFire = !mainFire;
        Debug.Log("Weapon Swapped!!!");
    }

    public void Attack()
    {
        Debug.Log("Attack launched!!!");
        if (projectilePrefab && altProjectile != null)
        {
            GameObject projectile;
            // Create a new projectile instance at the gun barrel's position and rotation.
            if (mainFire)
            {
                projectile = Instantiate(projectilePrefab, gunBarrel.position, gunBarrel.rotation);
            }
            else
            {
                projectile = Instantiate(altProjectile, gunBarrel.position, gunBarrel.rotation) ;
            }

            // Add force to the projectile to make it shoot out of barrel.
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(gunBarrel.up * 5f, ForceMode.Impulse);
            }
        }
    }
}
