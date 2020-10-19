using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
	public PlayerControl playerControl;
	public GameObject item;
	// 0 - огнетушитель
	// 1 - батарея
	

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (!playerControl.hasItem && Input.GetKeyDown(KeyCode.F)) {
			playerControl.GetItem(item, 0);
		}
    }

	private void OnTriggerEnter(Collider other) {
	}
}
