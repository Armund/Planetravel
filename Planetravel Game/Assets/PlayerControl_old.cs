using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl_old : MonoBehaviour
{
	//основные параметры
	public float speedMove = 5;
	public float speedOnLadder = 3;
	private Vector3 moveVector;

	//ссылки на компоненты
	private Rigidbody ch_rigidBody;
	//private CharacterController ch_controller;
	//private Animator ch_animator;

	//ссылки на другие объекты
	private ladderScript currentLadder;

	//состояния
	private bool nearLadder;
	private bool onLadder;

	// Start is called before the first frame update
	void Start()
    {
		ch_rigidBody = GetComponent<Rigidbody>();
		//ch_controller = GetComponent<CharacterController>();
		//ch_animator = GetComponent<Animator>();
		nearLadder = false;
		onLadder = false;
	}
	
    // Update is called once per frame
    void Update()
    {
		//CharachterMove();

		//передвижение в стороны
		moveVector = -transform.right * Input.GetAxis("Horizontal") * speedMove * Time.deltaTime;
		ch_rigidBody.position += moveVector;

		//передвижение по лестнице
		if (nearLadder) {
			LadderControl(currentLadder.gameObject.transform);
		}
	}

	/*
	private void CharachterMove() {
		moveVector = Vector3.zero;
		moveVector.x = Input.GetAxis("Horizontal") * speedMove;

		ch_controller.Move(moveVector * Time.deltaTime);		
	} */

	private void OnTriggerEnter(Collider other) {
		Debug.Log("ТРИГЕР ЕНТЕР");
		if (other.gameObject.CompareTag("Ladder")) {
			nearLadder = true;
			currentLadder = other.gameObject.GetComponent<ladderScript>();
		}
		if (other.gameObject.CompareTag("LadderTop") && onLadder) {
			LadderExit();
		}
	}

	/*private void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag("Ladder")) {
			LadderControl(other.transform);
		}
	}*/

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Ladder")) {
			Debug.Log("ОТЛИПЛИ");
			currentLadder = null;
			nearLadder = false;
		}
	}

	private void LadderControl(Transform ladder) {
		//прилипание к лестнице
		if (Input.GetKeyDown("w")) {
			if (nearLadder) {
				//currentLadder = ladder.GetComponent<ladderScript>();
				onLadder = true;
				//ch_rigidBody.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
				ch_rigidBody.position = new Vector3(ladder.position.x, ch_rigidBody.position.y, ch_rigidBody.position.z);
				ch_rigidBody.useGravity = false;

				Debug.Log("ПРИЛИПЛИ");
				currentLadder.topFloor.gameObject.SetActive(false);
			}
		}
		
		//движение по лестнице
		if (Input.GetKey("w")) {
			if (onLadder) {
				moveVector = transform.up * speedOnLadder * Time.deltaTime;

				ch_rigidBody.position += moveVector;

				if (transform.position.y > currentLadder.topSpawn.position.y) {
					LadderExit();
				}
			}
		}
	}

	private void LadderExit() {
		Debug.Log("ВЫХОДИМ");
		onLadder = false;
		ch_rigidBody.position = new Vector3(ch_rigidBody.position.x, currentLadder.topSpawn.transform.position.y, ch_rigidBody.position.z);
		ch_rigidBody.useGravity = true;
		currentLadder.topFloor.gameObject.SetActive(true);
		//currentLadder = null;
	}
}
