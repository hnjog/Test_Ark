using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    // 게임에 관한 '상태 변경'을 관리할 매니저

    // 플레이어의 Pos 와 어떤 플레이어가 움직였는지에 대한 Delegate
    public event Action<Vector2, Int32> OnPlayerPosChanged;

}
