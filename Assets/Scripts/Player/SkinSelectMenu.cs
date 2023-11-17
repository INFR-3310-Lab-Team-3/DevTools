using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelectMenu : MonoBehaviour
{
    public GameObject skinToEquip;

    public void EquipSkin()
    {
        SkinSelector.skinLoad = skinToEquip;
    }
}
