using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditorWindow : EditorWindow
{
    private SerializedObject serializedObject;
    private SerializedProperty soundSources;

    private void OnEnable()
    {
        serializedObject = new SerializedObject(SoundManager.Instance);
        soundSources = serializedObject.FindProperty("soundSources");
    }

    private void OnGUI()
    {
        GUILayout.Label("Sound Sources", EditorStyles.boldLabel);

        serializedObject.Update();

        EditorGUILayout.PropertyField(soundSources, true);

        serializedObject.ApplyModifiedProperties();

        GUILayout.Space(10);

        if (GUILayout.Button("Add Sound Source"))
        {
            SoundManager.Instance.soundSources.Add(new SoundSource());
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Apply Changes"))
        {
            EditorUtility.SetDirty(SoundManager.Instance);
        }
    }
}