using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private List<Interactable> _coins;
    [SerializeField] private List<Interactable> _platforms;

    [SerializeField] private List<PlatformView> _platformsViews;

    [SerializeField] private Interactable _coinToPool;
    [SerializeField] private Interactable _platformToPool;

    [SerializeField] private Fabrica _fabrica;

    [SerializeField] private int _amountToPool = 5;
    // сделать отдельные для платформы и даймондов

    public Interactable CoinToPool => _coinToPool;
    public Interactable PlatformToPool => _platformToPool;

    public List<Interactable> Coins => _coins;
    public List<Interactable> Platforms => _platforms;

    public List<PlatformView> PlatformViews => _platformsViews;

    void Awake()
    {
        CreatePrefabsInPool(_coins, _coinToPool);
        CreatePlatformWithView(_platforms, _platformToPool);
    }

    public Interactable GetPooledObject(List<Interactable> pool, Interactable prefab)
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy) return pool[i];
        }

        if (prefab as Platform)
        {
            var newPlatform = Create(pool, prefab);
            CreatePlatformView(newPlatform.transform);
            return newPlatform;
        }
        else
        {
            return Create(pool, prefab);
        }
    }

    private void CreatePlatformWithView(List<Interactable> pool, Interactable prefab)
    {
        Interactable platform;

        for (int i = 0; i < _amountToPool; i++)
        {
            platform = Create(pool, prefab);
            CreatePlatformView(platform.transform);
        }
    }

    private PlatformView CreatePlatformView(Transform transform)
    {
        int index = Random.Range(0, _platformsViews.Count);
        PlatformView view = _fabrica.CreatePrefab(_platformsViews[index], Quaternion.identity, transform);
        return view;
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
        Interactable template = _fabrica.CreatePrefab(prefab, Quaternion.identity, transform);
        template.gameObject.SetActive(false);
        pool.Add(template);
        return template;
    }
}