using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    protected override void SetForward()
    {
        transform.forward = rb.velocity.magnitude > 1f ? rb.velocity.normalized : Vector3.right;//Vector3.ProjectOnPlane(Vector3.Lerp(transform.forward, new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f)).normalized, 8f * Time.deltaTime), Vector3.up);
    }
    protected override void Attack()
    {
        Debug.Log("Zombie is Attacking!");
    }
}
