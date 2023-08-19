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
    public Image Icon;
    public Image Progress;
    public GameObject Explosion;
    public GameObject MagnetCollider;

    public bool Shielded { get; set; } = false;
    public float Multiplier { get; set; } = 0f;
    public float Magnet { get; set; } = 0f;
    public float Fist { get; set; } = 0f;

    public void Start()
    {
        Collider = Player.GetComponentInChildren<CapsuleCollider2D>();
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

    public void ActivateMultiplier()
    {
        Multiplier = 10f;
        Magnet = 0f;
        Fist = 0f;
    }

    public void ActivateMagnet()
    {
        Multiplier = 0f;
        Magnet = 10f;
        Fist = 0f;
        MagnetCollider.SetActive(true);
    }

    public void DeactivateMagnet()
    {
        MagnetCollider.SetActive(false);
    }

    public void ActivateFist()
    {
        Multiplier = 0f;
        Magnet = 0f;
        Fist = 10f;
    }

    public void SpawnFireball()
    {
        Player.SpawnFireball();
    }


    public void UseBomb()
    {
        Instantiate(Explosion, new Vector3(Player.transform.position.x, Player.transform.position.y, 0), Quaternion.identity);
        // destroy all asteroids
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject go in gos)
            go.GetComponent<AsteroidScript>().Destruct();
    }
}