using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T _Instance;
    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindAnyObjectByType<T>();

                if (_Instance == null)
                {
                    throw new Exception("There is no instance of " + typeof(T).Name + " in the scene.");
                }
            }

            return _Instance;
        }
    }
}
