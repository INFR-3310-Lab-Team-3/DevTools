using UnityEngine;
using System.Runtime.InteropServices;

public class PluginTester : MonoBehaviour
{
    public float xVal = 416.8346237f;
    public float yVal = 416.8346237f;
    public int numDecimals = 2;

    private const string DLL_NAME = "FinalProject_DLL";

    [DllImport(DLL_NAME)]
    private static extern int GetID();

    [DllImport(DLL_NAME)]
    private static extern void SetID(int id);

    [StructLayout(LayoutKind.Sequential)]
    struct Fint
    {
        int value;
        int factor;
        int sign;
    }

    [DllImport(DLL_NAME)]
    private static extern float GetXValue();
    [DllImport(DLL_NAME)]
    private static extern float GetYValue();

    [DllImport(DLL_NAME)]
    private static extern void SetXValue(float v, int precision);
    [DllImport(DLL_NAME)]
    private static extern void SetYValue(float v, int precision);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //SetID(33);
            //Debug.Log("ID: " + GetID());

            SetXValue(xVal, numDecimals);
            SetXValue(yVal, numDecimals);
            Debug.Log("X: " + GetXValue());
            Debug.Log("Y: " + GetYValue());
        }
    }
}