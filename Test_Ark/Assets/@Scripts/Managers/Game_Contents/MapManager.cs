using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    // Grid 방식으로 체크 (Cell Pos)
    // 생각해볼 점
    // Map 을 직접 랜덤으로 만들고 싶음
    // 그런데 이 방식은 프리팹으로 만드는 게 맞나?
    // - 프리팹을 만들고 그 안에서 TileManager 등을 넣어두고
    // 해당 매니저가 타일을 랜덤으로 배치하도록 한다
    // - 그리고 이 Map Manager에서는 오브젝트들의 이동 및 상호작용을 우선 (타일 데이터가 필요하면 TileManager에게)
    //

    public GameObject Map {  get; private set; }
    public string MapName { get; private set; }
    public Grid CellGrid { get; private set; }

    public void LoadMap(string mapName)
    {

    }
}
