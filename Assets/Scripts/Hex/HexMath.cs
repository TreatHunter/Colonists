using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMath
{
    public static float OuterRadius(float hexSize)
    {

        return hexSize;
    }

    public static float InnerRadius(float hexSize)
    {
        return hexSize * 0.866025404f;
    }

    public static Vector3 Corner(float hexSize, HexOrientation hexOrientation, int index)
    {

        float angle = -60f * index;
        if (hexOrientation == HexOrientation.PointyTop)
        {
            angle += -30f;
        }

        return new Vector3(hexSize * Mathf.Cos(angle * Mathf.Deg2Rad),
            0f,
            hexSize * Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    public static Vector3[] Corners(float hexSize, HexOrientation hexOrientation)
    {
        Vector3[] corners = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            corners[i] = Corner(hexSize, hexOrientation, i);

        }

        return corners;
    }

    public static Vector3[] Corners(float hexSize, HexOrientation hexOrientation, Vector3 centerPosition)
    {
        Vector3[] corners = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            corners[i] = centerPosition + Corner(hexSize, hexOrientation, i);

        }

        return corners;
    }

    public static Vector3 Center(float hexSize, int x, int z, HexOrientation hexOrientation)
    {
        Vector3 centerPosition;
        if (hexOrientation == HexOrientation.PointyTop)
        {
            centerPosition.x = (x + z * 0.5f - z / 2) * (InnerRadius(hexSize) * 2f);
            centerPosition.y = 0f;
            centerPosition.z = z * (OuterRadius(hexSize) * 1.5f);

        }
        else
        {
            centerPosition.x = x * (OuterRadius(hexSize) * 1.5f);
            centerPosition.y = 0f;
            centerPosition.z = (z + x * 0.5f - x / 2) * (InnerRadius(hexSize) * 2f);
        }

        return centerPosition;
    }

    public static Vector3[] AllVertices(float hexSize, HexOrientation hexOrientation, int x, int z)
    {
        Vector3[] allVertices = new Vector3[7];
        allVertices[0] = Center(hexSize, x, z, hexOrientation);
        HexMath.Corners(hexSize, hexOrientation, allVertices[0]).CopyTo(allVertices, 1);
        return allVertices;
    }
}
