using UnityEngine;

namespace FarmerDemo
{
    public abstract class MonoBehaviourSingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;
        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = this as T;
            else
                Destroy(gameObject);
        }
    }
}
