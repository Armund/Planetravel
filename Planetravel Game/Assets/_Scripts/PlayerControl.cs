﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	// commit minor
	// commit minor 2
	//основные параметры
	public float speedMove = 5;
	public float speedOnLadder = 3;
	public float jumpForce = 50;

	//перезаписываемые объекты
	private Vector3 moveVectorHorizontal;
	private Vector3 moveVectorVertical;

	//ссылки на компоненты
	private Rigidbody ch_rigidBody;
	private Transform ch_transform;

	//стабилизационные объекты
	//Quaternion rotation = Quaternion.Euler(0, 0, 0);
	//Vector3 zPosition = new Vector3(1, 1, 0);

	//состояния
    [SerializeField]
	private bool nearLadder;
    [SerializeField]
	private bool isGrounded;
    [SerializeField]
	private bool nearPOI;

    [SerializeField]
	public bool hasItem;
    
	//переносимый предмет
	private GameObject currentItem;
	public int itemCode;

	//ссылки на другие объекты
	POI_Object poiObject;
	
	void Start()
    {
		ch_rigidBody = GetComponent<Rigidbody>();
		ch_transform = GetComponent<Transform>();
		nearLadder = false;
		isGrounded = true;
		nearPOI = false;
		hasItem = false;
    }
	
    void Update()
    {
		moveVectorHorizontal = -transform.right * Input.GetAxis("Horizontal") * speedMove * Time.deltaTime;
		ch_rigidBody.position += moveVectorHorizontal;

		if (Input.GetKeyDown(KeyCode.Space) && !nearLadder && isGrounded) {
			ch_rigidBody.AddForce(ch_transform.up * jumpForce);
		}
		LadderControl();

		if (nearPOI && Input.GetKeyDown(KeyCode.E)) {
				poiObject.Interacting();
		}
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Ladder")) {
			EnterLadder();
		}
		if (other.gameObject.CompareTag("Ground")) {
			isGrounded = true;
		}
		if (other.gameObject.CompareTag("POI")) {
			if (poiObject == null) {
				poiObject = other.gameObject.GetComponent<POI_Object>();
				nearPOI = true;
			}
			//miniGame.Init();
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Ladder")) {
			ExitLadder();
		}
		if (other.gameObject.CompareTag("Ground")) {
			isGrounded = false;
		}
		if (other.gameObject.CompareTag("POI")) {
			if (poiObject != null) {
				if(!poiObject.ItemUser) poiObject.miniGame.Close();
				//poiObject.Close();
				poiObject = null;
				nearPOI = false;
			}

			//miniGame.Init();
		}
	}

	void LadderControl() {
		if (nearLadder) {
			moveVectorVertical = ch_transform.up * Input.GetAxis("Vertical") * speedOnLadder * Time.deltaTime;
			ch_rigidBody.position += moveVectorVertical;
		}
	}

	void EnterLadder() {
		//Debug.Log("ПРИЛИПЛИ");
		ch_rigidBody.velocity = Vector3.zero;
		nearLadder = true;
		ch_rigidBody.useGravity = false;
	}

	void ExitLadder() {
		//Debug.Log("ОТЛИПЛИ");
		nearLadder = false;
		ch_rigidBody.useGravity = true;
	}

	public void GetItem(GameObject item, int code)
    {
        if (currentItem != null) UseItem();
        
            currentItem = Instantiate(item, ch_transform.position + new Vector3(+0.2f, 0.6f, 0), Quaternion.identity, ch_transform);
            itemCode = code;
            hasItem = true;
     
	}

	public void UseItem()
    {
		Destroy(currentItem.gameObject);
		hasItem = false;
	}
}
