using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class modelSlideController : MonoBehaviour
{

    public GameObject MainPanel;
    public GameObject SubPanel;
    public GameObject currentItem;
    public List<GameObject> currentList;

    int counter;

    private static modelSlideController _instance;
    public static modelSlideController instance { get { return _instance; } }



    void Start()
    {
        _instance = this;
    }


    public void btnModelInTrigger()
    {
        Debug.Log("btnModelInTrigger");
        Animator anim = this.SubPanel.GetComponent<Animator>();
        anim.SetBool("ModelPanelSlideOut", false);
        anim.SetBool("ModelPanelSlideIn", true);
        MainPanel.SetActive(false);

    }

    public void btnModelOutTrigger()
    {
        Debug.Log("btnModelOutTrigger");
        Animator animMain = this.MainPanel.GetComponent<Animator>();
        Animator anim = this.SubPanel.GetComponent<Animator>();
        MainPanel.SetActive(true);
        anim.SetBool("ModelPanelSlideIn", false);
        animMain.SetBool("CategorySlideIn", true);
        anim.SetBool("ModelPanelSlideOut", true);
        GameObject[] tag = GameObject.FindGameObjectsWithTag("ModelButton");
        for (int i = 1; i < tag.Length; i++)
        {
            Destroy(tag[i]);
        }
    }
  
}
