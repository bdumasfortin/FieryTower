using TMPro;
using UnityEngine;

public class GUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _heightText;

    private int _highestPoint = 0;

    private void Update()
    {
        var currentHeight = LevelManager.ActiveLevel.CalculateCurrentPoints(Player.Instance.transform.position.y);

        if (currentHeight > _highestPoint)
            _highestPoint = currentHeight;

        // TODO: Move elsewhere
        if (_highestPoint >= LevelManager.ActiveLevel.StartMovingLavaAtHeight)
            LevelManager.ActiveLevel.StartMovingLava();

        _scoreText.text = _highestPoint.ToString();
        _heightText.text = currentHeight.ToString();
    }
}
