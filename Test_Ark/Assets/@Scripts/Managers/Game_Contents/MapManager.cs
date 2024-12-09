using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    // Grid 방식으로 체크 (Cell Pos)

    public GameObject Map {  get; private set; }
    public string MapName { get; private set; }
    public Grid CellGrid { get; private set; }


}
