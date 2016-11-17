using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour {

    private bool isPortraitConsistence;

    private int contNum;

    public GameObject contentX;
    public GameObject contentY;

    public GameObject tempItemX;
    public GameObject tempItemY;

    private List<GameObject> itemListX;
    private List<GameObject> itemListY;

    private string cateName;
    private Dictionary<int, string> cateWithIndex = new Dictionary<int, string>();

    private float firstX_X;
    private float firstY_X;
    private float firstX_Y;
    private float firstY_Y;
    
    void CreateList(float firstX, float firstY, GameObject tempItem, GameObject content)
    {
        GameObject first = Instantiate(tempItem) as GameObject;
        //first.transform.SetParent(contentY.transform, false);
        first.transform.position.Set(firstX, firstY, 0.0f);
        first.name = "SubItem0";
        first.gameObject.SetActive(true);
        first.GetComponentInChildren<Text>().text = GridLayout.hashmap[cateName][0].ProductName;
        Debug.Log(content.transform.position.x);
        first.transform.SetParent(contentY.transform, true);

        for (int i = 1; i < contNum; i++)
        {
            GameObject btn = Instantiate(tempItem) as GameObject;
            //btn.transform.SetParent(transform, false);
            first.transform.position.Set(firstX, firstY, 0.0f);
            btn.name = "SubItem" + i;
            //Debug.Log(i);

            btn.gameObject.SetActive(true);
            btn.GetComponentInChildren<Text>().text = GridLayout.hashmap[cateName][i].ProductName;
            btn.transform.SetParent(content.transform, false);

        }
    }

    void DestroyList(GameObject content)
    {
        var children = new List<GameObject>();
        foreach (Transform child in content.transform)
        {
            children.Add(child.gameObject);
        }
        children.RemoveAt(0);
        children.ForEach(child => Destroy(child));
    }

    void PortraitItemsSetup()
    {

        firstX_Y = tempItemY.transform.position.x;
        firstY_Y = tempItemY.transform.position.y;

        CreateList(firstX_Y, firstY_Y, tempItemY, contentY); 
    }

    void LandscapeItemsSetup()
    {
        firstX_X = tempItemX.transform.position.x;
        firstY_X = tempItemX.transform.position.y;

        CreateList(firstX_X, firstY_X, tempItemX, contentX);
    }
    

    void Start()
    {
        cateName = TransferCategoryName.catName;

        itemListX = new List<GameObject>();
        itemListY = new List<GameObject>();

        contNum = GridLayout.hashmap[cateName].Count;

        if ((Screen.height > Screen.width) || Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            PortraitItemsSetup();
            isPortraitConsistence = true;
            //SceneManager.LoadScene("Lucario/MainMenuPortrait");

            return;
        }
        if ((Screen.height < Screen.width) ||
            Input.deviceOrientation == DeviceOrientation.LandscapeLeft
            || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {

            LandscapeItemsSetup();
            isPortraitConsistence = false;
            //SceneManager.LoadScene("Lucario/MainMenu");

            return;
        }
        if ((Screen.height > Screen.width) || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {

            PortraitItemsSetup();
            isPortraitConsistence = true;

            //SceneManager.LoadScene("Lucario/MainMenuPortrait");

            return;
        }
    }

    void CheckRotateY()
    {


        if (!isPortraitConsistence)
        {
            for (int i = 0; i < itemListX.Count; i++)
            {
                itemListX[i].gameObject.SetActive(false);
            }
            PortraitItemsSetup();
            isPortraitConsistence = true;
        }
        else
        {
            DestroyList(contentY);
            CreateList(firstX_Y, firstY_Y, tempItemY, contentY);
            for (int i = 0; i < itemListY.Count; i++)
            {
                itemListY[i].gameObject.SetActive(true);
            }
        }
    }

    void CheckRotateX()
    {
        if (isPortraitConsistence)
        {
            for (int i = 0; i < itemListY.Count; i++)
            {
                itemListY[i].gameObject.SetActive(false);
            }
            LandscapeItemsSetup();
            isPortraitConsistence = false;
        }
        else
        {
            DestroyList(contentX);
            CreateList(firstX_X, firstY_X, tempItemX, contentX);
            for (int i = 0; i < itemListX.Count; i++)
            {
                itemListX[i].gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (ChangeState.clicked == true)
        {
            cateName = ChangeState.cateName;

            contNum = GridLayout.hashmap[cateName].Count;
            ChangeState.clicked = false;
        }

        

        if ((Screen.height > Screen.width) || Input.deviceOrientation == DeviceOrientation.Portrait)
        {

            CheckRotateY();

            //SceneManager.LoadScene("Lucario/MainMenuPortrait");


        }
        if ((Screen.height < Screen.width) || Input.deviceOrientation == DeviceOrientation.LandscapeLeft
            || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {

            CheckRotateX();
            //SceneManager.LoadScene("Lucario/MainMenu");


        }
        if ((Screen.height > Screen.width) || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {

            CheckRotateY();
            //SceneManager.LoadScene("Lucario/MainMenuPortrait");


        }
    }
}
