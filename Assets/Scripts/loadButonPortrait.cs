using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class loadButonPortrait : MonoBehaviour
{
    public Button button;
    private float FirstX;
    private float FirstY;
    private Sprite[] sImage;
    private int count;
    private Button[] idk;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void Awake()
    {

        count = 0;
        sImage = Resources.LoadAll<Sprite>("MainMenuButtons");
        loadButton();
    }

    void loadButton()
    {
        idk = new Button[6];

        FirstX = button.transform.position.x;
        FirstY = button.transform.position.y;
        for (int i = 0; i < 6; i++)
        {
            Button moreButton = Instantiate(button) as Button;
            idk[i % 6] = moreButton;

            moreButton.transform.SetParent(transform, false);
            //moreButton.GetComponent<RectTransform>().sizeDelta(m);
            moreButton.gameObject.SetActive(true);
            moreButton.transform.position = new Vector3(FirstX, FirstY, 0.0f);
            moreButton.image.sprite = sImage[i];

            FirstX = button.transform.position.x;
            FirstY = FirstY - (Screen.height / 7);
            count += 1;
        }
    }

    public void clickNext()
    {
        if (sImage.Length - count > 0)
        {
            Debug.Log("ClickNext: " + count);
            int length = (sImage.Length - count < 6) ? sImage.Length - count : 6;
            for (int i = count; i < 6 + count; i++)
            {
                if (i < length + count)
                {
                    idk[i % 6].image.sprite = sImage[i];
                }
                else
                {
                    idk[i % 6].gameObject.SetActive(false);
                }
            }
            count += length;
            Debug.Log("Count: " + count);
        }
    }

    public void clickBack()
    {
        if (count > 6)
        {
            Debug.Log("ClickBack: " + count);
            int length = (count == sImage.Length) ? sImage.Length % 6 : 6;
            for (int i = count - (6 + length); i < count - length; i++)
            {
                idk[i % 6].gameObject.SetActive(true);
                idk[i % 6].image.sprite = sImage[i];
            }
            count -= length;
            Debug.Log("Count: " + count);
        }
    }

}
