using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {
    //public GameObject audio;
    private AudioSource audio;
        // Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        //audio = GetComponent<GameObject>().GetComponentInChildren<AudioSource>;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnPlaySound()
    {
        audio.Play();
    }
}
