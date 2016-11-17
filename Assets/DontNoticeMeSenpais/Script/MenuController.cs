using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //singleton
    private static MenuController _instance;
    public static MenuController instance { get { return _instance; } }

    //init some param of list
    private List<GameObject> _listModel;

    //bien nay cho m get/set
    public List<GameObject> listModel;

    //item for swipe
    public GameObject previousItem;
    public GameObject _currentItem;
    public GameObject nextItem;
    public int current_item_number;

    //bien nay cho m get/set
    public GameObject currentItem
    {
        get
        {
            GameObject result = null;
            if (this.cl1 != null && this.cl2 != null)
            {
                result = this._currentItem;
            }

            if (this._currentItem == this.cl1)
            {
                result = this._listModel[0];
            }

            if (this._currentItem == this.cl2)
            {
                result = this._listModel[1] == this.cl1 ? this._listModel[0] : this._listModel[1];
            }
            return result;
        }
        set
        {
            if (this._currentItem != null)
            {
                this._currentItem.SetActive(false);
                this.previousItem.SetActive(false);
                this.nextItem.SetActive(false);
            }
            this._currentItem = value;
            int i = this._listModel.IndexOf(this._currentItem);
            this.previousItem = this._listModel[(i - 1 + this._listModel.Count) % this._listModel.Count];
            this.nextItem = this._listModel[(i + 1 + this._listModel.Count) % this._listModel.Count];
        }
    }

    //swipe properties and logic
    private ConvertScreenUnitToWorldUnit converter;
    private Camera ARcamera;
    private GameObject imageTarget;
    private SwipeController swipeController;
    public bool isMovingLeft;               //  ------------------------------------------
    public bool isMovingRight;              //  ------------------------------------------

    //clone gameObject if menu item < 3 in category
    GameObject cl1 = null, cl2 = null;


    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        this.Init();
    }

    // Update is called once per frame
    void Update()
    {
        //if(SortCategory.instance.current_item != null)
        //{
        //    this._currentItem = SortCategory.instance.current_item;
        //}

        this.UpdateModel();

        if (isMovingLeft)
        {
            this.isMovingLeft = true;
            this.MoveModelsToLeft(Time.deltaTime * 3);
            
            if (Vector3.Distance(Vector3.zero, this.nextItem.transform.localPosition) < 0.2f)
            {
                this.IncreaseCurrentModel();
                this.ResetAllModelPosition();
                this.isMovingLeft = false;
                return;
            }
            return;
        }

        if (isMovingRight)
        {
            this.isMovingRight = true;
            this.MoveModelsToRight(Time.deltaTime * 3);
            
            if (Vector3.Distance(Vector3.zero, this.previousItem.transform.localPosition) < 0.2f)
            {
                this.DecreaseCurrentModel();
                this.ResetAllModelPosition();
                this.isMovingRight = false;
                return;
            }
            return;
        }

        if (this.swipeController.IsSwiping(swipeDirection.Left))
        {
            this.isMovingLeft = true;
            return;
        }

        if (this.swipeController.IsSwiping(swipeDirection.Right))
        {
            this.isMovingRight = true;
            return;
        }




        if (!isMovingLeft && !isMovingRight)
        {
            //if (SortCategory.instance.listof_GO_topass().Count >= 3)
            //{
            //    this.current_item_number = this.listModel.IndexOf(this._currentItem);
            //    SortCategory.instance.SetCurrentItem(this.current_item_number);
            //}
            this.ResetAllModelPosition();
            return;
        }



    }


    private void Init()
    {
        this.ARcamera = Camera.main;
        this.imageTarget = this.gameObject;
        this.swipeController = SwipeController.instance;
        this.converter = ConvertScreenUnitToWorldUnit.instance;

        //swipe direction
        this.isMovingLeft = false;
        this.isMovingRight = false;

        //init private list
        this._listModel = this.listModel;

        //init three item to swipe
        this._listModel = new List<GameObject>();

    }


    private void ResetAllModelPosition()
    {

        this.previousItem.transform.localPosition = this.converter.GetBorderLeftWorldPoint(this._currentItem.transform.position);
        this.previousItem.SetActive(true);

        this._currentItem.transform.localPosition = Vector3.zero;
        this._currentItem.SetActive(true);

        this.nextItem.transform.localPosition = this.converter.GetBorderRightWorldPoint(this._currentItem.transform.position);
        this.nextItem.SetActive(true);
    }

    public void IncreaseCurrentModel()
    {
        int i = this._listModel.IndexOf(this._currentItem);
        this.previousItem.SetActive(false);
        this.previousItem = this._currentItem;
        this._currentItem = this.nextItem;
        this.nextItem = this._listModel[(i + 2) % this._listModel.Count];
        if (SortCategory.instance.listof_GO_topass().Count >= 2)
        {
            this.current_item_number = (current_item_number < listModel.Count - 1) ? (current_item_number + 1) : 0;
            //this.current_item_number = this.listModel.IndexOf(this._currentItem);
            SortCategory.instance.SetCurrentItem(this.current_item_number);
        }
    }


    public void DecreaseCurrentModel()
    {
        int i = this._listModel.IndexOf(this._currentItem);
        this.nextItem.SetActive(false);
        this.nextItem = this._currentItem;
        this._currentItem = this.previousItem;
        this.previousItem = this._listModel[(i - 2 + this._listModel.Count) % this._listModel.Count];
        if (SortCategory.instance.listof_GO_topass().Count >= 2)
        {
            this.current_item_number = (current_item_number > 0) ? (current_item_number - 1) : (listModel.Count - 1);
            //this.current_item_number = this.listModel.IndexOf(this._currentItem);
            SortCategory.instance.SetCurrentItem(this.current_item_number);
        }
    }



    public void MoveModelsToLeft(float distance)
    {
        this._currentItem.transform.Translate(Vector3.left * Time.deltaTime * 2, this.ARcamera.transform);
        this.previousItem.transform.Translate(Vector3.left * Time.deltaTime * 2, this.ARcamera.transform);
        this.nextItem.transform.Translate(Vector3.left * Time.deltaTime * 2, this.ARcamera.transform);

        //current_item_number = (current_item_number < listModel.Count - 1) ? (current_item_number + 1) : 0;
    }

    public void MoveModelsToRight(float distance)
    {
        this._currentItem.transform.Translate(Vector3.right * Time.deltaTime * 2, this.ARcamera.transform);
        this.previousItem.transform.Translate(Vector3.right * Time.deltaTime * 2, this.ARcamera.transform);
        this.nextItem.transform.Translate(Vector3.right * Time.deltaTime * 2, this.ARcamera.transform);

        //current_item_number = (current_item_number > 0) ? (current_item_number - 1) : (listModel.Count - 1);
    }

    // update current menu item and list item of categgory per frame
    private void UpdateModel()
    {
        //if(SortCategory.instance.listof_GO_topass() != null)
        //{
        //    this.listModel = SortCategory.instance.listof_GO_topass();
        //}
        // new list
        if (this._listModel.Count == 0 || this.listModel[0] != this._listModel[0])
        {
            foreach (var item in this._listModel)
            {
                item.SetActive(false);
            }

            //destroy two clone
            if (cl1 != null)
            {
                Object.Destroy(cl1);
                Object.Destroy(cl2);
            }

            this._listModel = this.listModel;
            if (this._currentItem == null)
            {
                this._currentItem = this._listModel[0];
            }
            //init new clone
            if (this.listModel.Count < 3)
            {
                int count = this.listModel.Count;
                this.cl1 = GameObject.Instantiate(this.listModel[0]);
                this.cl2 = GameObject.Instantiate(count == 1 ? this.listModel[0] : this.listModel[1]);
                this.cl1.transform.parent = this.transform;
                this.cl2.transform.parent = this.transform;
                //add to private list
                this._listModel.Add(cl1);
                this._listModel.Add(cl2);
            }
            int i = this._listModel.IndexOf(this._currentItem);
            this.previousItem = this._listModel[(i - 1 + this._listModel.Count) % this._listModel.Count];
            this.nextItem = this._listModel[(i + 1 + this._listModel.Count) % this._listModel.Count];

        }
        //if(SortCategory.instance.listof_model != null)
        //{
        //    SortCategory.instance.SetCurrentItem(current_item_number);
        //}


        //if(this.currentItem == null || this._currentItem == null)
        //{
        //    this.currentItem = this._listModel[0];
        //    this._currentItem = this._listModel[0];
        //}



    }


}