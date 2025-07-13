using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ShopView _shopView;

    [SerializeField] private Vector3 _offsetForOpenShop;

    private Vector3 _offset;

    private void Start()
    {
        _offset = CalculateOffset();
    }

    private void LateUpdate()
    {
        if (_shopView.gameObject.activeSelf)
        {
            ShowPlayerOnTheLeft();
        }
        else
        {
            FollowPlayer();
        }
    }

    private void ShowPlayerOnTheLeft()
    {
        transform.position = _player.transform.position + _offsetForOpenShop;
    }

    private void FollowPlayer()
    {
        transform.position = _player.transform.position + _offset;
    }

    private Vector3 CalculateOffset()
    {
        return transform.position - _player.transform.position;
    }
}
