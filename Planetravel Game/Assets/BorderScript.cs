using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			if (other.gameObject.transform.position.x > transform.position.x) {
				other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(500,10,0));

			} else {
				other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 1, 0) * 15);
			}
		}
	}
}
