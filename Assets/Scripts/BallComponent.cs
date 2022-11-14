using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    Rigidbody2D m_rigidbody;
    
    private void Start() 
    {
       m_rigidbody = GetComponent<Rigidbody2D>(); 
    }

    private void Update() 
    {
        if (GameplayManager.Instance.Pause)
        {
            m_rigidbody.simulated = false;
        }
        else if (!GameplayManager.Instance.Pause)
        {
            m_rigidbody.simulated = true;
        }

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button has been pressed");
        }
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse entering over object");
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse leaving object");
    }

    private void OnMouseDrag() 
    {   
        m_rigidbody.simulated = false;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
    }

    private void OnMouseUp() 
    {
        m_rigidbody.simulated = true;
    }
}
