using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Platform : Interactable
{
    [SerializeField] private Collider _collider;
    [SerializeField] private TMP_Text _text;

    [SerializeField] private float _smallScale = 0.6f;
    [SerializeField] private float _mediumScale = 0.9f;
    [SerializeField] private float _largeScale = 1.2f;

    private float _sector2 = 1f;
    private float _sector3 = 0.5f;

    private float _flyingDuration = 2f;

    private float _scoreMax = 3;
    private float _scoreMed = 2;
    private float _scoreMin = 1;

    private float _score;

    public event Action<float> ScoreChanged;
    public event Action PlayerJumpedOnPlatform;

    private void Awake()
    {
        _text.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            float differenceX = player.transform.position.x - transform.position.x;
            float differenceZ = player.transform.position.z - transform.position.z;

            float positiveX = differenceX < 0 ? -differenceX : differenceX;
            float positiveZ = differenceZ < 0 ? -differenceZ : differenceZ;

            if (positiveX < _sector3 && positiveZ < _sector3) _score = _scoreMax;
            else if (positiveX < _sector2 && positiveZ < _sector2) _score = _scoreMed;
            else _score = _scoreMin;

            ShowScore(_score);
            ScoreChanged?.Invoke(_score);
            PlayerJumpedOnPlatform?.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            _text.gameObject.SetActive(false);
        }
    }

    private void ShowScore(float score)
    {
        InitScore(score);
        _text.gameObject.SetActive(true);
        _text.rectTransform.DOAnchorPosY(0, _flyingDuration);
    }

    private void InitScore(float score)
    {
        if (score == _scoreMin) ChangeScale(_smallScale);
        else if (score == _scoreMed) ChangeScale(_mediumScale);
        else if (score == _scoreMax) ChangeScale(_largeScale);

        _text.text = "+" + score.ToString();
    }

    private void ChangeScale(float scale)
    {
        _text.rectTransform.localScale = new Vector3(scale, scale, scale);
    }
}
