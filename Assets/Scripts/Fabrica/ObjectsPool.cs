using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private List<Interactable> _crystalls;
    [SerializeField] private List<Interactable> _coins;
    [SerializeField] private List<Interactable> _platforms;

    [SerializeField] private Interactable _crystalToPool;
    [SerializeField] private Interactable _coinToPool;
    [SerializeField] private Interactable _platformToPool;

    [SerializeField] private int _amountToPool = 5;

    public Interactable CrystalToPool => _crystalToPool;
    public Interactable CoinToPool => _coinToPool;
    public Interactable PlatformToPool => _platformToPool;

    public List<Interactable> Crystalls => _crystalls;
    public List<Interactable> Coins => _coins;
    public List<Interactable> Platforms => _platforms;

    void Awake()
    {
        CreatePrefabsInPool(_crystalls, _crystalToPool);
        CreatePrefabsInPool(_coins, _coinToPool);
        CreatePrefabsInPool(_platforms, _platformToPool);
    }

    public Interactable GetPooledObject(List<Interactable> pool, Interactable prefab)
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i];
            }
        }

        return Create(pool, prefab);
    }

    private void CreatePrefabsInPool(List<Interactable> pool, Interactable prefab)
    {
        Interactable template;

        for (int i = 0; i < _amountToPool; i++)
        {
            template = Create(pool, prefab);
        }
    }

    private Interactable Create(List<Interactable> pool, Interactable prefab)
    {
        Interactable template = Instantiate(prefab, transform);
        template.gameObject.SetActive(false);
        pool.Add(template);
        return template;
    }
}
