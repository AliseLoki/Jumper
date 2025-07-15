using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlatformScoreBonusView : MonoBehaviour
{
    private const char Plus = '+';

    [SerializeField] private float _scoreBonusFlyingDuration = 2f;

    [SerializeField] private float _smallScale = 0.6f;
    [SerializeField] private float _mediumScale = 0.9f;
    [SerializeField] private float _largeScale = 1.2f;

    [SerializeField] private TMP_Text _text;

    public float BonusMax { get; private set; } = 3;
    public float BonusMid { get; private set; } = 2;
    public float BonusMin { get; private set; } = 1;

    private void Awake()
    {
        SetScoreBonusActive(false);
        // можно и в эвейке платформы вызывать
    }

    public void SetScoreBonusActive(bool isActive)
    {
        _text.gameObject.SetActive(isActive);
    }

    public void ShowScore(float bonusScore)
    {
        SetScale(bonusScore);
        InitBonusScoreAmount(bonusScore);
        SetScoreBonusActive(true);
        ShowBonusScoreViewFlyAnimation(_scoreBonusFlyingDuration);
    }

    private void ShowBonusScoreViewFlyAnimation(float fluingDuration)
    {
        _text.rectTransform.DOAnchorPosY(0, fluingDuration);
    }

    private void SetScale(float bonusScore)
    {
        if (bonusScore == BonusMin) ChangeScale(_smallScale);
        else if (bonusScore == BonusMid) ChangeScale(_mediumScale);
        else if (bonusScore == BonusMax) ChangeScale(_largeScale);
    }

    private void InitBonusScoreAmount(float bonusScore)
    {
        _text.text = Plus + bonusScore.ToString();
    }

    private void ChangeScale(float scale)
    {
        _text.rectTransform.localScale = new Vector3(scale, scale, scale);
    }
}
