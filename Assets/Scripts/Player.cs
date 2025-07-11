using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    private const int NumJumps = 1;

    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private float _jumpPower = 3;
    [SerializeField] private int _minJumpPower = 3;
    [SerializeField] private int _maxJumpPower = 13;
    [SerializeField] private float _pauseBetweenIncreasingJumpPower = 0.2f;
    [SerializeField] private float _duration = 1;
    [SerializeField] private float _jumpHeight = 3;

    [SerializeField] private PlatformsController _platformsFabrica;

    private bool _isJumpingOnAxisX = true;
    private Coroutine _coroutine;

    public event Action<float> JumpPowerChanged;
    public event Action<Interactable> CollectablesAmountChanged;

    public float JumpPower => _jumpPower;

    private void OnEnable()
    {
        _platformsFabrica.PlatformHasSpawnedOnAxisX += OnPlatformSpawned;
    }

    private void OnDisable()
    {
        _platformsFabrica.PlatformHasSpawnedOnAxisX -= OnPlatformSpawned;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IncreaseJumpPower();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground)) _isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground)) _isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Crystall crystall))
        {
            crystall.gameObject.SetActive(false);
            CollectablesAmountChanged?.Invoke(crystall);
        }

        if (other.TryGetComponent(out Coin coin))
        {
            coin.gameObject.SetActive(false);
            CollectablesAmountChanged?.Invoke(coin);
        }
    }

    private void OnPlatformSpawned(bool isTrue)
    {
        _isJumpingOnAxisX = isTrue;
    }

    private void IncreaseJumpPower()
    {
        if (_isGrounded)
        {
            _coroutine = StartCoroutine(JumpPowerIncreaser());
        }
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            if (_isJumpingOnAxisX) JumpDefault(_jumpPower, 0);
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
        _jumpPower = 3;
        JumpPowerChanged?.Invoke(_jumpPower);
    }
}
