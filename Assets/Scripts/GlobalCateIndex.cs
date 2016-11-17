using UnityEngine;
using System.Collections;

public class GlobalCateIndex : MonoBehaviour {

    private static GlobalCateIndex _instance;

    public static GlobalCateIndex Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Object.Destroy(this);
    }

    public int cateIndex = 0;
}
