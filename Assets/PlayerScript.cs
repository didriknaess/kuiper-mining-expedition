using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Rigidbody2D Body;
    private AudioManager Sfx;
    public int MaxVelocity;
    public LogicManager Logic;
    public bool IsAlive { get; set; }  = true;
    private AudioSource DeathNoise;

    void Start()
    {
        Sfx = GameObject.FindGameObjectWithTag("Sfx").GetComponent<AudioManager>();
        DeathNoise = GetComponent<AudioSource>();
    }

    void Update()
    {
        // rotation animation
        transform.Rotate(new Vector3(0, 0, -40) * Time.deltaTime);

        // out of bounds
        if (transform.position.y > 4.5 || transform.position.y < -4.5)
        {
            if (IsAlive)
            {
                GameOver();
                Logic.DeactivateShield();
            }
        }

        // input handler
        if (Input.GetKey(KeyCode.Space) && IsAlive)
        {
            Body.gravityScale = -1.2f;
            if (Body.velocity.y > MaxVelocity) Body.velocity = Vector2.up * MaxVelocity;
        }
        else if (IsAlive)
        {
            Body.gravityScale = 1.2f;
            if (Body.velocity.y < -MaxVelocity) Body.velocity = -Vector2.up * MaxVelocity;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !IsAlive)
        {
            Sfx.UnpauseMusic();
            Sfx.ButtonClicked();
            Logic.RestartGame();

        }
    }

    // collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsAlive && Logic.Shielded)
        {
            Logic.DeactivateShield();

            // gives inverse moementum to avoid double collisions
            var collisionHeight = collision.GetContact(0).point.y;
            if (collisionHeight < 0) Body.velocity = Vector2.up * 2;
            else Body.velocity = -Vector2.up * 2;
        }
        else if (IsAlive) GameOver();
    }

    private void GameOver()
    {
        DeathNoise.Play();
        Sfx.PauseMusic();
        Sfx.GameOver();
        Logic.GameOver();
        IsAlive = false;
    }
}
