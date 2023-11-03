using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AutoBuild : EditorWindow
{
    private int numWallsInRoom;
    private float wallThickness = 0.25f;
    private float wallHeight = 4f;
    private float roomSize = 5f;

    private List<GameObject> rooms = new List<GameObject>();

    [MenuItem("Window/Building Generator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AutoBuild));
    }
    private void OnGUI()
    {
        GUILayout.Label("Building Generator", EditorStyles.whiteLargeLabel);

        wallThickness = EditorGUILayout.FloatField("Wall Thickness", wallThickness);

        wallHeight = EditorGUILayout.FloatField("Wall Height", wallHeight);

        roomSize = EditorGUILayout.FloatField("Room Size", roomSize);

        numWallsInRoom = EditorGUILayout.IntField("Num walls in the room", numWallsInRoom);
        if (numWallsInRoom < 3) numWallsInRoom = 3;

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Generate Room"))
        {
            GenerateRoom();
        }
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Delete Last Room"))
        {
            DeleteLast();
        }
    }
    void GenerateRoom()
    {
        // make the Room
        GameObject room = new GameObject("Room " + (rooms.Count + 1)); // Creating GameObject with name
        room.transform.position = 0.5f * wallHeight * Vector3.up;
        Room_AutoBuild r = room.AddComponent<Room_AutoBuild>();
        rooms.Add(room);

        float angleStep = 360f / numWallsInRoom;
        // make the walls
        for (int i = 0; i < numWallsInRoom; i++)
        {
            GameObject wall = CreateWall("wall_" + r.walls.Count, room.transform);
            GameObject Target = CreateTarget("target_" + r.wallTargets.Count, room.transform);

            r.walls.Add(wall); // Add the wall to the current room's wall list
            r.wallTargets.Add(Target);

            float angle = i * angleStep * Mathf.Deg2Rad; // Convert angle to radians for trigonometric functions

            // Calculate the position around the circle
            Vector3 position = new Vector3(
                Mathf.Cos(angle) * roomSize,
                0f, // Assuming you want the objects to be at the same height
                Mathf.Sin(angle) * roomSize
            );

            // Place the child at the calculated position
            Target.transform.localPosition = position;
        }
    }
    GameObject CreateWall(string name, Transform p)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject w = new();
        cube.transform.parent = w.transform;
        cube.transform.localPosition = Vector3.zero;
        w.transform.localScale = new Vector3(roomSize, wallHeight, wallThickness);
        w.transform.position = p.transform.position;
        w.name = name;
        w.transform.parent = p.transform;
        return w;
    }
    GameObject CreateTarget(string name, Transform p)
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.localScale = new Vector3(wallThickness, wallHeight * 0.5f, wallThickness);
        cylinder.transform.position = p.transform.position;
        cylinder.transform.parent = p;
        cylinder.name = name;
        return cylinder;
    }
    void DeleteLast()
    {
        if (rooms.Count > 0)
        {
            GameObject temp = rooms[^1];
            rooms.Remove(temp);
            DestroyImmediate(temp);
        }
    }
}