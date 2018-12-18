using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTouch : MonoBehaviour
{
	public int touchFunctionSelector = 1;


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
	}

}
