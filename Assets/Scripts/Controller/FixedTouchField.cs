using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	//public static FixedTouchField Instance;
	//[HideInInspector]
	public Vector2 TouchDist;
	[HideInInspector]
	public Vector2 PointerOld;
	[HideInInspector]
	protected int PointerId;
	[HideInInspector]
	public bool Pressed;

	public bool touchDown { get; private set; }
	public bool touchUp { get; private set; }
    private void Awake()
    {
		//Instance = this;
    }
    // Use this for initialization
    void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Pressed)
		{
			if (PointerId >= 0 && PointerId < Input.touches.Length)
			{
				TouchDist = Input.touches[PointerId].position - PointerOld;
				PointerOld = Input.touches[PointerId].position;
			}
			else
			{
				TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
				PointerOld = Input.mousePosition;
			}
		}
		else
		{
			TouchDist = new Vector2();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Pressed = true;
		PointerId = eventData.pointerId;
		PointerOld = eventData.position;
		StartCoroutine(TouchDown());
	}


	public void OnPointerUp(PointerEventData eventData)
	{
		Pressed = false;
		StartCoroutine(TouchUp());
	}

	IEnumerator TouchDown() 
	{
		touchDown = true;
		yield return new WaitForEndOfFrame();
		touchDown = false;
	}
	IEnumerator TouchUp()
	{
		touchUp = true;
		yield return new WaitForEndOfFrame();
		touchUp = false;
	}
}
