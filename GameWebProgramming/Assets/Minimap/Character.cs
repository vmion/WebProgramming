using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public JoyStick stick;
    public Minimap minimap;

    float fSpeed = 10.0f;
    Vector3 StartPos = Vector3.zero;
    Vector2 MapSize = new Vector2(5, 2.5f);

    void Start () {
        StartPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position.x > 5  )
        {
            transform.position = new Vector3(5, transform.position.y, transform.position.z);
            return;
        }
        if (transform.position.x < -5)
        {
            transform.position = new Vector3(-5, transform.position.y, transform.position.z);
            return;
        }
        if (transform.position.z > 5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 5);
            return;
        }
        if (transform.position.z < -5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
            return;
        }

        Vector3 tmp = transform.position;
        tmp.x += stick.StickVec.x * Time.deltaTime * fSpeed;
        tmp.z += stick.StickVec.y * Time.deltaTime * fSpeed;
        transform.position = tmp;

        if (stick.StickVec.magnitude > 0)
        {
            float fDeltax = transform.position.x - StartPos.x;
            float fDeltay = transform.position.z - StartPos.z;

            float ratiox = fDeltax / MapSize.x;
            float ratioy = fDeltay / MapSize.y;
            Debug.Log(ratiox.ToString() + "    " + ratioy.ToString());

            minimap.UpdatePos(ratiox, ratioy);
        }

    }
}
