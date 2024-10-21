using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _instanceLock = new object();
    private static bool _quiting = false;

    public static T instance
    {
        get
        {
            lock (_instanceLock)
            {
                if (_instance == null && !_quiting)
                {

                    _instance = GameObject.FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).ToString());
                        _instance = go.AddComponent<T>();
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }

        }

    }

    protected virtual void Awake()
    {

        if (_instance == null)
        {
            _instance = gameObject.GetComponent<T>();
        }
        else if (_instance.GetInstanceID() != GetInstanceID())
        {
            Destroy(gameObject);
            throw new System.Exception(string.Format("Instance of {0} already exists, removing" +
                " {1}", GetType().FullName, ToString()));
        }

        Init();
    }

    protected virtual void Init()
    {

    }

    protected void OnApplicationQuit()
    {
        _quiting = true;
    }
}