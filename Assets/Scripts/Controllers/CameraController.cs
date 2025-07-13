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
           ChangeCameraPositon(_offsetForOpenShop);
        }
        else
        {
           ChangeCameraPositon(_offset);
        }
    }

    private void ChangeCameraPositon(Vector3 offset)
    {
        transform.position = _player.transform.position + offset;
    }

    private Vector3 CalculateOffset()
    {
        return transform.position - _player.transform.position;
    }
}
