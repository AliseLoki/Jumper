using System;

public class PlatformsScoreController 
{
    private float _score;

    public event Action<float> ScoreChanged;

    public void OnScoreChanged(float bonusScore)
    {
        _score += bonusScore;
        ScoreChanged?.Invoke(_score);
    }
}
