using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log($"Destroying duplicate instance of {typeof(T).Name} on {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        Debug.Log($"Singleton {typeof(T).Name} assigned to {gameObject.name}");

    }
}