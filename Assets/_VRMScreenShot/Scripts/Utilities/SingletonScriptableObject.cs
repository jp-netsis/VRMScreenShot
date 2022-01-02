using UnityEngine;

namespace jp.netsis.VRMScreenShot.Utilities
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    var filePath = typeof(T).Name;
                    m_Instance = Resources.Load(filePath) as T;
                }

                return m_Instance;
            }
        }
    }
}