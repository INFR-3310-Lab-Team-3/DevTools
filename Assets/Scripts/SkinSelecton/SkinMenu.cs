using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMenu : MonoBehaviour
{
    public GameObject skinToEquip;

    public void EquipSkin()
    {
        SkinSelector2.skinLoad = skinToEquip;
    }

}
