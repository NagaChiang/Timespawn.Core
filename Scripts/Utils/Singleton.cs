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
            if (ProtectedInstance == null)
            {
                GameObject obj = new GameObject(typeof(T).Name);
                ProtectedInstance = obj.AddComponent<T>();
            }

            return ProtectedInstance;
        }
    }
}