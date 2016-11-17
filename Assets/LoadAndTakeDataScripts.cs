using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadAndTakeDataScripts : MonoBehaviour {
	public InputField deviceIp;
	public InputField address;
	public InputField phone;
	public Button enter;
	public Text errorMessAddress;
	public Text errorMessPhone;
	// Use this for initialization
	void Start () {
		deviceIp.interactable = false;
		string id = Network.player.ipAddress;
		deviceIp.text = id;
		address.text = PlayerPrefs.GetString ("Address");
		phone.text = PlayerPrefs.GetString ("userPhone");
	}

	// Update is called once per frame6
	void Update () {
		enter.onClick.AddListener (delegate {
			enterKeyDown ();
		});
	}
	void  enterKeyDown(){
		if (address.text == "") {
			errorMessAddress.gameObject.SetActive (true);
		} else if (phone.text == "") {
			errorMessPhone.gameObject.SetActive (true);
		}
		else {
			PlayerPrefs.SetString("Address",address.text);
			PlayerPrefs.SetString ("userPhone", phone.text);
			errorMessAddress.gameObject.SetActive (false);
			errorMessPhone.gameObject.SetActive (false);
		}
	}

}