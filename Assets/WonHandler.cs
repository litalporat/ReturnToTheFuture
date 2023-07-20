using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonHandler : MonoBehaviour
{
    [SerializeField] Canvas wonCanvas;
    // Start is called before the first frame update
    void Start()
    {
        wonCanvas.enabled = false;
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            wonCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
