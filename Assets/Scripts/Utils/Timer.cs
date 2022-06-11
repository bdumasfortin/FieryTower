using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action OnTimerEnd;

    private float _targetSeconds = 1f;
    private bool _isActive = false;
    private float _currentTime = 0f;

    public float Ratio => _currentTime / _targetSeconds;

    public void SetTarget(float seconds) => _targetSeconds = seconds;
    public void Pause() => _isActive = false;
    public void Resume() => _isActive = true;

    public void Restart()
    {
        _currentTime = _targetSeconds;
        Resume();
    }

    void Update()
    {
        if (_isActive)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                Pause();
                OnTimerEnd?.Invoke();
            }
        }
    }
}
