using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public bool isThreat = false;
    public float dist = 0f;

    private int count = 0;
    private int numG = 0;
    private int numZ = 0;
    private void Update()
    {
        if (numG == manager.Instance().maxGhost && numZ == manager.Instance().maxZombie && !isThreat) OnCompleted();
        if (isThreat)
        {
            foreach (Enemy e in manager.Instance().enemies)
            {
                if (Vector3.Distance(e.transform.position, transform.position) > dist) count++;
            }
            if (count == manager.Instance().enemies.Count) OnCompleted();
            else count = 0;
        }
    }

    protected void OnCompleted()
    {
        ObjectiveManager.Instance().UnregisterObjective(this);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) numG++;
        if (other.gameObject.layer == 7) numZ++;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6) numG--;
        if (other.gameObject.layer == 7) numZ--;
    }
}
