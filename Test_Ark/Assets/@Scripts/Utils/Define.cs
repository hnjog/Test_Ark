using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum EScene
    {
        Unknown,
        TitleScene,
        GameScene,
    }

    public enum EUIEvent
    {
        Click,
        PointerDown,
        PointerUp,
        Drag,
    }

    public enum EJoystickState
    {
        PointerDown,
        PointerUp,
        Drag,
    }

    public enum ESound
    {
        Bgm,
        Effect,
        Max,
    }

    public enum ETileType // 타일의 지형 타입
    {
        None, // 궁중섬
        Dessert,
        Field,
        Ice,
        Lava,
    }

    public enum ETileEventType
    {
        None,
        Cave, // 동굴
        Island, // 섬
        Ruins,// 유적
        Temple, // 신전
    }

}
