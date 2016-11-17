using UnityEngine;
using System.Collections;

public class ConvertScreenUnitToWorldUnit : MonoBehaviour
{

    private static ConvertScreenUnitToWorldUnit _instance;
    public static ConvertScreenUnitToWorldUnit instance { get { return _instance; } }

    public float _distance { get; set; }
    private Camera ARcamera;
    private GameObject imageTarget;


    //constructor
    void Awake()
    {
        this.Init();
    }

    void Update()
    {
        //update distance
        this._distance = Vector3.Distance(this.ARcamera.transform.position, this.imageTarget.transform.position);
    }

    private void Init()
    {
        _instance = this;
        this.ARcamera = Camera.main;
        this.imageTarget = this.gameObject;
        this._distance = Vector3.Distance(this.ARcamera.transform.position, this.imageTarget.transform.position);

    }





    public Vector3 ConvertScreenPointToWorldPoint(Vector2 screenPoint)
    {
        return this.ARcamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, this._distance));
    }
































    public Vector3 GetBorderLeftWorldPoint(Vector3 currentModel)
    {
        Vector3 screenPoint = this.ARcamera.WorldToScreenPoint(currentModel);
        Vector3 screenLeftBorder = new Vector3(0 - 150, screenPoint.y, screenPoint.z);
        return this.ARcamera.ScreenToWorldPoint(new Vector3(screenLeftBorder.x, screenLeftBorder.y, this._distance));
    }

    public Vector3 GetBorderRightWorldPoint(Vector3 currentModel)
    {
        Vector3 screenPoint = this.ARcamera.WorldToScreenPoint(currentModel);
        Vector3 screenLeftBorder = new Vector3(Screen.width, screenPoint.y, screenPoint.z);
        return this.ARcamera.ScreenToWorldPoint(new Vector3(screenLeftBorder.x + 150, screenLeftBorder.y, this._distance));
    }

    public float GetDistanceToLeftBorder(Vector3 currentModel)
    {
        Vector3 worldLeftBorder = this.GetBorderLeftWorldPoint(currentModel);
        return Vector3.Distance(currentModel, worldLeftBorder);
    }

    public float GetDistanceToRightBorder(Vector3 currentModel)
    {
        Vector3 worldRightBorder = this.GetBorderRightWorldPoint(currentModel);
        return Vector3.Distance(currentModel, worldRightBorder);
    }

}
