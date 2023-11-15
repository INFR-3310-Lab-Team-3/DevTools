using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : Singleton_template<ObjectiveManager>
{
    [SerializeField] public List<Objective> objectives;

    [SerializeField] public GameObject reward;

    private void Start()
    {
        reward.SetActive(false);
        for (int i = 1; i < objectives.Count; i++)
        {
            objectives[i].gameObject.SetActive(false);
        }
        objectives[0].gameObject.SetActive(true);
    }

    public void UnregisterObjective(Objective o)
    {
        objectives.Remove(o);
        if (objectives.Count == 0) reward.SetActive(true);
        else objectives[0].gameObject.SetActive(true);
    }

    public void RegisterObjective(Objective o)
    {
        objectives.Add(o);
        reward.SetActive(false);
        objectives[0].gameObject.SetActive(objectives.Count == 1);
    }
}
