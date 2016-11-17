using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TransferCategoryName : MonoBehaviour {
    public static string catName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void TransferCatname()
    {
        catName = this.name;
        Debug.Log(catName);
    }

    public void OnClick()
    {
        catName = this.name;
        
        SceneManager.LoadScene("Lucario/SubMenu");
    }
}
