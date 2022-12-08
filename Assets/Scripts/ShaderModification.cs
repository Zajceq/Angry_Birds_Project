using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderModification : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float horizontalOffset;
    //public float verticalOffset;
    //public Color color;
    //public Texture2D texture2D;
    //public float moveSpeed;
    //public float blendSpeed;
    //public float speedTimeCounter;
    //public float blendTimeCounter;

    private void Start()
    {
        //meshRenderer.material.SetTexture("_MainTex", texture2D);
        //speedTimeCounter = 0.0f;
        //blendTimeCounter = 0.0f;
    }
    void Update()
    {
        meshRenderer.material.SetFloat("_HorizontalOffset", horizontalOffset);
        //meshRenderer.material.SetColor("_Color", color);
        //moveSpeed = meshRenderer.material.GetFloat("_MoveSpeed");
        //blendSpeed = meshRenderer.material.GetFloat("_BlendSpeed");
        //speedTimeCounter += Time.deltaTime * moveSpeed;
        //blendTimeCounter += Time.deltaTime * blendSpeed;
        //meshRenderer.material.SetFloat("_VerticalOffset", Mathf.Sin(speedTimeCounter));
        //meshRenderer.material.SetFloat("_BlendValue", Mathf.Abs(Mathf.Sin(blendTimeCounter)));
    }
}
