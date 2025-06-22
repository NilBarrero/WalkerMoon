using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public bool JumpPressed { get; private set; }

    void Update()
    {
        JumpPressed = DetectInput();
    }

    bool DetectInput()
    {
        if (Application.isMobilePlatform)
        {
            // Toca cualquier parte de la pantalla
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
        else
        {
            // Espacio o clic izquierdo
            return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
        }
    }
}
