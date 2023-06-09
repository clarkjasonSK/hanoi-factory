using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : MonoBehaviour
{
    private ObjectPool<GameObject> _obj_pool;
    public ObjectPool<GameObject> GameObjectPool
    {
        get { return _obj_pool; }
    }

    public GameObject ObjectTemplate;
    public Transform ObjectTransform;

    public bool CollectionCheck = false;
    public int InitialPoolSize = 35;
    public int MaxPoolSize = 150;

    public bool instantiateOnAwake = true;

    private GameObject tempGameObject;
    private Poolable tempPoolable;
    void Awake()
    {
        if (instantiateOnAwake)
        {
            startPooling();
        }
    }

    public void startPooling()
    {
        _obj_pool = new ObjectPool<GameObject>(generateObject, onGetObject, onReleaseObject, onDestroyObject, CollectionCheck, InitialPoolSize, MaxPoolSize);
    }

    public GameObject generateObject()
    {
        tempGameObject = Instantiate(ObjectTemplate, ObjectTransform);
        tempGameObject.SetActive(false);
        tempPoolable = tempGameObject.GetComponent<Poolable>();

        if (tempPoolable != null)
        {
            tempPoolable.OnInstantiate();
            tempPoolable.SetObjectPool(this);
        }

        return tempGameObject;
    }

    public void onGetObject(GameObject gameObj)
    {
        gameObj.SetActive(true);

        tempPoolable = gameObj.GetComponent<Poolable>();
        if (tempPoolable != null)
        {
            tempPoolable.OnActivate();
        }
    }

    public void onReleaseObject(GameObject gameObj)
    {
        gameObj.SetActive(false);
        tempPoolable = gameObj.GetComponent<Poolable>();
        if (tempPoolable != null)
        {
            tempPoolable.OnDeactivate();
        }

    }

    public void onReturnObjects(List<GameObject> gameObjs)
    {
        foreach (GameObject go in gameObjs)
        {
            _obj_pool.Release(go);
        }
    }

    public void onDestroyObject(GameObject gameObj)
    {
        Destroy(gameObj);
        //Debug.Log("GameObject cast to the shadow realm");
    }
}
