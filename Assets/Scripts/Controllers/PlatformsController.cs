using System;
using UnityEngine;

public class PlatformsController : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    [SerializeField] private Platform _currentPlatform;
    [SerializeField] private Platform _firstPlatform;

    [SerializeField] private Player _player;
    [SerializeField] private ObjectsPool _objectsPool;

    [SerializeField] private float _minOffset = 5;
    [SerializeField] private float _maxOffset = 8;

    private int _platformsAmount;

    public event Action<bool> PlatformHasSpawnedOnAxisX;

    public int PlatformsAmount => _platformsAmount;

    private void OnEnable()
    {
        _currentPlatform = _firstPlatform;
        _player.PlayerHasJumped += OnPlayerHasJumped;
    }

    private void Start()
    {
        TransformPlatformOnAxis(true, CalculateOffset(), 0);
    }

    private void OnDisable()
    {
        _player.PlayerHasJumped -= OnPlayerHasJumped;
    }

    private void OnPlayerHasJumped()
    {
        var random = UnityEngine.Random.Range(0, 2);

        if (random == 0) TransformPlatformOnAxis(true, CalculateOffset(), 0);
        else TransformPlatformOnAxis(false, 0, CalculateOffset()); 
    }

    private void TransformPlatformOnAxis(bool isAxisX, float x, float z)
    {
        TakePlatformFromPool(x, z);
        PlatformHasSpawnedOnAxisX?.Invoke(isAxisX);
        _platformsAmount++;
    }
   
    private void TakePlatformFromPool(float offsetX, float offsetZ)
    {
        Vector3 newPosition = new Vector3(_currentPlatform.transform.position.x + offsetX,
           _currentPlatform.transform.position.y, _currentPlatform.transform.position.z + offsetZ);

        Platform platform = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.PlatformToPool) as Platform;
        platform.transform.position = newPosition;
        platform.gameObject.SetActive(true);
        _currentPlatform = platform;
    }

    private float CalculateOffset()
    {
        return UnityEngine.Random.Range(_minOffset, _maxOffset);
    }
}
