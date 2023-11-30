using UnityEngine;
using System.Runtime.InteropServices;

public class PluginTester : MonoBehaviour
{
    public float Val = 416.8346237f;
    public int numDecimals = 2;

    private const string DLL_NAME = "FinalProject_DLL";

    [StructLayout(LayoutKind.Sequential)]
    struct FintValue
    {
        int value;
        int factor;
    }

    [DllImport(DLL_NAME)]
    private static extern float GetValue();

    [DllImport(DLL_NAME)]
    private static extern void SetValue(float v, int precision);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetValue(Val, numDecimals);
            Debug.Log("Val: " + GetValue());
        }
    }
}