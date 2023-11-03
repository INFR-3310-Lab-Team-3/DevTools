using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float chaseSpeed;
    private void Update()
    {
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(Random.Range(-8f, 5f), 1f, Random.Range(-5f, 8f)).normalized, 6f * Time.deltaTime);
        Move(wanderSpeed);
    }
    protected override void Attack()
    {
        Debug.Log("Attacking!");
    }
}
