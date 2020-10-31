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
    private Animator ch_animator;

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
    private bool nearWall;

    [SerializeField]
    public bool hasItem;

    //переносимый предмет
    private GameObject currentItem;
    public int itemCode;

    //ссылки на другие объекты
    POI_Object poiObject;
    public AudioSource StepAS;
    void Start()
    {
        ch_rigidBody = GetComponent<Rigidbody>();
        ch_transform = GetComponent<Transform>();
        ch_animator = GetComponent<Animator>();
        nearLadder = false;
        isGrounded = true;
        nearPOI = false;
        hasItem = false;
        nearWall = false;
    }

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space) && !nearLadder && isGrounded && !PauseScript.pause) {
			ch_rigidBody.AddForce(ch_transform.up * jumpForce);
		}
		if (nearPOI && Input.GetKeyDown(KeyCode.E)) {
			poiObject.Interacting();
		}

        if (ch_animator.GetBool("run") && !StepAS.isPlaying)
        {
            StepAS.Play();
        }
        else if(!ch_animator.GetBool("run"))
        {
            StepAS.Stop();
        }
    }

    void FixedUpdate()
    {
        moveVectorHorizontal = transform.forward * Mathf.Abs(Input.GetAxis("Horizontal")) * speedMove * Time.deltaTime;
        if (!nearWall)
        {
            ch_rigidBody.position += moveVectorHorizontal;
        }
        nearWall = false;


        LadderControl();


        if (Input.GetKey(KeyCode.D))
        {
            ch_animator.SetBool("run", true);
            ch_rigidBody.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ch_animator.SetBool("run", true);
            ch_rigidBody.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            ch_animator.SetBool("run", false);
            ch_rigidBody.rotation = Quaternion.Euler(0, 0, 0);
        }

        //ch_animator.SetFloat("xSpeed", Input.GetAxis("Horizontal"));
        //Debug.Log(Input.GetAxis("Horizontal"));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            EnterLadder();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            //nearWall = true;
        }
        if (other.gameObject.CompareTag("POI"))
        {

            poiObject = other.gameObject.GetComponent<POI_Object>();
            nearPOI = true;

            //miniGame.Init();
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            nearWall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            ExitLadder();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (other.gameObject.CompareTag("POI"))
        {
            if (poiObject != null)
            {
                if (!poiObject.ItemUser) poiObject.miniGame.Close();
                //poiObject.Close();
                poiObject = null;
                nearPOI = false;
            }

            //miniGame.Init();
        }
    }

    void LadderControl()
    {
        if (nearLadder)
        {
            moveVectorVertical = ch_transform.up * Input.GetAxis("Vertical") * speedOnLadder * Time.deltaTime;
            if (Input.GetAxis("Vertical") < 0 && isGrounded)
            {
                moveVectorVertical = new Vector3(0, 0, 0);
            }
            ch_rigidBody.position += moveVectorVertical;
        }
    }

    void EnterLadder()
    {
        //Debug.Log("ПРИЛИПЛИ");
        ch_rigidBody.velocity = Vector3.zero;
        nearLadder = true;
        ch_rigidBody.useGravity = false;
    }

    void ExitLadder()
    {
        //Debug.Log("ОТЛИПЛИ");
        nearLadder = false;
        ch_rigidBody.useGravity = true;
    }

    public void GetItem(GameObject item, int code)
    {
        if (currentItem != null) UseItem();

        currentItem = Instantiate(item, ch_transform.position + new Vector3(0, 0.8f, -0.2f), Quaternion.identity, ch_transform);
        itemCode = code;
        hasItem = true;

    }

    public void UseItem()
    {
        Destroy(currentItem.gameObject);
        hasItem = false;
    }
}
