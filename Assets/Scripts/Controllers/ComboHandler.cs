using UnityEngine;
using UnityEngine.Events;

public class ComboHandler : MonoBehaviour
{
    [System.Serializable]
    public class ComboStateEvent : UnityEvent<int, float>
    {
    }

    [Header("Events")]
    public ComboStateEvent OnComboStateUpdate;
    public UnityEvent OnComboEnded;

    private class ComboStreak
    {
        private int _activeStreak = 0;
        
        public bool IsActive => _activeStreak > 0;
        public int Streak => _activeStreak;

        public void Increment() => _activeStreak++;
        public void EndCombo() => _activeStreak = 0;
    }

    private static ComboHandler _instance = null;

    private readonly ComboStreak _comboStreak = new ComboStreak();
    private Timer _comboTimer;

    public static bool IsComboActive => _instance?._comboStreak.IsActive ?? false;

    public static void IncrementStreak()
    {
        _instance?._comboStreak.Increment();
        _instance?._comboTimer.Restart();
    }

    private void Awake()
    {
        _instance = this;
        InitComboTimer();
    }

    private void InitComboTimer()
    {
        _comboTimer = gameObject.AddComponent<Timer>();
        _comboTimer.SetTarget(GameConfig.GetConfig<float>("ComboTimeLimit"));
        _comboTimer.OnTimerEnd += OnTimerEnd;
    }

    private void Update()
    {
        if (_comboStreak.IsActive)
            PropagateComboState(_comboStreak.Streak, _comboTimer.Ratio);
    }

    private void OnTimerEnd()
    {
        _comboStreak.EndCombo();
        NotifyComboEnded();
    }

    private void PropagateComboState(int activeStreak, float timerRatio)
    {
        OnComboStateUpdate?.Invoke(activeStreak, timerRatio);
    }

    private void NotifyComboEnded()
    {
        OnComboEnded?.Invoke();
    }
}
