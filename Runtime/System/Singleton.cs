namespace Timespawn.Core.System
{
    public abstract class Singleton<T> where T : new()
    {
        private static T PrivateInstance;

        public static T Instance()
        {
            if (PrivateInstance == null)
            {
                PrivateInstance = new T();
            }

            return PrivateInstance;
        }
    }
}