using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

public class HexTile : Tile
{
    [Space]    [Space]    [Header("For Prefab")]
    public Define.ETileType tileType;
    public Define.ETileEventType tileEventType;
    public int DataId;
    public string Name;
}
}
