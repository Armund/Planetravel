using System.Collections;
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
	private bool nearLadder;
	private bool isGrounded;
	private bool nearMiniGame;

	//ссылки на другие объекты
	MiniGame miniGame;
	
	void Start()
    {
		ch_rigidBody = GetComponent<Rigidbody>();
		ch_transform = GetComponent<Transform>();
		nearLadder = false;
		isGrounded = true;
		nearMiniGame = false;
    }
	
    void Update()
    {
		moveVectorHorizontal = -transform.right * Input.GetAxis("Horizontal") * speedMove * Time.deltaTime;
		ch_rigidBody.position += moveVectorHorizontal;

		if (Input.GetKeyDown(KeyCode.Space) && !nearLadder && isGrounded) {
			ch_rigidBody.AddForce(ch_transform.up * jumpForce);
		}
		LadderControl();

		if (nearMiniGame && Input.GetKeyDown(KeyCode.E)) {
				miniGame.Init();
		}		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Ladder")) {
			EnterLadder();
		}
		if (other.gameObject.CompareTag("Ground")) {
			isGrounded = true;
		}
		if (other.gameObject.CompareTag("MiniGame")) {
			if (miniGame == null) {
				miniGame = other.gameObject.GetComponent<MiniGame>();
				nearMiniGame = true;
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
		if (other.gameObject.CompareTag("MiniGame")) {
			if (miniGame != null) {
				miniGame.Close();
				miniGame = null;
				nearMiniGame = false;
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
}
