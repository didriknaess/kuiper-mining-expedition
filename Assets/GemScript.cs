using UnityEngine;

public class GemScript : MonoBehaviour
{
    public LogicManager Logic;
    public AudioManager Sfx;
    private int Value;
    private static readonly string[] Names = { "ruby", "ruby", "ruby", "ruby", "ruby", "emerald", "emerald", "emerald", "diamond" };
    private static readonly float[][] RgbVals = {
        new float[] { 0.945098f, 0.6470588f, 0.4509804f }, // ruby
        new float[] { 0.945098f, 0.6470588f, 0.4509804f },
        new float[] { 0.945098f, 0.6470588f, 0.4509804f },
        new float[] { 0.945098f, 0.6470588f, 0.4509804f },
        new float[] { 0.945098f, 0.6470588f, 0.4509804f },
        new float[] { 0.6705883f, 0.8823529f, 0.5764706f }, // emerald
        new float[] { 0.6705883f, 0.8823529f, 0.5764706f },
        new float[] { 0.6705883f, 0.8823529f, 0.5764706f },
        new float[] { 0.4705882f, 0.7921569f, 0.9294118f } // diamond
    };
    private static readonly int[] Values = { 1, 1, 1, 1, 1, 2, 2, 2, 3 };
    public float MoveSpeed = 5;

    public bool Hover = true;
    private readonly float DeadZone = -50;
    private float HoverVal = 0f;
    private readonly float MaxHoverVal = -0.0005f;
    private bool Increment = true;

    private bool MagnetActive;
    private Rigidbody2D Body;
    private Vector3 TargetPosition;
    public float AttractStrength = 5f;

    public double[] SpeedVector { get; set; }
    public uint LaunchFrames { get; set; } = 0;

    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        Sfx = GameObject.FindGameObjectWithTag("Sfx").GetComponent<AudioManager>();
        var particles = gameObject.GetComponent<ParticleSystem>();

        // set score value, particle color and corresponding image
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        int r = Random.Range(0, Names.Length);
        renderer.sprite = Resources.Load<Sprite>(Names[r]);
        ParticleSystem.MainModule ma = particles.main;
        ma.startColor = new Color(RgbVals[r][0], RgbVals[r][1], RgbVals[r][2]);
        Value = Values[r];
    }

    // left movement & passive hover animation
    void Update()
    {
        transform.position += MoveSpeed * Time.deltaTime * Vector3.left;

        // hover animation
        if (Hover)
        {
            float delta = 0.001f * Time.deltaTime;
            if (Increment)
            {
                HoverVal += delta;
                transform.position += Vector3.up * delta;
            }
            else
            {
                HoverVal -= delta;
                transform.position -= Vector3.up * delta;
            }
            transform.position += Vector3.up * HoverVal;

            if (HoverVal >= MaxHoverVal) Increment = false;
            else if (HoverVal <= -MaxHoverVal) Increment = true;
        }

        // diagonal movement animation (from asteroid death)
        if (LaunchFrames > 1)
        {
            transform.position += Vector3.left * (float)SpeedVector[0];
            transform.position += Vector3.up * (float)SpeedVector[1];
            LaunchFrames--;
        }
        else if (LaunchFrames == 1)
        {
            Hover = true;
            LaunchFrames--;
        }

        // attraction if magnet is active
        if (MagnetActive)
        {
            Vector2 targetDirection = TargetPosition - transform.position;
            Body.velocity = targetDirection * AttractStrength;
        }

        if (transform.position.x < DeadZone) Destroy(gameObject);
    }

    public void SetTarget(Vector3 pos)
    {
        TargetPosition = pos;
        MagnetActive = true;
    }

    public void Launch(double[] vector, uint frameDuration)
    {

        Hover = false;
        SpeedVector = vector;
        LaunchFrames = frameDuration;
    }

    // deletion and score incrementation on player pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (!Logic.IsGameOver())
            {
                if (Logic.Multiplier > 0f) { Logic.AddScore(Value * 2); }
                else { Logic.AddScore(Value); }
            }
            Sfx.ItemPickup();
            Destroy(gameObject);
        }
    }
}