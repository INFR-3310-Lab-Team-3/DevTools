using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : Singleton_template<ObjectiveManager>
{
    [SerializeField] private List<Objective> objectives;

    [SerializeField] private GameObject reward;

    private void Start()
    {
        reward.SetActive(false);
        for (int i = 1; i < objectives.Count-1; i++)
        {
            objectives[i].gameObject.SetActive(false);
        }
        objectives[0].gameObject.SetActive(true);
    }

    public void UnregisterObjective(Objective o)
    {
        objectives.Remove(o);
        reward.SetActive(objectives.Count == 0);
        objectives[0].gameObject.SetActive(objectives.Count != 0);
    }

    public void RegisterObjective(Objective o)
    {
        objectives.Add(o);
        reward.SetActive(false);
        objectives[0].gameObject.SetActive(objectives.Count == 1);
    }
}
