using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public int deathDelay;

    [Header("GameMechanics")]
    public bool isGameActive = true;
    public bool isAbleToMove = true;
    public GameManager gameManager;
    


    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        rb = GetComponent<Rigidbody>();
        verticalInput = Input.GetAxis("Vertical");
        gameManager = GetComponent<GameManager>();
    }

        // Update is called once per frame
        void Update()
    {
        //Forward and Backward Movement


        if (isAbleToMove == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                animator.SetInteger("WalkForward", 1);
            }

            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
                animator.SetInteger("WalkForward", -1);
            }
            else
            {
                animator.SetInteger("WalkForward", 0);
            }

            //Clockwise and counterclockwise Rotation
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetInteger("Turning", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animator.SetInteger("Turning", -1);
            }
            else
            {
                animator.SetInteger("Turning", 0);
            }


            horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
        }
    }
   

    //GameOver sequence

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("End"))
        {
            
        }
        

        if (collision.gameObject.CompareTag("Enemy"))
        {
            isDead = true;

            if (isDead == true)
            {
                animator.SetBool("Died", true);
                isAbleToMove = false;
                isGameActive = false;
                
            }
        }
    }

    IEnumerator Level2(int levelDelay)
    {
        yield return new WaitForSeconds(levelDelay);
        
    }
}
