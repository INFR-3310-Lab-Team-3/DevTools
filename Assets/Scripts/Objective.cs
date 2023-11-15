using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public bool isThreat = false;
    public float dist = 0f;
    void Start()
    {
        
    }

    protected void OnCompleted()
    {
        ObjectiveManager.Instance().UnregisterObjective(this);
        this.gameObject.SetActive(false);
    }
}
