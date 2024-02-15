using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float slideSpeed;
    public CapsuleCollider pCol;
    private float gravity = 30f;
    private Animator animator;
    private Rigidbody rb;
    private bool isGrounded;
    public GameManagerScript gameManager;
    private float minXPos = -1.45f;
    private float maxXPos =  1.45f; 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        pCol = GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        playerTransform.Translate(Vector3.forward * Time.deltaTime * verticalSpeed);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float horizontalInput = touch.deltaPosition.x / Screen.width;
            transform.Translate(new Vector3(horizontalInput, 0f, 0f) * horizontalSpeed * Time.deltaTime);
        }

        if (transform.position.x <= minXPos)
        {
            transform.position = new Vector3(minXPos, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= maxXPos)
        {
            transform.position = new Vector3(maxXPos, transform.position.y, transform.position.z);
        }     
    }
    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravity);

        if (rb.velocity.y < 0.1 && !isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        { 
            gameObject.SetActive(false);
            gameManager.gameOverScreen();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            string item = other.gameObject.GetComponent<Collectable>().itemType;
            if (item == "Jump" && isGrounded)
            {
                rb.velocity = new Vector3(0, jumpForce, 0);
                isGrounded = false;
                animator.SetBool("isJumping", true);
                other.gameObject.SetActive(false);


            }
            if (item == "Slide")
            {
                animator.SetBool("isSliding", true);
                pCol.height = 0.36f;
                pCol.center = new Vector3(0, 0.15f, 0);
                transform.Translate(0, 0, slideSpeed);
                StartCoroutine(DelayedSlideAndAnimation());
                other.gameObject.SetActive(false);
            }          
        }
        if (other.CompareTag("Fýnýsh"))
        {     
           animator.SetBool("isFinished", true);
           verticalSpeed = 0;
           horizontalSpeed = 0;
           Debug.Log("Finished");
           gameManager.nextLevelScreen();
        }
      
        IEnumerator DelayedSlideAndAnimation()
        {
            yield return new WaitForSeconds(0.5f);
            pCol.height = 0.78f;
            pCol.center = new Vector3(0,0.35f,0);
            animator.SetBool("isSliding", false);
        }
      }
    }



        
    
        



    


