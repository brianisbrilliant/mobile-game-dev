using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCube : MonoBehaviour
{

    private Quaternion spinDirection;

    // Start is called before the first frame update
    void Start()
    {
        spinDirection = Random.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinDirection.eulerAngles * Time.deltaTime);

        if(Input.anyKeyDown) {
            spinDirection = Random.rotation;
        }
    }
}
