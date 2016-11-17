using UnityEngine;
using System.Collections;

public enum swipeDirection
{
    None = 0,
    Left = 1,
    Right = 2,
    Hold = 4,
}


public class SwipeController : MonoBehaviour
{
    public int isNull;
    private static SwipeController _instance;
    public static SwipeController instance { get { return _instance; } }

    public swipeDirection direction { get; set; }

    private ConvertScreenUnitToWorldUnit converter;
    private Vector2 begin;
    private Vector2 end;



    // Use this for initialization
    void Awake()
    {
        this.Init();
    }




    // Update is called once per frame
    void Update()
    {
        //defualt
        this.direction = swipeDirection.None;

        //not touch case
        if (Input.touchCount < 1)
        {
            return;
        }

        //get touch
        Touch touch = Input.GetTouch(0);

        //touch phase event
        if (touch.phase == TouchPhase.Began)
        {
            this.begin = touch.position;
        }


        //if(touch.phase == TouchPhase.Moved)
        //{
        //    Vector2 direction = touch.deltaPosition.normalized;
        //    if(direction == Vector2.right)
        //    {
        //        this.direction = swipeDirection.Right;
        //        return;
        //    }
        //    if(direction == Vector2.left)
        //    {
        //        this.direction = swipeDirection.Left;
        //        return;
        //    }
        //}

        if (touch.phase == TouchPhase.Moved)
        {
            this.end = touch.position;


            if ((this.begin.x - this.end.x) < -50f)
            {
                this.direction = swipeDirection.Right;
                this.begin = touch.position;
            }
            if ((this.begin.x - this.end.x) > 50f)
            {
                this.direction = swipeDirection.Left;
                this.begin = touch.position;
            }
        }

        if (touch.phase == TouchPhase.Stationary)
        {
            this.direction = swipeDirection.Hold;
            this.begin = touch.position;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            this.direction = swipeDirection.None;
        }

    }



    public bool IsSwiping(swipeDirection dir)
    {
        return this.direction == dir;
    }


    private void Init()
    {
        _instance = this;
    }

}
