using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State_I
{
    void Execute(Enemy enemy);
}

public class SeekState : State_I
{
    private Vector3 target;

    public SeekState(Vector3 t)
    {
        this.target = t;
    }

    public Vector3 SetTarget
    {
        set { this.target = value; }
    }

    public void Execute(Enemy enemy)
    {
        enemy.SteerTowards(target);
    }
}

public class FleeState : State_I
{
    private Vector3 target;

    public FleeState(Vector3 t)
    {
        this.target = t;
    }

    public Vector3 SetTarget
    {
        set { this.target = value; }
    }

    public void Execute(Enemy enemy)
    {
        enemy.SteerAwayFrom(target);
    }
}
