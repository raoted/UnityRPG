using UnityEngine;
using System.Collections;

/// <summary>
/// Joystick script
/// 
/// Author: Syberex (syberex@rambler.ru 2012)
/// Version: 1.1
/// </summary>

[RequireComponent(typeof(SphereCollider))]
public class Joystick : MonoBehaviour
{
	/// <summary>
	/// Constraints move Joystick
	/// </summary>
	public enum Constraints
	{
		None,
		Horizontal,
		Vertical
	}

	public Transform center;
	/// <summary>
	/// If true, generate message OnJoystickRotate(float rot).
	/// rot = (left) -Pi .. 0 (forward) .. Pi (right)
	/// </summary>
	public bool isRotation = false;
	public Constraints constraint = Constraints.None;

	float joystickRadius = 0f;
	Plane plane;
	int cntFrame;
	int cntFramePressed = 0;
	bool isPressed = false;

	Vector3 lastPos = Vector3.zero;
	Vector3 prevPos = Vector3.zero;


	void Start()
	{
		// Create the plane to drag along
		plane = new Plane(transform.forward, transform.position);
		joystickRadius =((SphereCollider) GetComponent<Collider>()).radius;
		if(center == null)
		{
			center = transform.Find("Center");
			if(center == null)
				Debug.LogWarning("Center of the joystick is not found.");
		}
	}


	// Update is called once per frame
	void Update()
	{
		cntFrame++;
	}


	void LateUpdate()
	{
		if(isPressed && cntFramePressed < cntFrame)
		{
			SendMessageOnJoystick(prevPos);
			cntFramePressed = cntFrame;
		}
	}


	/// <summary>
	/// Press the joystick
	/// </summary>
	void OnPress(bool pressed)
	{
		isPressed = pressed;
		
		if(center != null)
		{
			if(pressed)
				CalcPosition();
			else
			{
				CalcPosition();
				center.localPosition = Vector3.zero;
			}
		}
	}


	/// <summary>
	/// Drag the center
	/// </summary>
	void OnDrag(Vector2 delta)
	{
		prevPos = delta;
		if(center != null)
			CalcPosition();
	}


	void CalcPosition()
	{
		Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.lastEventPosition);
		float dist = 0f;
		
		if(plane.Raycast(ray, out dist))
		{
			lastPos = ray.GetPoint(dist);
		}
		
		Vector3 newPos1 = transform.InverseTransformPoint(lastPos);
		
		if(newPos1.magnitude > joystickRadius)
		{
			Vector3 normPos1 = newPos1.normalized;
			newPos1 = normPos1 * joystickRadius;
		}
		
		// Apply constraint
		if (constraint == Constraints.Horizontal)
			newPos1.y = 0f;
		else if (constraint == Constraints.Vertical)
			newPos1.x = 0f;
			
		center.localPosition = newPos1;
		
		SendMessageOnJoystick(newPos1);
	}
	
	
	/// <summary>
	/// Joystick event
	/// </summary>
	void SendMessageOnJoystick (Vector3 newPos)
	{
		float x = newPos.x / joystickRadius;
		float y = newPos.y / joystickRadius;
			
		Vector2 delta = new Vector2(x > 1f ? 1f : x, y > 1f ? 1f : y);
		if (!isRotation)
			gameObject.SendMessage("OnJoystick", delta, SendMessageOptions.DontRequireReceiver);
		else
		{
			float rot = Polar(delta.y, delta.x);
			gameObject.SendMessage("OnJoystickRotation", rot, SendMessageOptions.DontRequireReceiver);
		}
		prevPos = newPos;
	}
	
	
	/// <summary>
	/// Converting between Cartesian coordinates and polar,
	/// return angle in rad
	/// </summary>
	float Polar (float x, float y)
	{
		float f = 0f;	// if (x == 0 && y == 0)
		if (x > 0)
			f = Mathf.Atan(y / x);
		else if (x < 0 && y >= 0)
			f = Mathf.Atan(y / x) + Mathf.PI;
		else if (x < 0 && y < 0)
			f = Mathf.Atan(y / x) - Mathf.PI;
		else if (x == 0 && y > 0)
			f = Mathf.PI / 2f;
		else if (x == 0 && y < 0)
			f = -Mathf.PI / 2f;
		
		return f;
	}
}
