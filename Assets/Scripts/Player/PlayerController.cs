using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 horizontalMove = new Vector2(horizontal, 0);
        float vertical = Input.GetAxis("Vertical");
        Vector2 verticalMove = new Vector2(0, vertical);
        transform.position += (Vector3)((horizontalMove + verticalMove) * speed * Time.deltaTime);
    }
}
