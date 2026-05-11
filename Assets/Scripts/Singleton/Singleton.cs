using UnityEngine;

// T là kiểu của class kế thừa từ Singleton này
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public bool dontDestroyOnLoad = true; // Mặc định giữ đối tượng này qua các scene
    

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();

                if (_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            // Giữ đối tượng này không bị hủy khi load scene mới
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}