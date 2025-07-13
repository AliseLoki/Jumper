using UnityEngine;

public class Fabrica : MonoBehaviour
{
    public T CreatePrefab<T>(T prefab, Quaternion rotation, Transform parent) where T : MonoBehaviour
    {
        return (T)Instantiate(prefab, parent.transform.position, rotation, parent);
    }
}
