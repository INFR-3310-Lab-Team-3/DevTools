using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    private void Update()
    {
        transform.forward = Vector3.ProjectOnPlane(Vector3.Lerp(transform.forward, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)).normalized, 8f * Time.deltaTime), Vector3.up);
        Move();
    }
    public override void Attack()
    {
        // Attack logic for Zombie
    }
}
