using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Bomb_Has_Been_Planted.Classes;
using System;

public class SortCategory : MonoBehaviour
{

    public List<GameObject> listModel;
    public Button CatButton;
    public Button ModButton;
    public GameObject CategoryPanel;
    public GameObject ModelPanel;
    public Button TakeAway;
    public string typeBuy;
    private List<ItemDTO> listof_itemDTO;
    private SortedDictionary<string, List<ModelEntity>> database_map;

    private static SortCategory _instance;
    public static SortCategory instance { get { return _instance; } }

    //Use this for initialization
    void Start()
    {
        //init type
        this.typeBuy = "Take Away";
        //init singleton
        _instance = this;

        DataToMap_CategoryButton();

        List<string> keyList = new List<string>(database_map.Keys);

        AddListenerTakeAway(TakeAway);
    }

    private void DataToMap_CategoryButton()
    {
        database_map = new SortedDictionary<string, List<ModelEntity>>();
        listof_itemDTO = LoadItemsFromDatabase.getItemsArray();
        listof_model = new List<ModelEntity>();

        foreach (var item in listof_itemDTO)
        {
            int i = listof_itemDTO.IndexOf(item);
            ModelEntity me = new ModelEntity(item, this.listModel[i % listModel.Count], this.listModel[i % listModel.Count]);
            if (database_map.ContainsKey(item.Category.Name))
            {
                database_map[item.Category.Name].Add(me);
            }
            else
            {
                //listof_category_name.Add(item.Category.Name);
                numberof_category++;
                database_map.Add(item.Category.Name, new List<ModelEntity>());
                database_map[item.Category.Name].Add(me);
            }
        }
        CreateButton(CatButton, CategoryPanel);
    }

    public List<GameObject> listof_GO_topass()
    {
        List<GameObject> temp = new List<GameObject>();
        if (listof_model != null)
        {
            foreach (ModelEntity item in listof_model)
            {
                temp.Add(item.getModel(typeBuy));
            }
        }
        return temp;
    }
    public List<ModelEntity> listof_model;
    public int current_item_id;
    private int numberof_category = 0;
    private int numberof_model = 0;
    public List<ItemUnity> listof_item_unity;
    private ItemUnity current_item;
    private bool IsCategoryCreated = false;
    //-----------------------------------------------------------------------------------------------
    //private Dictionary<string, ModelEntity> mapof_model_name_Model;               // model name - product id
    //private List<string> listof_category_name;
    //private List<Button> listof_category_button;                            // not in use remove it if you want
    //private List<Button> listof_product_button;
    //-----------------------------------------------------------------------------------------------
    public ModelEntity current_product_model;
    public GameObject current_product_item;
    private Button current_product_button;

    private void CreateButton(Button base_button, GameObject ParentPanel)
    {
        float FirstX = base_button.transform.position.x;
        float FirstY = base_button.transform.position.y;

        for (int i = 0; i < (!IsCategoryCreated ? numberof_category : numberof_model); ++i)
        {
            Button moreButton = Instantiate(base_button) as Button;
            moreButton.transform.SetParent(ParentPanel.transform, false);
            moreButton.transform.position = new Vector3(FirstX, FirstY, 0.0f);

            if (!IsCategoryCreated)
            {
                GenerateButtonCategory(i, moreButton);
                if ((i + 2) == numberof_category)
                {
                    IsCategoryCreated = true;
                }
            }
            else
            {
                GenerateButtonModel(i, moreButton);
                if (i == 0)
                {
                    SetCurrentItem(i);
                }
            }
            FirstY = base_button.transform.position.y - (70 * (i + 1));
        }

    }

    public void SetCurrentItem(int id)
    {
        current_item_id = id;
        current_item = listof_item_unity[id];
        ChangeButtonColor(current_item.Button);
        current_product_model = current_item.Model;
        current_product_item = current_item.Model.getModel(typeBuy);

        MenuController.instance.current_item_number = current_item_id;
        MenuController.instance.currentItem = current_product_item;
    }
        
    private void GenerateButtonCategory(int i, Button moreButton)
    {
        List<string> tmp = new List<string>(database_map.Keys);
        moreButton.GetComponentInChildren<Text>().text = tmp[i];
        moreButton.name = tmp[i];

        Button temp = moreButton;
        moreButton.onClick.AddListener(() => AddListenerForCategoryButton(temp));
    }

    private void AddListenerForCategoryButton(Button temp)
    {
        string button_text = temp.name;

        database_map.TryGetValue(button_text, out listof_model);
        numberof_model = listof_model.Count;
        listof_item_unity = new List<ItemUnity>();
        CreateButton(ModButton, ModelPanel);
        MenuController.instance.listModel = listof_GO_topass();
    }

    private void GenerateButtonModel(int i, Button moreButton)
    {
        string name = listof_model[i].itemDTO.ProductName;
        moreButton.name = name;
        moreButton.GetComponentInChildren<Text>().text = name;

        ItemUnity item = new ItemUnity((i), name, moreButton, listof_model[i]);
        listof_item_unity.Add(item);

        Button temp = moreButton;
        moreButton.onClick.AddListener(() => AddListenerForModelButton(temp, (i)));
    }

    private void AddListenerForModelButton(Button temp, int aNumber)
    {
        ItemUnity iu = listof_item_unity[aNumber];
        current_item_id = aNumber;
        SetCurrentItem(aNumber);
    }

    private void SwitchTypeOfModel_takeaway_or_indoor(ModelEntity me)
    {
        if (current_product_item != null)
        {
            current_product_item.SetActive(false);
        }

        current_product_model = me;
        current_product_item = current_product_model.getModel(typeBuy);
        current_product_item.SetActive(true);
        current_product_item.transform.localPosition = Vector3.zero;

        MenuController.instance.listModel = listof_GO_topass();
        this.SetCurrentItem(current_item_id);
    }

    private void ChangeButtonColor(Button temp)
    {
        if (current_product_button != null)
        {
            current_product_button.image.color = Color.white;
        }
        current_product_button = temp;
        current_product_button.image.color = Color.red;
    }

    public void UpdateTypeBuy(Button button)
    {
        string s = button.GetComponentInChildren<Text>().text;
        button.GetComponentInChildren<Text>().text = s == "Take Away" ? "In House" : "Take Away";
        this.typeBuy = button.GetComponentInChildren<Text>().text;

        if (current_product_model != null)
        {
            SwitchTypeOfModel_takeaway_or_indoor(current_product_model);
        }
    }

    public void AddListenerTakeAway(Button btn)
    {
        Button button = btn;
        btn.onClick.AddListener(() => { UpdateTypeBuy(button); });
    }
}
