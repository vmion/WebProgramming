using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCollision : MonoBehaviour
{    
    BoxCollider collider; //본인의 박스 컬라이더
    public BoxCollider other; //다른 박스 컬라이더
    bool bCollisionCheck;
    public bool COLLISIONCHECK { get; set; }    
    void Awake()
    {
        collider = GetComponent<BoxCollider>();
        //bCollisionCheck = true;
        COLLISIONCHECK = true;
    }
    void Start()
    {        
    }

    void Update()
    {
        if(COLLISIONCHECK)
        {
            if (collider.bounds.Intersects(other.bounds))
            {
                Debug.Log(other.name + "과 bounds충돌");
            }
        }        
    }
}
