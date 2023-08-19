using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D Body;
    private AudioManager Sfx;
    public int MaxVelocity;
    public LogicManager Logic;
    public bool IsAlive { get; set; }  = true;
    private AudioSource DeathNoise;
    public GameObject Fireball;

    void Start()
    {
        Body = gameObject.GetComponentInChildren<Rigidbody2D>();
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

        if (Logic.Multiplier > 0f) Logic.Multiplier -= Time.deltaTime;
        if (Logic.Magnet > 0f) Logic.Magnet -= Time.deltaTime;
        else Logic.DeactivateMagnet();
        if (Logic.Fist > 0f) Logic.Fist -= Time.deltaTime;
    }

    // collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision!");
        if (collision.gameObject.layer == 6) { }
        else if (IsAlive && Logic.Fist > 0f)
        {
            Body.velocity = new Vector2(0, 0);
        }
        else if (IsAlive && Logic.Shielded)
        {
            Logic.DeactivateShield();

            // gives inverse moementum to avoid double collisions
            var collisionHeight = collision.GetContact(0).point.y;
            if (collisionHeight < 0) Body.velocity = Vector2.up * 2;
            else Body.velocity = -Vector2.up * 2;
        }
        else if (IsAlive) { GameOver(); }
    }

    private void GameOver()
    {
        DeathNoise.Play();
        Sfx.PauseMusic();
        Sfx.GameOver();
        Logic.GameOver();
        IsAlive = false;
    }

    public void SpawnFireball()
    {
        Instantiate(Fireball, new Vector3(transform.position.x + 1.3f, transform.position.y, 0), Quaternion.identity);
    }
}
