using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [field: SerializeField] public HexOrientation Orientation {  get; private set; }
    [field: SerializeField] public int Width { get; private set; }
    [field: SerializeField] public int Height { get; private set; }
    [field: SerializeField] public float HexSize { get; private set; }
    [field: SerializeField] public GameObject HexPrefab { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello, World!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++) {

                Vector3 centerPosition = HexMath.Center(HexSize, x, z, Orientation) + transform.position;
                Vector3[] corners = HexMath.Corners(HexSize,Orientation);
                for (int corner = 0; corner < corners.Length; corner++) {
                    Gizmos.DrawLine(
                        centerPosition + corners[corner%6],
                        centerPosition + corners[(corner+1) % 6]
                        );

                }
            }
        }
    }
}

public enum HexOrientation
{
    FlatTop,
    PointyTop
}
