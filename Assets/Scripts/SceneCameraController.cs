using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCameraController : MonoBehaviour {

    public float sceneWidth = 24;

    private Rect leftEdge;
    private Rect rightEdge;

    private void Awake()
    {
        float size = Screen.height * 0.2f;
        leftEdge = new Rect(0, size, size, Screen.height - size * 2);
        rightEdge = new Rect(Screen.width - size, size, size, Screen.height - size);
    }

    public void Update()
    {
        if (GameController.State != GameState.NavigatingScene)
        {
            return;
        }

        if (leftEdge.Contains(Input.mousePosition) || rightEdge.Contains(Input.mousePosition))
        {
            Vector3 mouse = Input.mousePosition;
            mouse.z = transform.position.z;
            mouse.y = transform.position.y;

            float xFraction = mouse.x / Screen.width;
            mouse.x = Mathf.Clamp(xFraction * sceneWidth, 0, sceneWidth);
            transform.position = Vector3.Lerp(transform.position, mouse, 1f * Time.deltaTime);
        }
    }

}

