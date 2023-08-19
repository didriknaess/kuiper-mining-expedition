using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public void Start()
    {
        Text HighScore = GameObject.FindGameObjectWithTag("HighScore").GetComponent<Text>();
        HighScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKey(KeyCode.Space))
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
