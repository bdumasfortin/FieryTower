using System;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    [SerializeField] private float _comboTimeLimit = 2.5f;

    private static readonly Dictionary<string, object> _configList = new Dictionary<string, object>();

    private void Awake()
    {
        _configList.Add("ComboTimeLimit", _comboTimeLimit);
    }

    public static T GetConfig<T>(string configName)
    {
        if (!_configList.TryGetValue(configName, out object value))
            return default;

        return (T)Convert.ChangeType(value, typeof(T));
    }
}
