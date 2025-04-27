using UnityEngine;

namespace MapScene.Map.Hexagon
{
    public static class HexagonsMathUtils
    {
        public static float OuterRadius(float hexSize)
        {

            return hexSize;
        }

        public static float InnerRadius(float hexSize)
        {
            return hexSize * 0.866025404f;
        }

        public static Vector3 Corner(float hexSize, HexagonsOrientation hexagonsOrientation, int index)
        {

            float angle = -60f * index;
            if (hexagonsOrientation == HexagonsOrientation.PointyTop)
            {
                angle += -30f;
            }

            return new Vector3(hexSize * Mathf.Cos(angle * Mathf.Deg2Rad),
                0f,
                hexSize * Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        public static Vector3[] Corners(float hexSize, HexagonsOrientation hexagonsOrientation)
        {
            Vector3[] corners = new Vector3[6];
            for (int i = 0; i < 6; i++)
            {
                corners[i] = Corner(hexSize, hexagonsOrientation, i);

            }

            return corners;
        }

        public static Vector3[] Corners(float hexSize, HexagonsOrientation hexagonsOrientation, Vector3 centerPosition)
        {
            Vector3[] corners = new Vector3[6];
            for (int i = 0; i < 6; i++)
            {
                corners[i] = centerPosition + Corner(hexSize, hexagonsOrientation, i);

            }

            return corners;
        }

        public static Vector3 Center(float hexSize, int x, int z, HexagonsOrientation hexagonsOrientation)
        {
            Vector3 centerPosition;
            if (hexagonsOrientation == HexagonsOrientation.PointyTop)
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

        public static Vector3[] AllVertices(float hexSize, HexagonsOrientation hexagonsOrientation, int x, int z)
        {
            Vector3[] allVertices = new Vector3[7];
            allVertices[0] = Center(hexSize, x, z, hexagonsOrientation);
            HexagonsMathUtils.Corners(hexSize, hexagonsOrientation, allVertices[0]).CopyTo(allVertices, 1);
            return allVertices;
        }
    }
}