using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverOverlay;

    // TODO: Main menu with controls, End when dead, restart, etc.

    // TODO: Lava goes faster and faster

    // TODO: Player can get slippery

    // TODO: Player powerups / bad powerups

    // TODO: Enemies

    // TODO: Double jumps

    // TODO: Weapons to defend himself

    // TODO: Really easy and simple at start, complex and larger level the more you go up

    // TODO: Scoreboard

    // TODO: Lava stops moving, becomes a boss

    public void OnGameOver()
    {
        // TODO: Deactivate controls and game update
        _gameOverOverlay.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Restart();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
