using UnityEngine;
using UnityEngine.UI;

public class DurationBarManager : MonoBehaviour
{
    public LogicManager Logic;
    public Image Icon;
    public Image Progress;
    public AudioManager Sfx;

    void Update()
    {
        if (Logic.IsGameOver()) { gameObject.transform.position = new Vector3(0, -100, 0); }
        else if (Logic.Multiplier > 0f)
        {
            Sfx.PauseMusic();
            Sfx.PlayActionTrack();
            gameObject.transform.position = new Vector3(0, 0, 0);
            Icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("double_points");
            Progress.GetComponent<Image>().fillAmount = Logic.Multiplier / 10;
        }
        else if (Logic.Magnet > 0f)
        {
            Sfx.PauseMusic();
            Sfx.PlayActionTrack();
            gameObject.transform.position = new Vector3(0, 0, 0);
            Icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("magnet");
            Progress.GetComponent<Image>().fillAmount = Logic.Magnet / 10;
        }
        else if (Logic.Fist > 0f)
        {
            Sfx.PauseMusic();
            Sfx.PlayActionTrack();
            gameObject.transform.position = new Vector3(0, 0, 0);
            Icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("fist");
            Progress.GetComponent<Image>().fillAmount = Logic.Fist / 10;
        }
        else
        {
            Sfx.PauseMusic();
            Sfx.PlayMainTrack();
            gameObject.transform.position = new Vector3(0, -100, 0);
        }
    }
}
