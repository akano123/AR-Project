using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridLayout : MonoBehaviour
{
    //Create Button
    private GridLayoutGroup grid;
    private Button[] idk;
    private Sprite[] sImage;
    private int count;
    private Image[] pageImage;
    private bool isLandscape = true;
    public static string[] catName;

    public static Dictionary<string, List<ItemDTO>> hashmap = new Dictionary<string, List<ItemDTO>>();

    //Swipe Left Or Right
    private Touch initialTouch = new Touch();
    private float distance = 0;
    private bool hasSwiped = false;

    void Awake()
    {
        grid = GetComponent<GridLayoutGroup>();
        count = 0;

        //Load Category From Database
        loadDatabaseCategory();

        //Load Button Images
        sImage = Resources.LoadAll<Sprite>("MainMenuButtons");

        //Create Page Images
        pageImage = GameObject.FindGameObjectWithTag("Page").GetComponentsInChildren<Image>();
        int pageNum = (catName.Length / 6 > 3) ? 3 : (catName.Length % 6 == 0) ? catName.Length / 6 : catName.Length / 6 + 1;
        for (int i = (pageNum == 1) ? 0 : pageNum; i < pageImage.Length; i++)
        {
            pageImage[i].gameObject.SetActive(false);
        }

        //Load Button
        loadButton();

        //Check Orientation -> Layout
        if (Screen.width < Screen.height)
        {
            portrait();
        }
        else
        {
            landscape();
        }
    }

    void Update()
    {
        if ((Input.deviceOrientation == DeviceOrientation.FaceUp || Input.deviceOrientation == DeviceOrientation.FaceDown) && isLandscape)
        {
            landscape();
        }
        else if ((Input.deviceOrientation == DeviceOrientation.FaceUp || Input.deviceOrientation == DeviceOrientation.FaceDown) && !isLandscape)
        {
            portrait();
        }
        else if ((Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown))
        {
            portrait();
        }
        else if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight))
        {
            landscape();
        }
    }

    void landscape()
    {
        grid.padding.top = Screen.height / 5;
        grid.padding.bottom = Screen.height / 5;
        grid.padding.left = Screen.width / 7;
        grid.padding.right = Screen.width / 7;
        grid.spacing = new Vector2(30f, 20f);
        grid.cellSize = new Vector2((Screen.width - grid.spacing.x - grid.padding.left - grid.padding.right) / 2, (Screen.height - grid.padding.top - grid.padding.bottom - grid.spacing.y) / 3);
        isLandscape = true;
    }

    void portrait()
    {
        grid.padding.top = Screen.height / 5;
        grid.padding.bottom = Screen.height / 5;
        grid.padding.left = Screen.width / 7;
        grid.padding.right = Screen.width / 7;
        grid.spacing = new Vector2(30f, 30f);
        grid.cellSize = new Vector2(Screen.width - grid.padding.left - grid.padding.right, (Screen.height - grid.padding.top - grid.padding.bottom - grid.spacing.y) / 6);
        isLandscape = false;
    }

    void loadButton()
    {
        Button button = GetComponentInChildren<Button>();
        idk = new Button[6];
        for (int i = 0; i < ((catName.Length < 6) ? catName.Length % 6 : 6); i++)
        {
            Button moreButton = Instantiate(button) as Button;
            idk[i % 6] = moreButton;
            moreButton.transform.SetParent(transform, false);
            moreButton.gameObject.SetActive(true);
            moreButton.image.sprite = sImage[i];
            moreButton.name = catName[i];
            count += 1;
        }
        button.gameObject.SetActive(false);
    }

    public void clickNext()
    {
        if (catName.Length - count > 0)
        {
            int length = (catName.Length - count < 6) ? catName.Length - count : 6;
            for (int i = count; i < 6 + count; i++)
            {
                if (i < length + count)
                {
                    idk[i % 6].image.sprite = sImage[i];
                    idk[i % 6].name = catName[i];
                }
                else
                {
                    idk[i % 6].gameObject.SetActive(false);
                }
            }
            count += length;

            //Update Page Images
            if (count != catName.Length || count < 12)
            {
                pageImage[1].GetComponent<Image>().color = Color.white;
                pageImage[0].GetComponent<Image>().color = Color.black;
            }
            else
            {
                pageImage[2].GetComponent<Image>().color = Color.white;
                pageImage[1].GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void clickBack()
    {
        if (count > 6)
        {
            int length = (count == catName.Length && count % 6 != 0) ? catName.Length % 6 : 6;
            for (int i = count - (6 + length); i < count - length; i++)
            {
                idk[i % 6].gameObject.SetActive(true);
                idk[i % 6].image.sprite = sImage[i];
                idk[i % 6].name = catName[i];
            }
            count -= length;

            //Update Page Images
            if (count == 6)
            {
                pageImage[0].GetComponent<Image>().color = Color.white;
                pageImage[1].GetComponent<Image>().color = Color.black;
            }
            else
            {
                pageImage[1].GetComponent<Image>().color = Color.white;
                pageImage[2].GetComponent<Image>().color = Color.black;
            }
        }
    }


    //Swipe Left Or Right
    void SwipeLeftOrRight()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                initialTouch = t;
            }
            else if (t.phase == TouchPhase.Moved && !hasSwiped)
            {
                float deltaX = initialTouch.position.x - t.position.x;
                float deltaY = initialTouch.position.y - t.position.y;
                distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                bool swipedSideways = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

                if (distance > 50f)
                {
                    if (swipedSideways && deltaX > 0) //swiped left
                    {
                        clickNext();
                    }
                    else if (swipedSideways && deltaX <= 0) //swiped right
                    {
                        clickBack();
                    }
                    else if (!swipedSideways && deltaY > 0) //swiped down
                    {

                    }
                    else if (!swipedSideways && deltaY <= 0)  //swiped up
                    {

                    }

                    hasSwiped = true;
                }

            }
            else if (t.phase == TouchPhase.Ended)
            {
                initialTouch = new Touch();
                hasSwiped = false;
            }
        }
    }

    void FixedUpdate()
    {
        SwipeLeftOrRight();
    }

    void loadDatabaseCategory()
    {
        foreach (ItemDTO item in LoadItemsFromDatabase.getItemsArray())
        {
            if (!hashmap.ContainsKey(item.Category.Name))
            {
                hashmap.Add(item.Category.Name, new List<ItemDTO>());
            }

            hashmap[item.Category.Name].Add(item);
        }
        catName = new string[hashmap.Keys.Count];
        hashmap.Keys.CopyTo(catName, 0);
    }
}
