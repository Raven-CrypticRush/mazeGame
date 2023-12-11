using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //VARIABLES

    [Header("Movement")]
    public float moveSpeed;
    public float turnSpeed;
    public float jumpForce;
    public bool isOnGround = true;
    private float verticalInput;
    private float horizontalInput;
    private Rigidbody rb;
    public GameObject player;
    

    [Header("Animations")]
    public Animator animator;
    public AudioSource audioSource;
    public bool isDead;
    public int walkForward;




    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        rb = GetComponent<Rigidbody>();
        verticalInput = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        //Forward and Backward Movement


        
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            animator.SetInteger("WalkForward", 1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            animator.SetInteger("WalkForward", -1);
        }

        //Clockwise and counterclockwise Rotation
        if(Input.GetKey(KeyCode.D))
        {
            animator.SetInteger("Turning", 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetInteger("Turning", -1);
        }

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
        
            if (isDead == true)
            {
                animator.SetBool("Died", true);
            }
        }
    }

    }
