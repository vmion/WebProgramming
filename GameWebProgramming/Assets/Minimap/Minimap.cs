using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {

    public RectTransform rcContent;
    public ScrollRect scRect;
    Vector2 normalPos = new Vector2(0.5f, 0.5f);
    void Start ()
    {
        scRect.normalizedPosition = normalPos;
    }
	
    public void UpdatePos(float fRatiox, float fRatioy)
    {
        normalPos.Set(fRatiox, fRatioy);
        scRect.normalizedPosition = normalPos;
    }

    void Update () {
		
	}
}
