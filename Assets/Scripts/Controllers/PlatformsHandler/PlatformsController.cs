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

    private Vector3 _centerBetweenCurrentAndPreviousPlatform;

    private PlatformsScoreController _scoreController;

    public event Action<bool> PlatformHasSpawnedOnAxisX;

    public PlatformsScoreController ScoreController =>_scoreController;
   
    public Vector3 Center => _centerBetweenCurrentAndPreviousPlatform;

    private void OnEnable()
    {
        _currentPlatform = _firstPlatform;
    }

    private void Awake()
    {
        // тоже продумать в эвейке создаетс€ скор онтроллер, в старте скор¬ью на него подписываетс€
        // после добавлени€ бутс“рэп может можно будет покрасивее это сделать
        _scoreController = new PlatformsScoreController();
    }

    private void Start()
    {
        TransformPlatformOnAxis(true, CalculateOffsetForPlatformsPosition(), 0);
    }

    private void OnDisable()
    {
        _currentPlatform.PlayerLandedOnPlatform -= OnPlayerHasLandedOnPlatform;
    }

    private void OnPlayerHasLandedOnPlatform(float bonusScore)
    {
        _scoreController.OnScoreChanged(bonusScore);

        // просчитываем вариант
        var random = UnityEngine.Random.Range(0, 2);

        if (random == 0) TransformPlatformOnAxis(true, CalculateOffsetForPlatformsPosition(), 0);
        else TransformPlatformOnAxis(false, 0, CalculateOffsetForPlatformsPosition());
    }

    private void TransformPlatformOnAxis(bool isAxisX, float x, float z)
    {
        TakePlatformFromPool(x, z);
        PlatformHasSpawnedOnAxisX?.Invoke(isAxisX);
    }

    private void TakePlatformFromPool(float offsetX, float offsetZ)
    {
        Platform platform = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.PlatformToPool) as Platform;
        platform.transform.position = CalculatePlatformPosition(offsetX,offsetZ);
        // InitView


        platform.gameObject.SetActive(true);
        _previousPlatform = _currentPlatform;
        _currentPlatform = platform;
        //
        _currentPlatform.PlayerLandedOnPlatform += OnPlayerHasLandedOnPlatform;
        _centerBetweenCurrentAndPreviousPlatform = CalculateDistanceBetweenPlatforms();
    }

    private Vector3 CalculatePlatformPosition(float offsetX, float offsetZ)
    {
        return new Vector3(_currentPlatform.transform.position.x + offsetX,
           _currentPlatform.transform.position.y, _currentPlatform.transform.position.z + offsetZ);
    }

    private Vector3 CalculateDistanceBetweenPlatforms()
    {
        return (_currentPlatform.transform.position + _previousPlatform.transform.position) / 2;
    }

    private float CalculateOffsetForPlatformsPosition()
    {
        return UnityEngine.Random.Range(_minOffset, _maxOffset);
    }
}
