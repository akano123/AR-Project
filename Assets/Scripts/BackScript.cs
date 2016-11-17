using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClickBack()
    {
        SceneManager.LoadScene("Lucario/MainMenu");
    }

    public void backTo2D3D()
    {
        SceneManager.LoadScene("MainMenu/MainMenuScene");
    }
}
