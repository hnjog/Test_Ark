using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class ResourceManager
{
    private Dictionary<string, Object> _resources = new Dictionary<string, Object>();
    private Dictionary<string, AsyncOperationHandle> _handles = new Dictionary<string, AsyncOperationHandle>();

    #region Load Resource
    public T Load<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))
            return resource as T;

        return null;
    }

    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>(key);
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab : {key}");
            return null;
        }

        // Addressable의 Instance 방식을 사용하지 않는 이유는
        // refCount에 영향을 주기에 Clear 쪽에서도 같은 방식을 이용해야 함
        // -> 제거를 할때 Addressable로 '사용되었는지'를 파악하는 여부가 까다롭기에
        // 일반적인 Unity 방식으로 사용

        if (pooling)
            return Managers.Pool.Pop(prefab);

        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        if (Managers.Pool.Push(go))
            return;

        Object.Destroy(go);
    }

    #endregion

    #region Addressable
    private void LoadAsync<T>(string key, Action<T> callback = null) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))
        {
            // Delegate 호출
            callback?.Invoke(resource as T);
            return;
        }

        string loadKey = key;
        if (key.Contains(".sprite"))
            loadKey = $"{key}[{key.Replace(".sprite", "")}]";

        var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
        asyncOperation.Completed += (op) =>
        {
            _resources.Add(key, op.Result);
            _handles.Add(key, asyncOperation);
            callback?.Invoke(op.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : Object
    {
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (op) =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                // .sprite를 붙여 다른 타입으로 인식하지 않도록
                // 캐스팅을 바꾸어줌 (Addressable의 버그를 예방)
                if (result.PrimaryKey.Contains(".sprite"))
                {
                    LoadAsync<Sprite>(result.PrimaryKey, (obj) =>
                    {
                        loadCount++;
                        callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                    });
                }
                else
                {
                    LoadAsync<T>(result.PrimaryKey, (obj) =>
                    {
                        loadCount++;
                        callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                    });
                }
            }

        };
    }

    public void Clear()
    {
        _resources.Clear();

        foreach (var handle in _handles)
            Addressables.Release(handle);

        _handles.Clear();
    }
    #endregion
}
