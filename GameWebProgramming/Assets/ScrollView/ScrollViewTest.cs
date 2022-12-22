using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScrollViewTest : MonoBehaviour {

	public RectTransform rcContent;
	public ScrollRect scrollrect;
	public RectTransform rcBase;
	public Item Baseobj;
	Vector3 Origin = Vector3.zero;

	List<Item> ViewList = new List<Item>();
	Queue<Item> _reservedList = new Queue<Item>();
	List<int> infoList = new List<int>();

	int nCount = 0;
	float curTime;
	// Use this for initialization
	void Start () 
	{
		curTime = Time.time;
	}	
	void AddItem()
	{
		infoList.Add(++nCount);
	}
	public void AttachParent(Transform p, Transform m, Vector3 pos)
	{
		m.SetParent(p);
		m.localScale = Vector3.one;
		m.position = pos;
	}
	public void Showlist()
	{

		if (Origin.y == 0)
			Origin = rcContent.sizeDelta;
		
		int lineCount = infoList.Count - 1;
		
		// 라인 늘어 날때 ...
		// 스크롤 영역 다시 설정...
		
		// 우선 스크롤 영역이 늘어난 크기를 계산한다...
		rcContent.sizeDelta = new Vector2(Origin.x, Origin.y + (rcBase.rect.height) * lineCount);
		
		float deltay = rcContent.sizeDelta.y - Origin.y;
		
		int nLine = 0;

		for (int i = 0; i < infoList.Count; ++i)
		{
			Item newObj = Instantiate(Baseobj);

			newObj.SetInfo(infoList[i]);

			AttachParent( rcContent.gameObject.transform,
			              newObj.transform,
			              Vector3.zero);

			//ViewList.Add(newObj);

			newObj.GetComponent<RectTransform>().localPosition = 
				new Vector3( rcBase.localPosition.x,
			                 rcBase.localPosition.y + deltay * 0.5f - (nLine * rcBase.rect.height),
			                 rcBase.localPosition.z);

			newObj.gameObject.SetActive(true);
			
			++nLine;
			
			_reservedList.Enqueue(newObj);
		}

		scrollrect.verticalNormalizedPosition = 0.0f;
	}
	public void Reset()
	{
		Item[] _slot = _reservedList.ToArray();
		
		for (int i = 0; i < _slot.Length; i++)
		{
			DestroyImmediate(_slot[i].gameObject);
		}

		_reservedList.Clear();
		
	}		
	void Update () 
	{
		if( Input.GetKeyDown(KeyCode.F1) )
		{
			AddItem();
			Reset();
			Showlist();
		}

		if( Input.GetMouseButtonDown(1))
		{
			Reset();
		}
	}
}


