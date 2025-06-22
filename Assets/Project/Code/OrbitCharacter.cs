using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCharacter : MonoBehaviour
{
    public Transform circleObject; //circulo
    public float Speed = 1f; 
    public float jumpForce = 5f;
    public float gravity = 9.8f;

    private float angle = 0f;
    private float radius = 1f;
    private float verticalOffset = 0f;
    private float verticalVelocity = 0f;
    private bool isJumping = false;

    private CircleCollider2D circleCollider;
    private BoxCollider2D boxCollider;
    private float bottomOffset = 0f; // valor fijo calculado en Start()

    public Jump input;
    public bool isMoving = false;
    public bool startMove = false;

    void Start()
    {
        circleCollider = circleObject.GetComponent<CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (circleCollider != null)
        {
            radius = circleCollider.radius * circleObject.localScale.x; //calculamos el radio del circulo en vase al circleCollider
        }
        else
        {
            Debug.LogWarning("CircleCollider2D no encontrado.");
        }

        // ángulo inicial para que empiece en la parte superior del círculo (90 grados).
        angle = Mathf.PI / 2;

        // ajuste para que los pies del personaje esten en el vorde del circulo
        bottomOffset = boxCollider.bounds.extents.y - 0.05f;
    }

    void Update()
    {
        if(!isMoving && input.JumpPressed)
        {
            isMoving = true;
            startMove = true;
            return;
        }

        if (isMoving)
        {
            angle += Speed * Time.deltaTime;
        }

        Vector2 dirFromCenter = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        if (isJumping)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            verticalOffset += verticalVelocity * Time.deltaTime;

            if (verticalOffset <= 0.01f)
            {
                verticalOffset = 0f;
                verticalVelocity = 0f;
                isJumping = false;
            }
        }

        Vector2 center = circleObject.position;

        Vector2 position = center + dirFromCenter * (radius + verticalOffset + bottomOffset);
        transform.position = position;

        float angleDegrees = angle * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angleDegrees - 90f);

        if (input.JumpPressed && !isJumping && startMove == true)
        {
            isJumping = true;
            verticalVelocity = jumpForce;
        }
    }
}

