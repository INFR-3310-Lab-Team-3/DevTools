using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    protected override void SetForward()
    {
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(Random.Range(-8f, 5f), 1f, Random.Range(-5f, 8f)).normalized, 6f * Time.deltaTime);
    }
    protected override void Attack()
    {
        Debug.Log("Ghost is Attacking!");
    }
}
