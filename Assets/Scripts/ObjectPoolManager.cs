using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPoolManager : MonoBehaviour, IGameModule
{
    [Serializable]
    public class PooledObject
    {
        public string name;
        public GameObject prefab;
        public int poolSize;
    }

    public List<PooledObject> objectsToPool = new List<PooledObject>();

    public bool IsInitialized { get { return _isInitialized; } }
    private bool _isInitialized = false;

    private readonly Dictionary<string, List<GameObject>> _objectPoolByName = 
                                    new Dictionary<string, List<GameObject>>();

    #region Interface Implementation
    public IEnumerator LoadModule()
    {
        Debug.Log("Loading Object Pool");
        InitializePool();
        yield return new WaitUntil(()=> { return IsInitialized; });

        ServiceLocator.Register<ObjectPoolManager>(this);
        yield return null;
    }
    #endregion

    private void InitializePool()
    {
        GameObject PoolManagerGO = new GameObject("Object Pool");
        PoolManagerGO.transform.SetParent(GameObject.FindWithTag("Services").transform);

        foreach(PooledObject poolObj in objectsToPool)
        {
            if(_objectPoolByName.ContainsKey(poolObj.name) == false)
            {
                GameObject poolGO = new GameObject(poolObj.name);
                poolGO.transform.SetParent(PoolManagerGO.transform);
                _objectPoolByName.Add(poolObj.name, new List<GameObject>());

                for(int i = 0; i<poolObj.poolSize; ++i)
                {
                    GameObject go = Instantiate(poolObj.prefab);
                    go.transform.SetParent(poolGO.transform);
                    go.SetActive(false);
                    _objectPoolByName[poolObj.name].Add(go);
                }
            }
            else
            {
                Debug.LogWarning("Pool Aready contains entry for " + poolObj.name);
            }

            _isInitialized = true;
        }
    }

    public GameObject GetObjectFromPool(string name)
    {
        GameObject ret = null;

        if(_objectPoolByName.ContainsKey(name))
        {
            ret = GetNextObject(name);
        }
        else
        {
            Debug.LogError("No pool exists with name: " + name);
        }
        return ret;
    }

    private GameObject GetNextObject(string name)
    {

        List<GameObject> pooledObjects = _objectPoolByName[name];

        foreach(GameObject go in pooledObjects)
        {
            if(go == null)
            {
                Debug.LogError("Pooled Object is NULL");
                continue;
            }

            if(go.activeInHierarchy)
            {
                continue;
            }
            else
            {
                return go;
            }
        }

        Debug.LogError("Object Pool depleted: No unused objects to Return from Pool " + name);
        return null;
    }

    public void RecycleObject(GameObject go)
    {
        go.SetActive(false);
    }

}
