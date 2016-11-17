using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ListCartController : MonoBehaviour {

    public GameObject Panel;
    public GameObject Prefab;

    public Button Next;
    public Button Back;

    public Text Total;

    // Use this for initialization
    IEnumerator Start () {
        // TEMPORARY
        yield return StartCoroutine(InitiateCartItem());
        StartCoroutine(LoadCartToGUI());
    }

    void Update()
    {
        if (Carts.GetListCart() == null)
        {
            Debug.Log("Nothing");
        } else
        {
            Debug.Log(Carts.GetListCart().Count);
        }

        TotalPrice();
    }

    IEnumerator LoadCartToGUI()
    {
        yield return 0;
        if (Carts.GetListCart() != null)
        {
            foreach (CartDTO ca in Carts.GetListCart())
            {
                GameObject newPrefab = Instantiate(Prefab);
                CartItemController con = newPrefab.GetComponent<CartItemController>();

                con.productId = ca.Item.ProductId;
                con.Name.text = ca.Item.ProductName;
                con.Price.text = ca.Item.Price.ToString();
                con.Quantity.text = ca.Quantity.ToString();

                con.transform.SetParent(Panel.transform);
                con.transform.localPosition = Vector3.one;
            }
        }
    }

    void TotalPrice()
    {
        double sum = 0;
        if (Carts.GetListCart() != null)
        {
            foreach (CartDTO i in Carts.GetListCart())
            {
                sum += (i.Item.Price * (double)i.Quantity);
            }
        }
        Total.text = sum.ToString();
    }

    IEnumerator InitiateCartItem()
    {
        yield return 0;
        var list = LoadItemsFromDatabase.getItemsArray();
        foreach (ItemDTO i in list)
        {
            Carts.AddToCart(i.ProductId);
        }
    }
}
