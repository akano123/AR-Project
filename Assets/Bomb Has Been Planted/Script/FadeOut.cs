using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour {

    public Image splashImg;
    public Image splashImg2;
    public GameObject panel;
    public string loadLevel;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2.5f);
        fadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(loadLevel);
    }

    void fadeOut()
    {
        panel.GetComponent<Image>().CrossFadeColor(Color.black, 2.0f, false, false);
        splashImg.GetComponent<Image>().CrossFadeColor(Color.black, 2.0f, false, false);
        splashImg2.GetComponent<Image>().CrossFadeColor(Color.black, 2.0f, false, false);

    }


}
