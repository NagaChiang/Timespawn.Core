using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timespawn.Core.Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T ProtectedInstance;

        public static T Instance()
        {
            if (!ProtectedInstance)
            {
                GameObject obj = new GameObject(typeof(T).Name);
                ProtectedInstance = obj.AddComponent<T>();
            }

            return ProtectedInstance;
        }

        protected virtual void Awake()
        {
            if (ProtectedInstance)
            {
                Debug.LogAssertionFormat("Singleton instance of {0} already exists. Destroy self.", GetType().Name);
                Destroy(this);
            }
            else
            {
                ProtectedInstance = this as T;
            }
        }
    }
}