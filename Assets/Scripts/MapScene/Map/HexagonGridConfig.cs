namespace MapScene.Map
{
    public class HexagonGridConfig
    {
        public readonly int GridWidth;
        public readonly int GridHeight;
        public readonly float HexagonsSizeAsOuterRadius;
        public readonly HexagonsOrientation HexagonsOrientation;

        public HexagonGridConfig(float hexagonsSizeAsOuterRadius, HexagonsOrientation hexagonsOrientation, int gridHeight, int gridWidth)
        {
            HexagonsSizeAsOuterRadius = hexagonsSizeAsOuterRadius;
            HexagonsOrientation = hexagonsOrientation;
            GridHeight = gridHeight;
            GridWidth = gridWidth;
        }
    }
    
    public enum HexagonsOrientation
    {
        FlatTop,
        PointyTop
    }
}