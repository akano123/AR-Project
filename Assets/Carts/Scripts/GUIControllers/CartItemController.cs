using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CartItemController : MonoBehaviour {

    public Text Name;
    public Text Price;
    public InputField Quantity;
    public Button Add;
    public Button Minus;
    public Button Remove;

    // Leave blank
    public int productId;

	// Use this for initialization
	void Start () {
        Add.onClick.AddListener(AddButton);
        Minus.onClick.AddListener(MinusButton);
        // quantity listener
        Quantity.onValueChanged.AddListener(delegate { QuantityChanged(); });
        //Quantity.onEndEdit.AddListener(delegate { QuantityEndEdit(); });
        // remove listener
        Remove.onClick.AddListener(delegate { RemoveFromCartAndDestroyObj(); });
	}

    private void RemoveFromCartAndDestroyObj()
    {
        Carts.RemoveCart(productId);
        Destroy(this.gameObject);
    }

    private void QuantityEndEdit()
    {
        if (string.IsNullOrEmpty(Quantity.text))
        {
            Quantity.text = "1";
        }

        Carts.UpdateCart(productId, int.Parse(Quantity.text));
    }

    private void QuantityChanged()
    {
        int num = 1;

        if (!string.IsNullOrEmpty(Quantity.text))
        {
            try
            {
                num = int.Parse(Quantity.text);
                if (num > 1000)
                {
                    num = 1000;
                }

                if (num < 0)
                {
                    num = 1;
                } 
            }
            catch (Exception)
            {
                num = 1;
            }

            Quantity.text = num.ToString();
            Carts.UpdateCart(productId, int.Parse(Quantity.text));
        } else
        {
            Carts.UpdateCart(productId, 1);
        }
    }

    private void AddButton()
    {
        int num;
        try
        {
            num = int.Parse(Quantity.text);
        }
        catch (Exception)
        {
            num = 1;
        }

        if (num >= 0)
        {
            num = num + 1;
        } else
        {
            num = 1;
        }

        Quantity.text = num.ToString();
    }

    private void MinusButton()
    {
        int num;
        try
        {
            num = int.Parse(Quantity.text);
        } catch (Exception) {
            num = 1;
        }

        if (num > 2)
        {
            num = num - 1;
        }
        else
        {
            num = 1;
        }

        Quantity.text = num.ToString();
    }
}
