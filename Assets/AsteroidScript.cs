using UnityEngine;
using System;

public class AsteroidScript : MonoBehaviour
{
    public float MoveSpeed = 5;
    public readonly float DeadZone = -12f;
    private string TypeName;
    public GameObject Gem;
    public GameObject Explosion;
    private AudioManager Sfx;

    void Start()
    {
        Sfx = GameObject.FindGameObjectWithTag("Sfx").GetComponent<AudioManager>();
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();

        int roll = UnityEngine.Random.Range(0, 6);
        int type = UnityEngine.Random.Range(1, 5);
        if (roll >= 5)
        {
            TypeName = "meteor";
        }
        else
        {
            TypeName = "asteroid";
        }
        renderer.sprite = Resources.Load<Sprite>(TypeName + type);

        var collider = gameObject.GetComponent<CapsuleCollider2D>();
        if (type == 1)
        {
            collider.offset = new Vector2(-0.02f, -0.05f);
            collider.size = new Vector2(0.9f, 0.75f);
        }
        else if (type == 2)
        {
            collider.offset = new Vector2(0, 0.08f);
            collider.size = new Vector2(1.1f, 0.68f);
        }
        else if (type == 3)
        {
            collider.offset = new Vector2(-0.02f, -0.01f);
            collider.size = new Vector2(0.78f, 0.78f);
        }
        else if (type == 4)
        {
            collider.offset = new Vector2(-0.01f, -0.015f);
            collider.size = new Vector2(0.88f, 0.88f);
        }

        transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0,359)));
    }

    void Update()
    {
        transform.position += MoveSpeed * Time.deltaTime * Vector3.left;
        transform.Rotate(new Vector3(0, 0, 40) * Time.deltaTime);

        if (transform.position.x < DeadZone) Destroy(gameObject);
    }

    // deletion and score incrementation on player pickup
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerScript>() != null
            || collision.gameObject.layer == 7)
        {
            Destruct();
        }
    }

    public void Destruct()
    {
        int spawns;
        if (TypeName == "asteroid") spawns = UnityEngine.Random.Range(0, 3);
        else spawns = UnityEngine.Random.Range(2, 5);
        for (int i = 0; i <= spawns; i++)
        {
            double theta = UnityEngine.Random.Range(0, 360) * Mathf.PI / 180;
            double[] vector = RotateVector(new double[] { 0.01, 0 }, theta);
            GameObject loot = Instantiate(Gem, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            var gs = loot.GetComponentInChildren<GemScript>();
            gs.Launch(vector, 60);
        }
        // play explosion animation
        Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        // play explosion sfx
        Sfx.Explosion();
        Destroy(gameObject);
    }

    private double[] RotateVector(double[] vector, double theta)
    {
        double[][] rotMatrix = new double[][] {
            new double[] { Math.Cos(theta), -Math.Sin(theta) },
            new double[] { Math.Sin(theta), Math.Cos(theta) }
        };
        double[] res = new double[]
        {
            vector[0] * rotMatrix[0][0] + vector[1] * rotMatrix[1][0],
            vector[0] * rotMatrix[0][1] + vector[1] * rotMatrix[1][1]
        };
        return res;
    }
}