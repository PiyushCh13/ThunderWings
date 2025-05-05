using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance 
    {
        get 
        {
            if (instance == null) 
            {
                new GameObject(typeof(T).ToString()).AddComponent<T>();
            }
            return instance;
        }

    }

    protected virtual void Awake() 
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            Destroy(instance);
        }
    }
}
