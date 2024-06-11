using UnityEngine;

namespace ATGC.GEO
{
    /// <summary>
    /// µ¥Àý½Å±¾
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingletonNoDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindFirstObjectByType<T>();
                    if (instance == null)
                    {
                        GameObject gameObject = GameObject.Find(typeof(T).Name);
                        if (gameObject == null)
                        {
                            gameObject = new GameObject(typeof(T).Name);
                        }
                        instance = gameObject.GetComponent<T>();
                    }
                }
                return instance;
            }
            set { instance = value; }
        }

        public virtual void Awake()
        {
            //DontDestroyOnLoad(gameObject);
            if (!instance)
            {
                instance = gameObject.GetComponent<T>();
            }
        }

    }
}
