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

    public static Vector2 CubeToAxial(Vector3 cube)
    {
        return new Vector2(cube.x, cube.y);
    }

    public static Vector2 OffsetToAxial(int x, int z, HexOrientation hexOrientation)
    {
        if (hexOrientation == HexOrientation.PointyTop)
        {
            return OffsetToAxialPointy(x, z);
        }
        else
        {
            return OffsetToAxialFlat(x, z);
        }
    }

    private static Vector2 OffsetToAxialFlat(int col, int row)
    {
        var q = col;
        var r = row - (col - (col & 1)) / 2;
        return new Vector2(q, r);
    }

    private static Vector2 OffsetToAxialPointy(int col, int row)
    {
        var q = col - (row - (row & 1)) / 2;
        var r = row;
        return new Vector2(q, r);
    }

    public static Vector2 CubeToOffset(int x, int y, int z, HexOrientation hexOrientation)
    {
        if (hexOrientation == HexOrientation.PointyTop)
        {
            return CubeToOffsetPointy(x, y, z);
        }
        else
        {
            return CubeToOffsetFlat(x, y, z);
        }
    }

    public static Vector2 CubeToOffset(Vector3 offsetCoordinate, HexOrientation hexOrientation)
    {
        return CubeToOffset((int)offsetCoordinate.x, (int)offsetCoordinate.y, (int)offsetCoordinate.z, hexOrientation);
    }

    private static Vector2 CubeToOffsetFlat(int x, int y, int z)
    {
        return new Vector2(x, y + (x - (x & 1)) / 2);
    }

    private static Vector2 CubeToOffsetPointy(int x, int y, int z)
    {
        return new Vector2(x + (y - (y & 1)) / 2, y);
    }

    private static Vector3 CubeRound(Vector3 fractionalCube)
    {
        int rx = Mathf.RoundToInt(fractionalCube.x);
        int ry = Mathf.RoundToInt(fractionalCube.y);
        int rz = Mathf.RoundToInt(fractionalCube.z);

        float xDiff = Mathf.Abs(rx - fractionalCube.x);
        float yDiff = Mathf.Abs(ry - fractionalCube.y);
        float zDiff = Mathf.Abs(rz - fractionalCube.z);

        if (xDiff > yDiff && xDiff > zDiff)
        {
            rx = -ry - rz;
        }
        else if (yDiff > zDiff)
        {
            ry = -rx - rz;
        }
        else
        {
            rz = -rx - ry;
        }
        return new Vector3(rx, ry, rz);
    }

    public static Vector3 AxialToCube(Vector2 axial)
    {
        return new Vector3(axial.x, axial.y, -axial.x - axial.y);
    }

    private static Vector2 AxialRound(Vector2 fractionalAxial)
    {
        return CubeToAxial(CubeRound(AxialToCube(fractionalAxial)));
    }

    public static Vector2 CoordinateToAxial(float x, float z, float hexSize, HexOrientation hexOrientation)
    {
        if (hexOrientation == HexOrientation.PointyTop)
        {
            return CoordinateToAxialPointy(x, z, hexSize);
        }
        else
        {
            return CoordinateToAxialFlat(x, z, hexSize);
        }
    }

    private static Vector2 CoordinateToAxialPointy(float x, float z, float hexSize)
    {
        float ax = (Mathf.Sqrt(3) / 3 * x - 1f / 3 * z) / hexSize;
        float ay = (2f / 3 * z) / hexSize;
        Debug.Log("not rounded hex axial coordinate : x =" + (ax + 0.5) + ", y = " + (ay + 0.6));
        return AxialRound(new Vector2(ax, ay));
    }

    private static Vector2 CoordinateToAxialFlat(float x, float z, float hexSize)
    {
        float ax = (2f / 3 * x) / hexSize;
        float ay = (Mathf.Sqrt(3) / 3 * z - 1f / 3 * x) / hexSize;
        Debug.Log("not rounded hex axial coordinate : x =" + (ax + 0.5) + ", y = " + (ay + 0.6));
        return AxialRound(new Vector2(ax, ay));
    }

    public static Vector2 CoordinateToOffset(float x, float z, float hexSize, HexOrientation hexOrientation)
    {
        return CubeToOffset(AxialToCube(CoordinateToAxial(x, z, hexSize, hexOrientation)), hexOrientation);
    }
}