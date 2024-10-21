using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexGridMeshGenerator))]
public class HexGridMeshGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        HexGridMeshGenerator hexGridMeshGenerator = (HexGridMeshGenerator)target;

        if (GUILayout.Button("Generate hex mesh"))
        {
            hexGridMeshGenerator.GenerateGridMesh();
        }

        if (GUILayout.Button("Clear hex mesh"))
        {
            hexGridMeshGenerator.ClearGridMesh();
        }
    }
}
