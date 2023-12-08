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
    public GameObject cube;




    [Header("Shooting")]
    public GameObject projectile;
    public float shootDelay;
    public GameObject spawnPoint;
    public bool canShoot = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Forward and Backward Movement

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * moveSpeed * verticalInput * Time.deltaTime);

        //Clockwise and counterclockwise Rotation

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(cube);

        }
    }

    }
