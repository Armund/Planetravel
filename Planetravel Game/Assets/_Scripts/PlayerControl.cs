using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
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
	private bool nearLadder;
	private bool isGrounded;

	//ссылки на другие объекты
	MiniGame miniGame;
	
	void Start()
    {
		ch_rigidBody = GetComponent<Rigidbody>();
		ch_transform = GetComponent<Transform>();
		nearLadder = false;
		isGrounded = true;
    }
	
    void Update()
    {
		moveVectorHorizontal = -transform.right * Input.GetAxis("Horizontal") * speedMove * Time.deltaTime;
		ch_rigidBody.position += moveVectorHorizontal;

		if (Input.GetKeyDown(KeyCode.Space) && !nearLadder && isGrounded) {
			ch_rigidBody.AddForce(ch_transform.up * jumpForce);
		}
		
		LadderControl();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Ladder")) {
			EnterLadder();
		}
		if (other.gameObject.CompareTag("Ground")) {
			isGrounded = true;
		}
		if (other.gameObject.CompareTag("MiniGame")) {
			Debug.Log("MG");
			miniGame = other.gameObject.GetComponent<MiniGame>();
			Debug.Log(miniGame.GetType());
			miniGame.Init();
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Ladder")) {
			ExitLadder();
		}
		if (other.gameObject.CompareTag("Ground")) {
			isGrounded = false;
		}
	}

	void LadderControl() {
		if (nearLadder) {
			moveVectorVertical = ch_transform.up * Input.GetAxis("Vertical") * speedMove * Time.deltaTime;
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
}
