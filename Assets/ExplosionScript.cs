using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float LifeTime = 0.3f;

    void Start()
    {
        Destroy(gameObject, LifeTime);
    }
}
