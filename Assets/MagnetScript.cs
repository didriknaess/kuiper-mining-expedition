using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<GemScript>(out GemScript gs))
        {
            gs.SetTarget(transform.parent.position);
        }
    }
}
