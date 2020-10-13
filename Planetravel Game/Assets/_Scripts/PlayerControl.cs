using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	//основные параметры
	public float speedMove = 5;
	public float speedOnLadder = 3;

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
	private bool nearLadder;
	
	void Start()
    {
		ch_rigidBody = GetComponent<Rigidbody>();
		ch_transform = GetComponent<Transform>();
		nearLadder = false;
    }
	
    void Update()
    {
		moveVectorHorizontal = -transform.right * Input.GetAxis("Horizontal") * speedMove * Time.deltaTime;
		ch_rigidBody.position += moveVectorHorizontal;

		
		LadderControl();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Ladder")) {
			EnterLadder();
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Ladder")) {
			ExitLadder();
		}
	}

	void LadderControl() {
		if (nearLadder) {
			moveVectorVertical = ch_transform.up * Input.GetAxis("Vertical") * speedMove * Time.deltaTime;
			ch_rigidBody.position += moveVectorVertical;
		}
	}

	void EnterLadder() {
		Debug.Log("ПРИЛИПЛИ");
		nearLadder = true;
		ch_rigidBody.useGravity = false;
	}

	void ExitLadder() {
		Debug.Log("ОТЛИПЛИ");
		nearLadder = false;
		ch_rigidBody.useGravity = true;
	}
}
