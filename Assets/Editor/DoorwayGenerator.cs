using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class DoorwayGenerator : EditorWindow
{
    private GameObject wallToChange;
    private GameObject door;
    private float doorX = 0;
    private Vector3 doorSize = Vector3.zero;
    private Vector3 doorPosition = Vector3.zero;

    [MenuItem("Window/Doorway Generator")]
    public static void ShowWidow()
    {
        var window = GetWindow<DoorwayGenerator>("Doorway Generator");
        window.Focus();
    }
    private void OnSelectionChange()
    {
        if (Selection.activeGameObject != null)
        {
            wallToChange = Selection.activeGameObject.transform.parent.gameObject;
            doorSize = new Vector2(wallToChange.transform.localScale.x * 0.5f, wallToChange.transform.localScale.y * 0.5f);
        }
    }
    private void Update()
    {
        var window = GetWindow<DoorwayGenerator>("Doorway Generator");
        window.Focus();
    }
    private void OnGUI()
    {
        GUILayout.Label("Doorway Generator", EditorStyles.boldLabel);

        if (Selection.activeGameObject != null)
        {
            wallToChange = Selection.activeGameObject.transform.parent.gameObject;
            GUILayout.Label("Max Door X: " + wallToChange.transform.localScale.x, EditorStyles.label);
            GUILayout.Label("Max Door Y: " + wallToChange.transform.localScale.y, EditorStyles.label);
            doorSize = EditorGUILayout.Vector2Field("Door Size", doorSize);
            if (doorSize.x > wallToChange.transform.localScale.x) doorSize.x = wallToChange.transform.localScale.x;
            if (doorSize.x < 0) doorSize.x = 0;
            if (doorSize.y > wallToChange.transform.localScale.y) doorSize.y = wallToChange.transform.localScale.y;
            if (doorSize.y < 0) doorSize.y = 0;
            ///
            GUILayout.Label("Max X pos: " + ((wallToChange.transform.localScale.x * 0.5f) - (doorSize.x * 0.5f)), EditorStyles.label);
            GUILayout.Label("Min X pos: " + (-(wallToChange.transform.localScale.x * 0.5f) + (doorSize.x * 0.5f)), EditorStyles.label);
            doorX = EditorGUILayout.FloatField("Door X Position", doorX);
            if (doorX > ((wallToChange.transform.localScale.x * 0.5f) - (doorSize.x * 0.5f))) doorX = ((wallToChange.transform.localScale.x * 0.5f) - (doorSize.x * 0.5f));
            if (doorX < (-(wallToChange.transform.localScale.x * 0.5f) + (doorSize.x * 0.5f))) doorX = (-(wallToChange.transform.localScale.x * 0.5f) + (doorSize.x * 0.5f));

            if (GUILayout.Button("Modify Wall"))
            {
                ModifyWall();
            }
        }
        else GUILayout.Label("Please Select a wall first", EditorStyles.label);
    }
    private void ModifyWall()
    {
        // create new empty wall object
        GameObject newWall = new();
        newWall.transform.SetPositionAndRotation(wallToChange.transform.position, wallToChange.transform.rotation);
        newWall.transform.parent = wallToChange.transform.parent;
        newWall.name = "Wall";

        // handle door constraints
        doorPosition.x = doorX;
        doorPosition.y = (wallToChange.transform.localScale.y * -0.5f) + (doorSize.y * 0.5f);
        doorPosition.z = 0f;
        doorSize.z = wallToChange.transform.localScale.z * 0.5f;

        float RightXScale = (wallToChange.transform.localScale.x * 0.5f) - doorPosition.x - (doorSize.x * 0.5f);
        float RightXPos = doorPosition.x + (doorSize.x * 0.5f) + (RightXScale * 0.5f);
        float LeftXScale = doorPosition.x + (wallToChange.transform.localScale.x * 0.5f) - (doorSize.x * 0.5f);
        float LeftXPos = doorPosition.x - (doorSize.x * 0.5f) - (LeftXScale * 0.5f);

        Vector3 LeftScale = new Vector3(LeftXScale, wallToChange.transform.localScale.y, wallToChange.transform.localScale.z);
        Vector3 LeftPos = new Vector3(LeftXPos, 0f, 0f);
        Vector3 RightScale = new Vector3(RightXScale, wallToChange.transform.localScale.y, wallToChange.transform.localScale.z);
        Vector3 RightPos = new Vector3(RightXPos, 0f, 0f);

        float TopScaleY = wallToChange.transform.localScale.y - doorSize.y;
        float TopPosY = (wallToChange.transform.localScale.y * 0.5f) - (TopScaleY * 0.5f);
        // create door and new walls
        door = CreateWall(doorPosition, doorSize, newWall.transform);
        door.name = "door";
        GameObject RightWallPortion = CreateWall(RightPos, RightScale, newWall.transform);
        RightWallPortion.name = "RightWall";
        GameObject LeftWallPortion = CreateWall(LeftPos, LeftScale, newWall.transform);
        LeftWallPortion.name = "LeftWall";
        GameObject TopWallPortion = CreateWall(new Vector3(doorPosition.x, TopPosY, doorPosition.z), new Vector3(doorSize.x, TopScaleY, wallToChange.transform.localScale.z), newWall.transform);
        TopWallPortion.name = "TopWall";

        // destroy
        DestroyImmediate(wallToChange.transform.GetChild(0).gameObject);
        Debug.Log("RW: " + RightWallPortion.transform.position);
    }
    GameObject CreateWall(Vector3 position, Vector3 size, Transform parent)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.localScale = size;
        wall.transform.position = parent.TransformPoint(position); // Convert local position to world space
        wall.transform.rotation = parent.rotation;
        wall.name = name;
        wall.transform.parent = parent;
        return wall;
    }
}
