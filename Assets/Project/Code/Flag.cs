using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public GameObject circleObject; // Asignar el círculo desde el inspector
    public GameObject player; // Asignar desde el inspector
    public float delayToActivate = 0.5f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public bool win = false;

    private OrbitCharacter character;
    private bool colliderActivated = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        character = player.GetComponent<OrbitCharacter>();

        // Obtener datos del círculo
        CircleCollider2D circleCollider = circleObject.GetComponent<CircleCollider2D>();
        Vector3 center = circleObject.transform.position;
        float circleRadius = circleCollider.radius * circleObject.transform.lossyScale.x;

        // Obtener el offset vertical del personaje (desde su centro hasta sus pies)
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        float characterBottomOffset = playerCollider.bounds.extents.y;

        // Calcular dirección hacia arriba
        Vector3 direction = Vector3.up;

        // Posicionar justo en el borde superior del círculo, encima del personaje
        transform.position = center + direction * circleRadius;

        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    void Update()
    {
        if (!colliderActivated && character.startMove)
        {
            colliderActivated = true;
            StartCoroutine(ActivateColliderAfterDelay());
        }

        if (win)
        {
            ActivateFlag();
        }
    }

    IEnumerator ActivateColliderAfterDelay()
    {
        yield return new WaitForSeconds(delayToActivate);
        boxCollider.enabled = true;
    }

    void ActivateFlag()
    {
        spriteRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (win) return;

        if (collision is BoxCollider2D)
        {
            win = true;
            character.isMoving = false;

            Debug.Log("¡Nivel ganado!");
        }
    }
}
