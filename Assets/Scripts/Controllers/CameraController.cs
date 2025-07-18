using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private ShopView _shopView;
    [SerializeField] private Vector3 _offsetForOpenShop;

    private Player _player;
    private Vector3 _offset;

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

    public void Init(Player player)
    {
        _player = player;
        _offset = CalculateOffset();
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
