using UnityEngine;

public class PlatformFactory : MonoBehaviour
{
    private static PlatformFactory _instance = null;
    public static PlatformFactory Instance => _instance;
    
    [SerializeField] private GameObject _smallPlatformPrefab;
    [SerializeField] private GameObject _mediumPlatformPrefab;
    [SerializeField] private GameObject _largePlatformPrefab;
    [SerializeField] private GameObject _fullPlatformPrefab;

    private void Awake()
    {
        _instance = this;
    }

    private GameObject FindPrefabFromType(PlatformType type)
    {
        if (type == PlatformType.Small)
            return _smallPlatformPrefab;
        else if (type == PlatformType.Medium)
            return _mediumPlatformPrefab;
        else if (type == PlatformType.Large)
            return _largePlatformPrefab;
        else
            return _fullPlatformPrefab;
    }

    public GameObject Instantiate(PlatformType type, float height, Transform container = null)
    {
        // TODO: Randomize x value for platform position

        return GameObject.Instantiate(FindPrefabFromType(type), new Vector3(0, height, 0), Quaternion.identity, container);
    }
}

public enum PlatformType
{
    Small,
    Medium,
    Large,
    Full
}