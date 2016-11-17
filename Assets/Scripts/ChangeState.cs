using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeState : MonoBehaviour
{
    public static bool clicked = false;

    public static string cateName;

    //public static GlobalCateIndex cateIndex;
    public int contNum;
    public GameObject scrollview;
    public Canvas landscape;
    public GameObject inactives;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void onClick()
    {
        //Make your button active and all others inactive
        {
            cateName = this.name;
            clicked = true;
            Debug.Log(cateName);

            Transform btn = landscape.transform.GetChild(scrollview.transform.GetSiblingIndex() + 1);
            btn.SetParent(inactives.transform, true);
            this.transform.SetParent(landscape.transform, true);
            this.gameObject.transform.SetSiblingIndex(scrollview.transform.GetSiblingIndex() + 1);
        }
        
    }
}
