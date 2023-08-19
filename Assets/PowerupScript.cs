using UnityEngine;
using System.Threading;

public class PowerupScript : MonoBehaviour
{
    public LogicManager Logic;
    public AudioManager Sfx;

    private string Name;

    private static readonly string[] Names = {
        "shield", "bomb", "fireball", // single-use
        "double_points", "magnet", "fist", // duration-based
    };

    private static readonly float[][] RgbVals = { // currently placeholders from gem src code
        new float[] { 0.2627451f, 0.8392158f, 0.8705883f }, // shield, light blue
        new float[] { 0.6745098f, 0.7529413f, 0.7568628f }, // bomb, dark grey / black
        new float[] { 0.945098f, 0.6470588f, 0.4509804f }, // fireball, orange
        new float[] { 1f, 1f, 0 }, // double_points, yellow
        new float[] { 0, 0, 1f }, // magnet, dark blue
        new float[] { 1f, 0, 0 } // fist, red
    };
    public float MoveSpeed = 5;

    public float DeadZone = -50;
    public float HoverVal = 0f;
    public bool Increment = true;

    private void Start()
    {
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        Sfx = GameObject.FindGameObjectWithTag("Sfx").GetComponent<AudioManager>();
        var particles = gameObject.GetComponent<ParticleSystem>();

        // set score value, particle color and corresponding image
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        int r = Random.Range(0, Names.Length);
        Name = Names[r];

        renderer.sprite = Resources.Load<Sprite>(Names[r]);
        ParticleSystem.MainModule ma = particles.main;
        ma.startColor = new Color(RgbVals[r][0], RgbVals[r][1], RgbVals[r][2]);
    }

    // left movement & passive hover animation
    void Update()
    {
        transform.position += MoveSpeed * Time.deltaTime * Vector3.left;

        float delta = 0.001f * Time.deltaTime;
        if (Increment) {
            HoverVal += delta;
            transform.position += Vector3.up * delta;
        }
        else
        {
            HoverVal -= delta;
            transform.position -= Vector3.up * delta;
        }
        transform.position += Vector3.up * HoverVal;

        if (HoverVal >= 0.0005f) Increment = false;
        else if (HoverVal <= -0.0005f) Increment = true;

        if (transform.position.x < DeadZone) Destroy(gameObject);
    }

    // deletion and score incrementation on player pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Sfx.ItemPickup();
            if (Name.Equals("double_points")) Logic.ActivateMultiplier();
            else if (Name.Equals("magnet")) Logic.ActivateMagnet();
            else if (Name.Equals("fist")) Logic.ActivateFist();
            else if (Name.Equals("shield")) Logic.ActivateShield();
            else if (Name.Equals("bomb")) Logic.UseBomb();
            else if (Name.Equals("fireball")) Logic.SpawnFireball();
            Destroy(gameObject);
        }
    }
}