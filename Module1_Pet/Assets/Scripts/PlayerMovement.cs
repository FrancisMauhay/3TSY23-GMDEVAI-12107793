using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;

    InputAction move;

    [SerializeField] float PlayerMovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 direction = move.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x,0,direction.y) * PlayerMovementSpeed * Time.deltaTime;
    }
}
