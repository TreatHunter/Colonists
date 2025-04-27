using UnityEngine;

namespace MapScene.Map.MapGenerator
{
    public class MapGenerationConfigSingleton : Singleton<MapGenerationConfigSingleton>
    {
        [SerializeField] public Type type;
    }

    public enum Type
    {
        Random,
        FromFile,
        DirectImport
    }
}