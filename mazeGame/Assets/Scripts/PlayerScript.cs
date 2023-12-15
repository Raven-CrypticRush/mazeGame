using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    public bool end1 = false;
    public bool end2 = false;
    public bool end3 = false;
    


    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        rb = GetComponent<Rigidbody>();
        verticalInput = Input.GetAxis("Vertical");
        gameManager = GameObject.Find("GamManager").GetComponent<GameManager>();
    }

        // Update is called once per frame
        void Update()
    {
        //movement
        if (isAbleToMove == true)
        {
            if (Input.GetButton("forward"))
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                animator.SetInteger("WalkForward", 1);
            }

            else if (Input.GetButton("backward"))
            {
                transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
                animator.SetInteger("WalkForward", -1);
            }
            else
            {
                animator.SetInteger("WalkForward", 0);
            }

        //Clockwise and counterclockwise Rotation
            if (Input.GetButton("right"))
            {
                animator.SetInteger("Turning", 1);
            }
            else if (Input.GetButton("left"))
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
        //restart button
        if (Input.GetButton("restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
   

    //GameOver sequence

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("End1"))
        {
            end1 = true;
            end2 = false;
            end3 = false;
        }
        if (collision.gameObject.CompareTag("End2"))
        {
            end2 = true;
            end1 = false;
            end3 = false;
        }
        if (collision.gameObject.CompareTag("End3"))
        {
            end3= true;
            end1 = false;
            end2 = false;
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

}
