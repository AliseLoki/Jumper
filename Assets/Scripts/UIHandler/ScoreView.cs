using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private PlatformsController _platformsController;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _platformsController.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _platformsController.ScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(float score)
    {
        _text.text = score.ToString();
    }
}
