using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateOffset;

    void Update()
    {
        Rotate(_rotateOffset);
    }

    private void Rotate(Vector3 vector3)
    {
        transform.Rotate(vector3 * Time.deltaTime);
    }
}
