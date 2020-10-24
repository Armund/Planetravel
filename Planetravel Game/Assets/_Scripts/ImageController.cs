using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
	//RectTransform planet;
	//[SerializeField]
	public RectTransform space;
	[SerializeField]
	float minScale;
	[SerializeField]
	float maxScale;
	[SerializeField]
	float currentScale;
	[SerializeField]
	public float currentDistance;
	[SerializeField]
	float maxDistance;

	float maxAngle;

	public bool isDirectionToPlanet;

	public GameObject target;

	// Start is called before the first frame update
	void Start()
    {
		//planet = GetComponent<RectTransform>();
		minScale = 0.8f;
		maxScale = 9.0f;
		maxDistance = 5000;
		currentDistance = 5000;
        currentScale = 1f;
		isDirectionToPlanet = true;
		maxAngle = 0.55f;
		//anchor = planet
    }

	// Update is called once per frame
	private void Update() {
		/*
		if (Input.GetKey(KeyCode.B)) {
			currentDistance += 1f;
		} else if ((Input.GetKey(KeyCode.V))) {
			currentDistance -= 1f;
		}
		if (Input.GetKeyDown(KeyCode.C)) {
			isDirectionToPlanet = !isDirectionToPlanet;
		}
		*/
	}

	void FixedUpdate()
    {
		currentScale = maxScale / (currentDistance / maxDistance * (maxScale - minScale));
        if (currentDistance < 0)
        {
            currentScale = maxScale;
        }
        else
        {
            currentScale = Mathf.Clamp(currentScale, minScale, maxScale);
        }
        transform.localScale = new Vector3(currentScale, currentScale, 1);


		if (!isDirectionToPlanet && transform.rotation.z < maxAngle) {
			//Debug.Log(planet.rotation.z);
			transform.RotateAround(target.transform.position, Vector3.forward, 5 * Time.deltaTime);
			space.RotateAround(target.transform.position, Vector3.forward, 5 * Time.deltaTime);
		} else if (isDirectionToPlanet && transform.rotation.z > 0) {
			transform.RotateAround(target.transform.position, Vector3.forward, -10 * Time.deltaTime);
			space.RotateAround(target.transform.position, Vector3.forward, -10 * Time.deltaTime);
		}

		//space.rotation = planet.rotation;
	}
}
