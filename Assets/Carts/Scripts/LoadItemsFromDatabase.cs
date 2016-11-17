using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.Net;
using System.IO;

public class LoadItemsFromDatabase {

    private static List<ItemDTO> items;

    public static List<ItemDTO> getItemsArray()
    {
        if (items == null)
        {
            LoadDatabase();
        }

        return items;
    }

    private static void LoadDatabase()
    {
        string url = "http://202.78.227.93:8027/api/product/GetProductList/494EC308-7344-41A9-9347-D05754002CFC/7";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = WebRequestMethods.Http.Get;
        request.ContentType = "application/json; charset=utf-8";
        HttpWebResponse respone = (HttpWebResponse)request.GetResponse();
        using (var reader = new StreamReader(respone.GetResponseStream()))
        {
            var json = reader.ReadToEnd();
            items = JsonMapper.ToObject<List<ItemDTO>>(json);
        }
    }
}
