using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class HexGridMeshGenerator : MonoBehaviour
{
    [field: SerializeField] public LayerMask gridLayer { get; private set; }
    [field: SerializeField] public HexGrid hexGrid { get; private set; }

    private void Awake()
    {
        if (hexGrid == null)
            hexGrid = GetComponentInParent<HexGrid>();
        if (hexGrid == null)
            Debug.LogError("HexGridMeshGenerator could not fing");
    }

    public void GenerateGridMesh()
    {
        GenerateGridMesh(hexGrid.Width, hexGrid.Height, hexGrid.HexSize, hexGrid.Orientation, gridLayer);

    }

    public void GenerateGridMesh(HexGrid hexGrid, LayerMask layerMask)
    {
        this.hexGrid = hexGrid;
        this.gridLayer = layerMask;
        GenerateGridMesh();
    }

    public void ClearGridMesh()
    {
        if (GetComponent<MeshFilter>().sharedMesh == null)
            return;
        GetComponent<MeshFilter>().sharedMesh.Clear();
        GetComponent<MeshCollider>().sharedMesh.Clear();
    }

    public void GenerateGridMesh(int width, int height, float hexSize, HexOrientation hexOrientation, LayerMask layerMask)
    {
        ClearGridMesh();
        Vector3[] vertices = new Vector3[7 * width * height];
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {

                int centerPositionIndex = (z * width + x) * 7;
                HexMath.AllVertices(hexSize, hexOrientation, x, z).CopyTo(vertices, centerPositionIndex);

            }
        }
        int[] triangles = new int[3 * 6 * width * height];
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int triangle = 0; triangle < 6; triangle++)
                {
                    int cornerIndex = triangle + 2 > 6 ? triangle + 2 - 6 : triangle + 2;
                    int triangleCornerStartIndex = 3 * 6 * (z * width + x) + triangle * 3;
                    int verticeStartIndex = (z * width + x) * 7;

                    triangles[triangleCornerStartIndex] = verticeStartIndex;
                    triangles[triangleCornerStartIndex + 1] = verticeStartIndex + triangle + 1;
                    triangles[triangleCornerStartIndex + 2] = verticeStartIndex + cornerIndex;
                }
            }
        }

        Debug.Log("vert");
        Debug.Log(vertices.Length);
        for (int i = 0; i < vertices.Length; i++)
        {
            Debug.Log(vertices[i]);
        }

        Debug.Log("triangles");
        Debug.Log(triangles.Length);
        for (int i = 0; i < triangles.Length; i++)
        {
            Debug.Log(triangles[i]);
        }
        Mesh mesh = new Mesh();
        mesh.name = "Hex mesh";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
        mesh.RecalculateUVDistributionMetrics();
        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        int gridLayerIndex = GetLayerIndex(layerMask);
        Debug.Log("Layer Index :" + gridLayerIndex);
        gameObject.layer = 6;
    }

    private int GetLayerIndex(LayerMask layerMask)
    {
        int layerMaskValue = layerMask.value;
        Debug.Log("Layer Value :" + layerMaskValue);
        for (int i = 0; i < 32; i++)
        {
            if (((1 << i) & layerMaskValue) != 0)
            {
                Debug.Log("Layer Index loop :" + i);
                return i;
            }
        }
        return 0;
    }
}
