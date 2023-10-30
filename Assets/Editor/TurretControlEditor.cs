using UnityEditor;
using UnityEngine;

public class TurretControlEditor : EditorWindow
{
    [MenuItem("Window/Turret Control Window")]
    public static void ShowWindow()
    {
        GetWindow<TurretControlEditor>("Turret Control");
    }

    private void OnGUI()
    {
        GUILayout.Label("Turret Control", EditorStyles.boldLabel);
        GUILayout.Label("While in runtime, choose a command then click Run", EditorStyles.label);

        // Begin a vertical group for the first two buttons
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Swap Bullets"))
        {
            SetSwapCommand();
        }

        if (GUILayout.Button("Attack"))
        {
            SetAttackCommand();
        }
        // End the vertical group
        GUILayout.EndHorizontal();
        if (GUILayout.Button("RUN COMMAND"))
        {
            ExecuteCommand();
        }
    }
    // Binding Commands
    private void SetSwapCommand()
    {
        GameObject commandTrigger = GameObject.Find("CommandTrigger");
        if (commandTrigger != null)
        {
            commandTrigger.GetComponent<CommandTrigger>().SetSwapCommand();
        }
    }

    private void SetAttackCommand()
    {
        GameObject commandTrigger = GameObject.Find("CommandTrigger"); 
        if (commandTrigger != null)
        {
            commandTrigger.GetComponent<CommandTrigger>().SetAttackCommand();
        }
    }

    private void ExecuteCommand()
    {
        GameObject commandTrigger = GameObject.Find("CommandTrigger");
        if (commandTrigger != null)
        {
            commandTrigger.GetComponent<CommandTrigger>().ExecuteCommand();
        }
    }
}
