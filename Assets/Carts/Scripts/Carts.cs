using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Carts {

    private static List<CartDTO> cart;
    private static List<ItemDTO> list;

    static Carts() {
        if (list == null)
        {
            list = LoadItemsFromDatabase.getItemsArray();
        }
    }

    public static void AddToCart(int ProductId)
    {
        ItemDTO item = list.First(q => q.ProductId == ProductId);
        if (item != null)
        {
            if (cart == null)
            {
                cart = new List<CartDTO>();
            }

            cart.Add(new CartDTO
            {
                Item = item,
                Quantity = 1,
            });
            Debug.Log("Added Success");
        }
    }

    public static void UpdateCart(int ProductId, int quantity)
    {
        if (cart != null)
        {
            CartDTO dt = cart.First(q => q.Item.ProductId == ProductId);
            dt.Quantity = quantity;
            Debug.Log("Quantity updated to " + quantity);
        }
    }

    public static void RemoveCart(int ProductId)
    {
        if (cart != null)
        {
            cart.RemoveAll(q => q.Item.ProductId == ProductId);
            Debug.Log("Removed success");
        }
    }

    public static List<CartDTO> GetListCart()
    {
        return cart;
    }

    
}
