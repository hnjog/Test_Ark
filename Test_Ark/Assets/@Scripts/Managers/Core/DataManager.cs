using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<key, value> // Generic
{
    Dictionary<key, value> MakeDict();

    // Validate() : 예정
    // 런타임 중 데이터를 확인하는 방식이 위험할 수 있으므로
    // 데이터 변질 여부 등을 첫 로딩 때 검사하기 위함
}
public class DataManager
{
    // 필드 겸 프로퍼티 (변수와 속성) 역할을 겸함

    public void Init()
    {

    }

    private Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        // 특정 경로의 Text 에셋
        // Addressable 방식으로 load
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(path);

        // Newton Json을 convert를 이용하여
        // 해당 클래스 방식으로 캐스팅하여 return 받음
        return JsonConvert.DeserializeObject<Loader>(textAsset.text);
    }
}
