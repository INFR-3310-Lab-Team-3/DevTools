using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    public static GameObject skinLoad;

    private void Awake()
    {
        Instantiate(skinLoad, transform);
    }
}
