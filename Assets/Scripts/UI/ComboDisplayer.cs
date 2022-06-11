using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        HideComboInterface();
    }

    public void OnComboStateChanged(int activeStreak, float timerRatio)
    {
        if (activeStreak <= 0)
            return;

        ShowComboInterface();
        UpdateStreakText(activeStreak.ToString());
        UpdateSliderValue(timerRatio);
    }

    public void OnComboEnded()
    {
        HideComboInterface();
    }

    private void UpdateStreakText(string text)
    {
        _text.text = text;
    }

    private void UpdateSliderValue(float value)
    {
        _slider.value = value;
    }

    private void ShowComboInterface()
    {
        _text.gameObject.SetActive(true);
        _slider.gameObject.SetActive(true);
    }

    private void HideComboInterface()
    {
        _text.gameObject.SetActive(false);
        _slider.gameObject.SetActive(false);
    }
}
