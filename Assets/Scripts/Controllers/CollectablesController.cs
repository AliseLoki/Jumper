using System.Collections.Generic;
using UnityEngine;

public class CollectablesController : MonoBehaviour
{
    [SerializeField] private ObjectsPool _pool;
    [SerializeField] private PlatformsController _platformsController;

    private void OnEnable()
    {
        _platformsController.PlatformHasSpawnedOnAxisX += OnPlatformHasSpawned;
    }

    private void OnDisable()
    {
        _platformsController.PlatformHasSpawnedOnAxisX -= OnPlatformHasSpawned;
    }

    private void OnPlatformHasSpawned(bool isTrue)
    {
        int chance = Random.Range(0, 2);

        if (chance == 0)
        {
            int secondChance = Random.Range(0, 2);

            if (secondChance == 0) SpawnTemplate(_pool.Coins, _pool.CoinToPool);
            else SpawnTemplate(_pool.Crystalls, _pool.CrystalToPool);
        }
    }

    private void SpawnTemplate(List<Interactable> pool, Interactable template)
    {
        var newTemplate = _pool.GetPooledObject(pool, template);

        Vector3 newPos = new Vector3(_platformsController.Center.x, _platformsController.Center.y + 4,
            _platformsController.Center.z);

        newTemplate.transform.position = newPos;
        newTemplate.gameObject.SetActive(true);
    }
}
