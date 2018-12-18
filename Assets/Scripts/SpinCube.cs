using UnityEngine;
using UnityEngine.UI;

public class SpinCube : MonoBehaviour
{
    private bool isHalfSpeed = false;

    private Quaternion spinDirection;

    public ParticleSystem touchExplosion;
	public Text debugText;


    private Vector3 position;
    private float width;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        spinDirection = Random.rotation;

        // center of the screen
        width = (float)Screen.width;
        height = (float)Screen.height;
        position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinDirection.eulerAngles * Time.deltaTime);

		if(Input.touchCount > 0) {
			// starting with the first touch
			Touch touch = Input.GetTouch(0);

			if(touch.phase == TouchPhase.Began) {
				// calculate screen position
				Vector3 newPos = Camera.main.ScreenToWorldPoint(touch.position);
				debugText.text = "touchpos = " + newPos.ToString("0.00");
				// create new particle explosion at the position of the touch in screeen space
				ParticleSystem ps =  Instantiate(touchExplosion, newPos, Quaternion.identity);
				GameObject cubeMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cubeMarker.transform.position = newPos;
				cubeMarker.transform.localScale = Vector3.one * 0.5f;
				// destroy the particle system after the duration of the particle system
				Destroy(ps.gameObject, touchExplosion.main.duration);
				Destroy(cubeMarker, 3);
			}
		}

		// Unity's implementation of touch. useful example.
		if(false) {
			// Handle screen touches.
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);

				// Move the cube if the screen has the finger moving.
				if (touch.phase == TouchPhase.Moved)
				{
					Vector2 pos = touch.position;
					pos.x = (pos.x - width) / width;
					pos.y = (pos.y - height) / height;
					position = new Vector3(-pos.x, pos.y, 0.0f);

					// Position the cube.
					transform.position = position;
				}

				if (Input.touchCount == 2)
				{
					touch = Input.GetTouch(1);

					if (touch.phase == TouchPhase.Began)
					{
						// Halve the size of the cube.
						transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
					}

					if (touch.phase == TouchPhase.Ended)
					{
						// Restore the regular size of the cube.
						transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					}
				}
			}
		}

    }

    public void HalfSpeed() {
        if(isHalfSpeed) Time.timeScale = 1;
        else Time.timeScale = 0.5f;
        isHalfSpeed = !isHalfSpeed;
    }

	public void NewSpin() { spinDirection = Random.rotation; }
}
