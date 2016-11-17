using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeMenuPerspective : MonoBehaviour {
    public Canvas LandScape;
    public Canvas Portrait;
   
    // Use this for initialization
    void Start () {
        //Debug.Log(Input.deviceOrientation);
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            Portrait.gameObject.SetActive(true);
            LandScape.gameObject.SetActive(false);
            //SceneManager.LoadScene("Lucario/MainMenuPortrait");

            return;
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft
            || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            Portrait.gameObject.SetActive(false);
            LandScape.gameObject.SetActive(true);
            //SceneManager.LoadScene("Lucario/MainMenu");

            return;
        }
        if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {

            Portrait.gameObject.SetActive(true);
            LandScape.gameObject.SetActive(false);
            //SceneManager.LoadScene("Lucario/MainMenuPortrait");

            return;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(Input.deviceOrientation);
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            Portrait.gameObject.SetActive(true);
            LandScape.gameObject.SetActive(false);
            //SceneManager.LoadScene("Lucario/MainMenuPortrait");

            return;
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft
            || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            Portrait.gameObject.SetActive(false);
            LandScape.gameObject.SetActive(true);
            //SceneManager.LoadScene("Lucario/MainMenu");
            return;
        }
        if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {

            //SceneManager.LoadScene("Lucario/MainMenuPortrait");
            Portrait.gameObject.SetActive(true);
            LandScape.gameObject.SetActive(false);
            return;
        }
    }
}
