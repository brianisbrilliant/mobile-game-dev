using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTouch : MonoBehaviour
{
	public int touchFunctionSelector = 1;
	[Range(1,5)]
	public float moveSpeed = 1;
	[Range(0.05f,0.5f)]
	public float joystickThreshold = 0.2f;
	public Joystick joystick;


    // Update is called once per frame
    void Update()
    {
		if(touchFunctionSelector == 0) {
			if(Input.touchCount > 0) {
				Touch touch = Input.GetTouch(0);
				Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
				touchPos.z = 0f;
				this.transform.position = touchPos;

			}
		}
		if(touchFunctionSelector == 1) {
			for(int i = 0; i < Input.touchCount; i++) {
				Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
				Debug.DrawLine(Vector3.zero, touchPos, Color.red);
			}
		}
		if(touchFunctionSelector == 2) {
			this.transform.Translate(
					joystick.Horizontal * moveSpeed * Time.deltaTime, 
					joystick.Vertical * moveSpeed * Time.deltaTime, 
					0);
		}
		if(touchFunctionSelector == 3) {
			if(joystick.Horizontal >= joystickThreshold) {
				this.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
			} else if(joystick.Horizontal <= -joystickThreshold) {
				this.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
			}
			if(joystick.Vertical >= joystickThreshold) {
				this.transform.Translate(0, moveSpeed * Time.deltaTime, 0);
			} else if(joystick.Vertical <= -joystickThreshold) {
				this.transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
			}
		}
	}

}
