using System.Collections.Generic;
using UnityEngine;

public class CollectablesController : MonoBehaviour
{
    [SerializeField] private float _collectablesPosYOffset = 4; 
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
        // вынести шанс в инспектор
        if (chance == 0)
        {
             SpawnTemplate(_pool.Coins, _pool.CoinToPool);          
        }
    }

    private void SpawnTemplate(List<Interactable> pool, Interactable template)
    {
        var newTemplate = _pool.GetPooledObject(pool, template);
        newTemplate.transform.position = CalculateCollectablePosition();
        newTemplate.gameObject.SetActive(true);
    }

    private Vector3 CalculateCollectablePosition()
    {
        return new Vector3(_platformsController.Center.x, _platformsController.Center.y + _collectablesPosYOffset,
            _platformsController.Center.z);
    }
}