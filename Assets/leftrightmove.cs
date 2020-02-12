using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class leftrightmove : VarStorage
{
    private int direction = 1;
    public float speedX;
    public float speedZ = 0;
    private float moveAreaX;
    private float boundLeft;
    private float boundRight;

    private float currentPosX;

    private Vector3 originalPos;
    private int thisBox;

    // Start is called before the first frame update
    void Start()
    {
        GameObject referenceObj = GameObject.Find("borderA");
        VarStorage getBoxID = referenceObj.GetComponent<VarStorage>();
        thisBox = getBoxID.currentBox;

        Debug.Log("this box " + thisBox);

        Vector3 moveArea = GameObject.Find("borderA").GetComponent<Collider>().bounds.size;
        Vector3 objSize = GetComponent<Collider>().bounds.size;
        moveAreaX = moveArea.x;

        originalPos = this.transform.position;

        boundLeft = originalPos.x;
        boundRight = boundLeft + moveAreaX - objSize.x;

        speedX = Random.Range(3.0f, 10.0f);

        this.gameObject.name = "obj" + thisBox;

        boxStatus[thisBox] = "uncollided";

        Debug.Log(this.gameObject.name);
        //Debug.Log("currentBox " + currentBox + "boxStatus " + boxStatus[currentBox]);

        Debug.Log("moveAreaX" + moveAreaX);
        Debug.Log("boundLeft" + boundLeft);
        Debug.Log("boundRight" + boundRight);
        Debug.Log("objSize" + objSize.x);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if (boxStatus[thisBox] == "uncollided")
        {
            GameObject thisObject = GameObject.Find("obj" + thisBox);

            //get the object's current X position
            Vector3 currentPos = thisObject.transform.position;
            currentPosX = currentPos.x;

            if (Input.GetMouseButtonDown(0))
            {
                speedX = 0;
                speedZ = -6;
            }

            //change the direction if the object hits the bounds
            if (currentPosX <= boundLeft)
            {
                direction = 1;
            }
            if (currentPosX >= boundRight)
            {
                direction = -1;
            }

            //move the object
            thisObject.transform.Translate(direction * speedX * Time.deltaTime, 0, 0);
            thisObject.transform.Translate(0, 0, speedZ * Time.deltaTime);
            //Debug.Log("currentPosX" + currentPosX);
            //Debug.Log("boundRight" + boundRight);
            //Debug.Log("direction" + direction);
        }

    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "FixedGround")
        {
            speedZ = 0;
            if (boxStatus[thisBox] == "uncollided")
            {
                GameObject referenceObj = GameObject.Find("borderA");
                VarStorage getBoxID = referenceObj.GetComponent<VarStorage>();

                boxStatus[thisBox] = "collided";
                getBoxID.currentBox += 1;
                GameObject newObj = Instantiate(this.gameObject, originalPos, Quaternion.identity) as GameObject;
                newObj.gameObject.name = "obj" + getBoxID.currentBox;
                //Debug.Log(newObj.gameObject.name);
                //Debug.Log(currentBox);
            }

            Destroy(GetComponent<Rigidbody>());
        }
    }
}
