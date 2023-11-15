using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    protected Rigidbody rb;


    private SeekState seekCase;
    private FleeState fleeCase;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        seekCase = new SeekState(transform.position);
        fleeCase = new FleeState(transform.position);
    }
    void Update()
    {
        SetForward();



        currentState?.Execute(this);
    }

    protected abstract void Attack();

    private State_I currentState;

    public void ChangeState(State_I newState)
    {
        currentState = newState;
    }

    protected abstract void SetForward();

    public void SteerTowards(Vector3 target)
    {
        rb.AddForce(speed * (target - transform.position).normalized);
    }
    public void SteerAwayFrom(Vector3 threat)
    {
        rb.AddForce(speed * (transform.position - threat).normalized);
    }
}
