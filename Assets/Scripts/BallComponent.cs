using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum BallInstruction
// {
//     Idle = 0,
//     MoveUp,
//     MoveDown,
//     MoveLeft,
//     MoveRight,
//     RotateLeft,
//     RotateRight,
//     ScaleUp,
//     ScaleDown
// }
public class BallComponent : MonoBehaviour
{
    Rigidbody2D m_rigidbody;
    // public float PositionSpeed = 1.0f;
    // public float RotationSpeed = 20.0f;
    // private int currentInstruction = 0;
    // private float instructionLenght = 1.0f;
    // private float instructionRotate = 90.0f;
    // private float instructionScale = 2.0f;
    // private Vector3 vecRotation = Vector3.zero;
    // private Vector3 startPosition;
    // private Quaternion startRotation;
    // private Vector3 startScale;
    // private bool startPositionChecked = false;
    // public List<BallInstruction> Instructions = new List<BallInstruction>();

    // private void Update()
    // {
    //     if(currentInstruction < Instructions.Count)
    //     {
    //         float RealPositionSpeed = PositionSpeed * Time.deltaTime;
    //         float RealRotationSpeed = RotationSpeed * Time.deltaTime;
    //         if (startPositionChecked == false)
    //         {
    //             startPosition = transform.position;
    //             startRotation = transform.rotation;
    //             startScale = transform.localScale;
    //             startPositionChecked = true;
    //         }

    //         switch (Instructions[currentInstruction])
    //         {
    //             case BallInstruction.MoveUp:
    //                 transform.position += Vector3.up * RealPositionSpeed;
    //                 break;
                
    //             case BallInstruction.MoveDown:
    //                 transform.position += Vector3.down * RealPositionSpeed;
    //                 break;
                
    //             case BallInstruction.MoveLeft:
    //                 transform.position += Vector3.left * RealPositionSpeed;
    //                 break;
                
    //             case BallInstruction.MoveRight:
    //                 transform.position += Vector3.right * RealPositionSpeed;
    //                 break;

    //             case BallInstruction.RotateLeft:
    //                 vecRotation += Vector3.forward * RealRotationSpeed;
    //                 transform.rotation = Quaternion.Euler(vecRotation);
    //                 break;

    //             case BallInstruction.RotateRight:
    //                 vecRotation += Vector3.back * RealRotationSpeed;
    //                 transform.rotation = Quaternion.Euler(vecRotation);
    //                 break;
                
    //             case BallInstruction.ScaleUp:
    //                 transform.localScale += Vector3.one * RealPositionSpeed;
    //                 break;
                
    //             case BallInstruction.ScaleDown:
    //                 transform.localScale -= Vector3.one * RealPositionSpeed;
    //                 break;

    //             default:
    //                 Debug.Log("Idle");
    //                 break;
    //         }    

    //         if (Mathf.Abs(transform.position.x - startPosition.x) >= instructionLenght || Mathf.Abs(transform.position.y - startPosition.y) >= instructionLenght)
    //         {
    //             startPositionChecked = false;
    //             ++currentInstruction;
    //         }

    //         if (Mathf.Abs(transform.rotation.eulerAngles.z - startRotation.eulerAngles.z) >= instructionRotate && Mathf.Abs(transform.rotation.eulerAngles.z - startRotation.eulerAngles.z) <= 270)
    //         {
    //             startPositionChecked = false;
    //             ++currentInstruction;
    //         }

    //         if (Mathf.Abs(transform.localScale.x - startScale.x) >= instructionScale)
    //         {
    //             startPositionChecked = false;
    //             ++currentInstruction;
    //         }
    //     }
    // }
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
