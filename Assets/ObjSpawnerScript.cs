using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    public GameObject Obj;
    public float Delay;
    public float Spawnrate;
    public float HeightOffset;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = Spawnrate - Delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer < Spawnrate)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            Timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        float top = transform.position.y + HeightOffset;
        float bottom = transform.position.y - HeightOffset;

        Instantiate(Obj, new Vector3(transform.position.x, Random.Range(bottom, top), 0), transform.rotation);
    }
}
