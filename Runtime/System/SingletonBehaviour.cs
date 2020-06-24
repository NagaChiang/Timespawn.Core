using UnityEngine;

namespace Timespawn.Core.System
{
    [DisallowMultipleComponent]
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T PrivateInstance;

        public static T Instance()
        {
            if (!PrivateInstance)
            {
                PrivateInstance = new GameObject(typeof(T).Name).AddComponent<T>();
            }

            return PrivateInstance;
        }

        protected virtual void Awake()
        {
            if (PrivateInstance)
            {
                Debug.LogWarningFormat("Singleton instance of {0} already exists. Destroy self.", GetType().Name);
                Destroy(this);
            }
            else
            {
                PrivateInstance = this as T;
            }
        }

        protected void OnDestroy()
        {
            if (PrivateInstance == this)
            {
                PrivateInstance = null;
            }
        }
    }
}