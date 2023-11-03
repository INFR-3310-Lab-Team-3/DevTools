using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float damage;
    protected Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    protected abstract void Attack();

    protected void Move(float speed)
    {
        rb.AddForce(transform.forward * speed);
    }
}
