using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance = null;

    public List<Level> Levels = new List<Level>();

    public static Level ActiveLevel => _instance.Levels.Find(l => l.isActiveAndEnabled);

    private void Awake()
    {
        _instance = this;
    }

    public static void LoadLevel(int levelIndex)
    {
        _instance.Levels.ForEach(l => l.gameObject.SetActive(false));
        _instance.Levels[levelIndex]?.gameObject.SetActive(true);
    }
}
