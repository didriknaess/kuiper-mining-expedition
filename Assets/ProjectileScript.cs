using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private readonly float DeadZone = 12f;
    void Update()
    {
        transform.position += Time.deltaTime * 3 * Vector3.right;

        if (transform.position.x > DeadZone) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}