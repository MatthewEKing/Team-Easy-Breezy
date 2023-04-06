using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Player Variables")]
    [SerializeField] float speed;
    [SerializeField] float walkSpeed;
    public bool canMove = true;

    [Header("Building")]
    public Transform buildPoint;
    [SerializeField] LayerMask groundLayers;

    private Vector2 moveVector;

    enum PlayerState
    {
        Walking,
        Building
    }

    [SerializeField] PlayerState state = PlayerState.Walking;


    private void Awake()
    {
        instance = this;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!canMove) { return; }

        moveVector = context.ReadValue<Vector2>();
    }

    /*public void OnJump(InputAction.CallbackContext context)
    {
        return;
        if (context.started)
        {
            if (CheckIfGrounded())
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            Debug.DrawRay(transform.position, Vector3.down, Color.red, 5f);
        }
    }*/

    public void BuildTurret(InputAction.CallbackContext context)
    {
        if (context.started && state != PlayerState.Building && canMove)
        {
            if (CheckIfBuildable() && TurretSelect.instance.selectedTurret != null && GameManager.instance.totalScrap >= TurretSelect.instance.selectedTurret.scrapCost)
            {
                StartCoroutine(BuildTurretCoroutine());
            }
            else
            {
                Debug.Log("Invalid Building Spot");
            }
        }
    }

    void Update()
    {
        MovePlayer();

        switch (state)
        {
            case PlayerState.Walking:
                speed = walkSpeed;
                break;

            case PlayerState.Building:
                speed = 0f;
                break;
        }

        /*if (!isGrounded)
        {
            rb.velocity += Vector3.down * gravityForce;
        }*/
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(moveVector.x, 0, moveVector.y);

        if (state == PlayerState.Walking)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), .25f);
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }


    bool CheckIfBuildable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(buildPoint.position, 1f, groundLayers);
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.tag != "Buildable" || collider.gameObject.tag == null)
            {
                //Debug.Log("Not Buildable");
                return false;
            }
        }

        //Debug.Log("Is Buildable");
        return true;
    }

    IEnumerator BuildTurretCoroutine()
    {
        state = PlayerState.Building;
        Debug.Log("Building Turret...");
        yield return new WaitForSeconds(3f);
        Instantiate(TurretSelect.instance.selectedTurret.prefab, buildPoint.position, Quaternion.identity);
        GameManager.instance.RemoveScrap(TurretSelect.instance.selectedTurret.scrapCost);
        state = PlayerState.Walking;
        Debug.Log("Built!");
    }

    /*bool CheckIfGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.2f, isGround))
        {
            Debug.Log("Hit Ground");
            return true;
        }
        else
        {
            return false;
        }
    }*/
}