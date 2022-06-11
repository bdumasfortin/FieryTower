using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Lava")]
    [SerializeField] Transform _lava;
    [SerializeField] int _startMovingLavaAtHeight = 3;
    [SerializeField] float _lavaSpeed = 2;

    [Header("Platforms")]
    [SerializeField] Transform _platformContainer;
    [SerializeField] float _platformStartHeight = -1;
    [SerializeField] float _platformSpacing = 3;

    public int StartMovingLavaAtHeight => _startMovingLavaAtHeight;
    private bool _lavaIsMoving = false;

    private void Awake()
    {
        // TODO: Make dynamic and despawn lower platforms
        for (int i = 0; i < 200; i++)
            PlatformFactory.Instance.Instantiate(PlatformType.Full, _platformStartHeight + _platformSpacing * i, _platformContainer);
    }

    private void FixedUpdate()
    {
        if (_lavaIsMoving)
            _lava.position += Vector3.up * _lavaSpeed * Time.deltaTime;
    }

    public int CalculateCurrentPoints(float playerHeight)
    {
        return Mathf.FloorToInt((playerHeight - _platformStartHeight + _platformSpacing) / _platformSpacing);
    }

    public void StartMovingLava()
    {
        _lavaIsMoving = true;
    }
}
