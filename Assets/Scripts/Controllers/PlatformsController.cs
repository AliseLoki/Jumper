using System;
using UnityEngine;

public class PlatformsController : MonoBehaviour
{
    [SerializeField] private Platform _firstPlatform;
    [SerializeField] private Platform _currentPlatform;
    [SerializeField] private Platform _previousPlatform;

    [SerializeField] private ObjectsPool _objectsPool;

    [SerializeField] private float _minOffset = 5;
    [SerializeField] private float _maxOffset = 8;

    private float _score;
    private Vector3 _centerBetweenCurrentAndPreviousPlatform;

    public event Action<bool> PlatformHasSpawnedOnAxisX;
    public event Action<float> ScoreChanged;

    public Vector3 Center => _centerBetweenCurrentAndPreviousPlatform;

    private void OnEnable()
    {
        _currentPlatform = _firstPlatform;
    }

    private void Start()
    {
        TransformPlatformOnAxis(true, CalculateOffset(), 0);
    }

    private void OnDisable()
    {
        _currentPlatform.ScoreChanged -= OnScoreChanged;
        _currentPlatform.PlayerJumpedOnPlatform -= OnPlayerHasJumped;
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
    }

    private void TakePlatformFromPool(float offsetX, float offsetZ)
    {
        Vector3 newPosition = new Vector3(_currentPlatform.transform.position.x + offsetX,
           _currentPlatform.transform.position.y, _currentPlatform.transform.position.z + offsetZ);

        Platform platform = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.PlatformToPool) as Platform;
        platform.transform.position = newPosition;
        platform.gameObject.SetActive(true);
        _previousPlatform = _currentPlatform;
        _currentPlatform = platform;
        _currentPlatform.ScoreChanged += OnScoreChanged;
        _currentPlatform.PlayerJumpedOnPlatform += OnPlayerHasJumped;
        _centerBetweenCurrentAndPreviousPlatform = CalculateDistanceBetweenPlatforms();
    }

    private Vector3 CalculateDistanceBetweenPlatforms()
    {
        return (_currentPlatform.transform.position + _previousPlatform.transform.position) / 2;
    }

    private float CalculateOffset()
    {
        return UnityEngine.Random.Range(_minOffset, _maxOffset);
    }

    private void OnScoreChanged(float score)
    {
        _score += score;
        ScoreChanged?.Invoke(_score);
    }
}
