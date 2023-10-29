using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    private void Update()
    {
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(Random.Range(-8f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 8f)).normalized, 6f * Time.deltaTime);
        Move();
    }
    public override void Attack()
    {
        Debug.Log("Attacking!");
    }
}
