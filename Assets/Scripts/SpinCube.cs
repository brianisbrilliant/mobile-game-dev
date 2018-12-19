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
        NewSpin();

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
				newPos.z = 0;
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

	public void NewSpin() { 
		spinDirection = Random.rotation;
		Debug.Log("spinSpeed = " + spinDirection.ToString("0.0"));
		LineColor(spinDirection.x);
	}

	// line color changer.
	void LineColor(float input) {
		Debug.Log("Input = " + input);

		input = Mathf.Abs(input);

		// if(input > 1){ 
		// 	input = Random.value;
		// }
		// else if(input < 0) {
		// 	input = Random.value;
		// }

		TrailRenderer[] lrs = GetComponentsInChildren<TrailRenderer>();
		Debug.Log("there are " + lrs.Length + " children.");
		for(int i = 0; i < lrs.Length; i++) {
			Gradient gradient = new Gradient();
			gradient.SetKeys(
				new GradientColorKey[] { 
							new GradientColorKey(Color.HSVToRGB(input, Random.Range(0.8f, 0.9f), 1), 0.0f),
							new GradientColorKey(Color.HSVToRGB(input, Random.Range(0.8f, 0.9f), 1), 0.35f), 
							new GradientColorKey(Color.HSVToRGB(input, Random.Range(0.8f, 0.9f), 1), 1.0f) },
				new GradientAlphaKey[] { 
							new GradientAlphaKey(Random.Range(0.9f, 1), 0.0f), 
							new GradientAlphaKey(Random.Range(0.5f, 1), 0.8f), 
							new GradientAlphaKey(Random.Range(0.0f, 0.2f), 1.0f) }
			);
			lrs[i].colorGradient = gradient;
			lrs[i].startWidth = Random.Range(0.2f,0.4f);
			lrs[i].endWidth = Random.Range(0.0f,0.1f);
		}
	}
}
