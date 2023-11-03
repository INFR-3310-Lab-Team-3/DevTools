using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Room_AutoBuild : MonoBehaviour
{
    public List<GameObject> walls = new();
    public List<GameObject> wallTargets = new();
    private void Update()
    {
        UpdateRoom();
    }
    private void UpdateRoom()
    {
        foreach (GameObject w in walls)
        {
            UpdateWall(w);
        }
    }
    private void UpdateWall(GameObject w)
    {
        int index = walls.IndexOf(w);
        Transform LT = wallTargets[index == 0 ? walls.Count - 1 : index - 1].transform;
        Transform RT = wallTargets[index].transform;
        w.transform.localScale = new Vector3(Vector3.Distance(LT.position, RT.position), w.transform.localScale.y, w.transform.localScale.z);
        w.transform.position = (LT.position + RT.position) * 0.5f;
        w.transform.right = (RT.position - w.transform.position).normalized;
    }
}
