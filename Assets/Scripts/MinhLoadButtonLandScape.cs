using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class loadButtonLandScape : MonoBehaviour {
    public Button button;
    private float FirstX;
    private float FirstY;
    public int count;
    // Use this for initialization
    void Start () {
        loadScene();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void loadScene()
    {


        Sprite[] sImage = Resources.LoadAll<Sprite>("MainMenuButtons");

        FirstX = button.transform.position.x;

        FirstY = button.transform.position.y;
        for (int i = count; i < count + 6; i++)
        {
            //Debug.Log(i);
            if (i == (sImage.Length - 1))
            {
                return;
            }

            Button moreButton = Instantiate(button) as Button;
            moreButton.transform.SetParent(transform, false);

            moreButton.transform.position = new Vector3(FirstX, FirstY, 0f);
            //Debug.Log(">>>>>>>>>>>>>>>>>>>>>" + FirstX);
            moreButton.image.sprite = sImage[(int)i];
            moreButton.gameObject.SetActive(true);

            if (i % 2 == 0)
            {
                FirstX += (Screen.width) / 3;
            }
            else {
                FirstX = button.transform.position.x;
                FirstY = FirstY - (Screen.height / 4);
                Debug.Log(FirstY);
            }


        }
        count += 6;
    }
}
