using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public int Score = 0;
    public Text ScoreText;
    public GameObject GameOverScreen;
    public PlayerScript Player;
    private CapsuleCollider2D Collider;
    public GameObject Shield;
    public int Multiplier { get; set; } = 1;
    public bool Shielded { get; set; } = false;

    public void Start()
    {
        Collider = Player.GetComponent<CapsuleCollider2D>();
    }

    [ContextMenu("Increase Score")]
    public void AddScore(int score)
    {
        Score += score;
        ScoreText.text = Score.ToString();
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
    }

    public bool IsGameOver()
    {
        return !Player.IsAlive;
    }

    public void ActivateShield()
    {
        Shielded = true;
        Shield.SetActive(true);
        Collider.offset = new Vector2(0, 0);
        Collider.size = new Vector2(1.32f, 1.32f);
    }

    public void DeactivateShield()
    {
        Shielded = false;
        Shield.SetActive(false);
        Collider.offset = new Vector2(0.005f, 0.03f);
        Collider.size = new Vector2(0.45f, 0.85f);
    }
}