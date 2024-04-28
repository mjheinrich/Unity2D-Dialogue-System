using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask solidObjects;

    private Vector2 moveDirection;

    public bool canMove;

    //private NpcController dc;

    private void Start()
    {
        canMove = true;
        //dc = FindObjectOfType<NpcController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!dc.dialoguePanel.activeInHierarchy)
        {
            canMove = true;
        }

        else
        {
            canMove = false;
        }*/

        ProcessInputs();
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
