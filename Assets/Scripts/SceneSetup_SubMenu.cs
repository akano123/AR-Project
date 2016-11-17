using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SceneSetup_SubMenu : MonoBehaviour {

    private bool isPortraitConsistence;
    private Dictionary<int, string> cateWithIndex = new Dictionary<int, string>();

    public Button tempX;
    public Button tempY;
    public GameObject scrollviewX;
    public GameObject inactivesX;
    public GameObject scrollviewY;
    public GameObject inactivesY;
    public GameObject bgX;
    public GameObject bgY;
    //public List<Button> btnList;

    private List<Button> btnListX;
    private List<Button> btnListY;

    public int cateIndex;
    public int cateNum;
    //Test content scroll
    public int contNum;

    private float firstX_X;
    private float firstY_X;
    private float firstX_Y;
    private float firstY_Y;

    private Sprite[] Imgs_X;
    private Sprite[] Imgs_Y;

    //Convert the original to <index, category name> format
    void LoadCategoriesForSubMenu()
    {
        var count = -1;
        var original = GridLayout.hashmap;
        foreach (var item in original)
        {
            count++;
            cateWithIndex.Add(count, item.Key);
        }
    }

    //Lookup the category name in the new format
    int FindIndex(string name)
    {
        foreach (var item in cateWithIndex)
        {
            if (item.Value == name)
            {
                return item.Key;
            }
        }
        return -1;
    }

    void PortraitCanvas()
    {
        scrollviewY.gameObject.SetActive(true);
        inactivesY.gameObject.SetActive(true);
        bgY.gameObject.SetActive(true);

        scrollviewX.gameObject.SetActive(false);
        inactivesX.gameObject.SetActive(false);
        bgX.gameObject.SetActive(false);

        //btnList = new List<Button>();
        firstX_Y = tempY.transform.position.x;
        firstY_Y = tempY.transform.position.y;

        Imgs_Y = Resources.LoadAll<Sprite>("SubMenus");

        Button newbtn = Instantiate(tempY) as Button;
        newbtn.transform.SetParent(transform, false);
        newbtn.transform.position.Set(firstX_Y, firstY_Y, 0.0f);
        newbtn.image.sprite = Imgs_Y[0];
        newbtn.name = GridLayout.catName[0];

        if (cateIndex == 0)
        {
            newbtn.transform.SetSiblingIndex(scrollviewY.transform.GetSiblingIndex() + 1);
        }
        else
        {
            newbtn.transform.SetParent(inactivesY.transform, true);
        }

        newbtn.gameObject.SetActive(true);
        btnListY.Add(newbtn);

        for (int i = 1; i < cateNum; i++)
        {
            Button btn = Instantiate(tempY) as Button;
            btn.transform.SetParent(transform, false);

            firstX_Y += (Screen.width / 4);

            btn.transform.position = new Vector3(firstX_Y, firstY_Y, 0.0f);
            btn.image.sprite = Imgs_Y[i];
            btn.name = GridLayout.catName[i];
            if (cateIndex == i)
            {
                btn.transform.SetSiblingIndex(scrollviewY.transform.GetSiblingIndex() + 1);
            }
            else
            {
                btn.transform.SetParent(inactivesY.transform, true);
            }
            btn.gameObject.SetActive(true);
            btnListY.Add(btn);
        }
    }

    void LandscapeCanvas()
    {
        scrollviewX.gameObject.SetActive(true);
        inactivesX.gameObject.SetActive(true);
        bgX.gameObject.SetActive(true);

        scrollviewY.gameObject.SetActive(false);
        inactivesY.gameObject.SetActive(false);
        bgY.gameObject.SetActive(false);

        //btnList = new List<Button>();
        firstX_X = tempX.transform.position.x;
        firstY_X = tempX.transform.position.y;

        Imgs_X = Resources.LoadAll<Sprite>("LandscapeButtons/Right");

        Button newbtn = Instantiate(tempX) as Button;
        newbtn.transform.SetParent(transform, false);
        newbtn.transform.position.Set(firstX_X, firstY_X, 0.0f);
        newbtn.image.sprite = Imgs_X[0];
        newbtn.name = GridLayout.catName[0];

        if (cateIndex == 0)
        {
            newbtn.transform.SetSiblingIndex(scrollviewX.transform.GetSiblingIndex() + 1);
        }
        else
        {
            newbtn.transform.SetParent(inactivesX.transform, true);
        }

        newbtn.gameObject.SetActive(true);
        btnListX.Add(newbtn);

        for (int i = 1; i < cateNum; i++)
        {
            Button btn = Instantiate(tempX) as Button;
            btn.transform.SetParent(transform, false);

            firstY_X -= (Screen.height / 4);

            btn.transform.position = new Vector3(firstX_X, firstY_X, 0.0f);
            btn.image.sprite = Imgs_X[i];
            btn.name = GridLayout.catName[i];
            if (cateIndex == i)
            {
                btn.transform.SetSiblingIndex(scrollviewX.transform.GetSiblingIndex() + 1);
            }
            else
            {
                btn.transform.SetParent(inactivesX.transform, true);
            }
            btn.gameObject.SetActive(true);
            btnListX.Add(btn);

        }
    }
    

    // Use this for initialization
    void Start()
    {
        btnListX = new List<Button>();
        btnListY = new List<Button>();

        //Load this piece of crap
        LoadCategoriesForSubMenu();

        cateIndex = FindIndex(TransferCategoryName.catName);
        Debug.Log(cateIndex);

        if ((Screen.height > Screen.width) || Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            PortraitCanvas();
            isPortraitConsistence = true;
            //SceneManager.LoadScene("Lucario/MainMenuPortrait");

            return;
        }
        if ((Screen.height < Screen.width) ||
            Input.deviceOrientation == DeviceOrientation.LandscapeLeft
            || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {

            LandscapeCanvas();
            isPortraitConsistence = false;
            //SceneManager.LoadScene("Lucario/MainMenu");

            return;
        }
        if ((Screen.height > Screen.width) || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {

            PortraitCanvas();
            isPortraitConsistence = true;

            //SceneManager.LoadScene("Lucario/MainMenuPortrait");

            return;
        }
        
    }

    void CheckRotateY()
    {
        scrollviewY.gameObject.SetActive(true);
        inactivesY.gameObject.SetActive(true);
        bgY.gameObject.SetActive(true);

        scrollviewX.gameObject.SetActive(false);
        inactivesX.gameObject.SetActive(false);
        bgX.gameObject.SetActive(false);

        if (!isPortraitConsistence)
        {
            for (int i = 0; i < btnListX.Count; i++)
            {
                btnListX[i].gameObject.SetActive(false);
            }
            PortraitCanvas();
            isPortraitConsistence = true;
        }
        else
        {
            for (int i = 0; i < btnListY.Count; i++)
            {
                btnListY[i].gameObject.SetActive(true);
            }
        }
    }

    void CheckRotateX()
    {
        if (isPortraitConsistence)
        {
            for (int i = 0; i < btnListY.Count; i++)
            {
                btnListY[i].gameObject.SetActive(false);
            }
            LandscapeCanvas();
            isPortraitConsistence = false;
        }
        else
        {
            for (int i = 0; i < btnListX.Count; i++)
            {
                btnListX[i].gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update () {
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
