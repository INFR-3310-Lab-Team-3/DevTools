using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector2 : MonoBehaviour
{
    public static GameObject skinLoad;

    private void Awake()
    {
        Instantiate(skinLoad, transform);
    }

}
