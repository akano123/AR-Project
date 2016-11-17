using UnityEngine;
using System.Collections;

public class CheckInactives : MonoBehaviour {

    private int childCount;
	// Use this for initialization
	void Start () {
        childCount = this.transform.childCount;
	}
	
	// Update is called once per frame
	void Update () {
	    if (this.transform.childCount != childCount)
        {

        }
	}
}
