using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _maxDistanceFromTarget = 0.5f;

    private float _targetHeight = 0f;

    private void Update()
    {
        if (_targetTransform.position.y > 0)
            _targetHeight = _targetTransform.position.y;
    }

    private void FixedUpdate()
    {
        var distanceFromTarget = transform.position.y - _targetHeight;
        var moveSpeedAdjusted = _moveSpeed * Time.deltaTime * Mathf.Abs(distanceFromTarget);

        if (distanceFromTarget < -_maxDistanceFromTarget) // Below target
        {
            transform.position += Vector3.up * Mathf.Min(moveSpeedAdjusted, Mathf.Abs(distanceFromTarget));
        }
        else if (distanceFromTarget > _maxDistanceFromTarget) // Above target
        {
            transform.position += Vector3.down * Mathf.Min(moveSpeedAdjusted, distanceFromTarget);
        }
    }
}
