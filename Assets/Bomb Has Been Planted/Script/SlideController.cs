using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlideController : MonoBehaviour {

    public GameObject panel;
    public Button btnIn;
    public Button btnOut;


    public void btnInTrigger()
    {
        Debug.Log("btnInTrigger");
        Animator anim = this.panel.GetComponent<Animator>();
        anim.SetBool("slideOut", false);
        anim.SetBool("slideIn", true);
        btnIn.gameObject.SetActive(false);

    }

    public void btnOutTrigger()
    {
        Debug.Log("btnOutTrigger");
        Animator anim = this.panel.GetComponent<Animator>();
        anim.SetBool("slideIn", false);
        anim.SetBool("slideOut", true);
        btnIn.gameObject.SetActive(true);
    }

    public void btnCatInTrigger()
    {
        Debug.Log("btnCatInTrigger");
        Animator anim = this.panel.GetComponent<Animator>();
        anim.SetBool("CategorySlideOut", false);
        anim.SetBool("CategorySlideIn", true);
        btnIn.gameObject.SetActive(false);

    }

    public void btnCatOutTrigger()
    {
        Debug.Log("btnCatOutTrigger");
        Animator anim = this.panel.GetComponent<Animator>();
        anim.SetBool("CategorySlideIn", false);
        anim.SetBool("CategorySlideOut", true);
        btnIn.gameObject.SetActive(true);
    }

    public void btnModelInTrigger()
    {
        Debug.Log("btnModelInTrigger");
        Animator anim = this.panel.GetComponent<Animator>();
        anim.SetBool("ModelPanelSlideOut", false);
        anim.SetBool("ModelPanelSlideIn", true);
        btnIn.gameObject.SetActive(false);

    }

    public void btnModelOutTrigger()
    {
        Debug.Log("btnModelOutTrigger");
        Animator anim = this.panel.GetComponent<Animator>();
        anim.SetBool("ModelPanelSlideIn", false);
        anim.SetBool("ModelPanelSlideOut", true);
        btnIn.gameObject.SetActive(true);
    }
}
