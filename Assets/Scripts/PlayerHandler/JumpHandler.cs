using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class JumpHandler : MonoBehaviour
{
    private const int NumJumps = 1;

    [SerializeField] private float _jumpPower = 3;
    [SerializeField] private float _defaultJumpPower = 3;
    [SerializeField] private int _minJumpPower = 3;
    [SerializeField] private int _maxJumpPower = 13;
    [SerializeField] private float _pauseBetweenIncreasingJumpPower = 0.2f;
    [SerializeField] private float _duration = 1;
    [SerializeField] private float _jumpHeight = 3;

    [SerializeField] private Player _player;

    private Coroutine _coroutine;

    public event Action<float> JumpPowerChanged;

    public void IncreaseJumpPower()
    {
        if (_player.CollisionHandler.IsGrounded)
        {
            _coroutine = StartCoroutine(JumpPowerIncreaser());
        }
    }

    public void Jump()
    {
        if (_player.CollisionHandler.IsGrounded)
        {
            if (_player.IsJumpingOnAxisX) JumpDefault(_jumpPower, 0);
            else JumpDefault(0, _jumpPower);
        }
    }

    private void JumpDefault(float jumpPowerX, float jumpPowerZ)
    {
        transform.DOJump(new Vector3(transform.position.x + jumpPowerX, transform.position.y,
            transform.position.z + jumpPowerZ), _jumpHeight, NumJumps, _duration);
        if (_coroutine != null) StopCoroutine(_coroutine);
        SetDefaultNumberOfJumpPower();
    }

    private IEnumerator JumpPowerIncreaser()
    {
        while (!Input.GetMouseButtonUp(0))
        {
            var newJumpPower = Mathf.Clamp(_jumpPower++, _minJumpPower, _maxJumpPower);
            JumpPowerChanged?.Invoke(newJumpPower);
            yield return new WaitForSeconds(_pauseBetweenIncreasingJumpPower);
        }
    }

    private void SetDefaultNumberOfJumpPower()
    {
        _jumpPower = _defaultJumpPower;
        JumpPowerChanged.Invoke(_jumpPower);
    }
}
