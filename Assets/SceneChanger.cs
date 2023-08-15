using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MoveToScene(1);
        }
    }

    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void Test()
    {
        Debug.Log("Accessed Button!");
    }
}
