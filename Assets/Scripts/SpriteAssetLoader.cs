using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SpriteAssetLoader : MonoBehaviour
{
    public string SpriteToLoad;
    private SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void LoadSprite()
    {
        Sprite sprite = AssetBundlesManager.Instance.GetSprite(SpriteToLoad);
        m_spriteRenderer.sprite = sprite; //sprite is loaded after pressing "?" HUD Button
    }
}
