using UnityEngine;

public class PlatformsSpawner : MonoBehaviour
{
    [SerializeField] private ObjectsPool _objectsPool;

    //private void TakePlatformFromPool(float offsetX, float offsetZ)
    //{
    //    Platform platform = _objectsPool.GetPooledObject(_objectsPool.Platforms, _objectsPool.PlatformToPool) as Platform;
    //    platform.transform.position = CalculatePlatformPosition(offsetX, offsetZ);
    //    // InitView


    //    platform.gameObject.SetActive(true);
    //    _previousPlatform = _currentPlatform;
    //    _currentPlatform = platform;
    //    //
    //    _currentPlatform.ScoreChanged += _scoreController.OnScoreChanged;
    //    _currentPlatform.PlayerJumpedOnPlatform += OnPlayerHasJumped;
    //    _centerBetweenCurrentAndPreviousPlatform = CalculateDistanceBetweenPlatforms();
    //}
}
