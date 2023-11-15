using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    protected Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (rb.velocity.magnitude > speed) rb.velocity = rb.velocity.normalized * speed;
        SetForward();
        if (ObjectiveManager.Instance().objectives.Count != 0)
        {
            rb.isKinematic = false;
            foreach (Objective o in ObjectiveManager.Instance().objectives)
            {
                if (o.gameObject.activeInHierarchy)
                {
                    if (!o.isThreat) ChangeState(new SeekState(o.transform.position));
                    else ChangeState(new FleeState(o.transform.position));
                }
            }
        }
        else if (ObjectiveManager.Instance().reward.activeInHierarchy) ChangeState(new SeekState(ObjectiveManager.Instance().reward.transform.position));//ChangeState(new IdleState(transform.position));
        else rb.isKinematic = true;
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
