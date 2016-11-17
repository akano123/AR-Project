using UnityEngine;
using System.Collections;

public class DetectAspectDevice : MonoBehaviour {

    public Canvas portrait;
    public Canvas landscape;
    // Use this for initialization
    void Start () {
        portrait.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Input.deviceOrientation);
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            landscape.gameObject.SetActive(false);
            portrait.gameObject.SetActive(true);
            return;
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft
            || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            portrait.gameObject.SetActive(false);
            landscape.gameObject.SetActive(true);
            return;
        }
        if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            landscape.gameObject.SetActive(false);
            portrait.gameObject.SetActive(true);
            return;
        }
    }
}
