using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DoorwayGenerator : EditorWindow
{
    private GameObject wallToChange;
    private GameObject door;
    private Vector3 doorSize = Vector3.zero;
    private Vector3 doorPosition = Vector3.zero;

    [MenuItem("Window/Doorway Generator")]
    public static void ShowWidow()
    {
        EditorWindow.GetWindow(typeof(DoorwayGenerator));
    }
    private void OnGUI()
    {
        GUILayout.Label("Doorway Generator", EditorStyles.boldLabel);

        wallToChange = Selection.activeGameObject;

        doorSize = EditorGUILayout.Vector2Field("Door Size", doorSize);
        doorPosition = EditorGUILayout.Vector2Field("Door Position", doorPosition);

        if (GUILayout.Button("Modify Wall"))
        {
            if (wallToChange != null) ModifyWall();
            else Debug.LogWarning("Please Select a wall first");
        }
    }
    /// <summary>
    /// doorPos: -(wallScale.x - (doorScale.x * 0.5f) To (wallScale.x - (doorScale.x * 0.5f), doorScale.y * 0.5f, 0
    /// doorScale.z == wallToChangeScale.z* 0.5f
    /// LeftWallScale: (wallToChangeScale.x* 0.5f) - (doorScale.x* 0.5f) + doorPos.x, wallToChangeScale.y, wallToChangeScale.z
    /// LeftWallPos: (wallToChangeScale.x* 0.5f) - (doorPos.x - doorScale.x), LeftWallScale.y* 0.5f, 0
    /// 
    /// RightWallScale: (wallToChangeScale.x* 0.5f) - (doorScale.x* 0.5f) - doorPos.x, wallToChangeScale.y, wallToChangeScale.z
    /// RightWallPos: -(wallToChangeScale.x* 0.5f) - (doorPos.x - doorScale.x), RightWallScale.y* 0.5f, 0
    /// 
    /// TopWallScale: doorScale.x, wallToChangeScale.y - (doorScale.y + doorPos.y), wallToChangeScale.z
    /// TopWallPos: doorPos.x, wallToChangeScale.y - (doorScale.y + (doorPos.y* 0.5f)), 0
    /// </summary>
    private void ModifyWall()
    {
        // create new empty wall object
        GameObject newWall = new();
        newWall.transform.SetPositionAndRotation(wallToChange.transform.position, wallToChange.transform.rotation);
        newWall.transform.parent = wallToChange.transform.parent;
        newWall.name = "Wall";

        // handle door constraints
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
        door = CreateCube(doorPosition, doorSize, newWall.transform);
        door.name = "door";
        GameObject RightWallPortion = CreateCube(RightPos, RightScale, newWall.transform);
        RightWallPortion.name = "RightWall";
        GameObject LeftWallPortion = CreateCube(LeftPos, LeftScale, newWall.transform);
        LeftWallPortion.name = "LeftWall";
        GameObject TopWallPortion = CreateCube(new Vector3(doorPosition.x, TopPosY, doorPosition.z), new Vector3(doorSize.x, TopScaleY, wallToChange.transform.localScale.z), newWall.transform);
        TopWallPortion.name = "TopWall";

        // destroy old wall
        DestroyImmediate(wallToChange);
    }
    GameObject CreateCube(Vector3 position, Vector3 size, Transform p)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = p;
        cube.transform.localPosition = position;
        cube.transform.rotation = p.rotation;
        cube.transform.localScale = size;
        return cube;
    }
}
