using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; } = null;

    /// <summary>
    ///  Awake()  base.Awake()
    /// </summary>
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(Instance);
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    /// <summary>
    ///  OnDestroy()  base.OnDestroy()
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}

public abstract class Singleton<T> where T : new()
{
    private static T s_instance;
    public static T Instance => s_instance ??= new T();
}
