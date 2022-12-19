using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCollision : MonoBehaviour
{    
    BoxCollider collider; //������ �ڽ� �ö��̴�
    public BoxCollider other; //�ٸ� �ڽ� �ö��̴�
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
                Debug.Log(other.name + "�� bounds�浹");
            }
        }        
    }
}
