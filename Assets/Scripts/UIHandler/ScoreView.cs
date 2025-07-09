using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private PlatformsController _platformsController;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _platformsController.PlatformHasSpawnedOnAxisX += OnPlatformSpawned;
    }

    private void OnDisable()
    {
        _platformsController.PlatformHasSpawnedOnAxisX -= OnPlatformSpawned;
    }

    private void OnPlatformSpawned(bool isSpawned)
    {
        _text.text = "Score: " + _platformsController.PlatformsAmount.ToString();
    }
}
