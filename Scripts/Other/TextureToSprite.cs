using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class TextureToSprite : MonoBehaviour {
    [Header("Input")]
    public Texture2D inputTexture;
    public RenderTexture inputRenderTexture;
    public Vector2 spritePivot = Vector2.zero;

    [Header("Debug")]
    [SerializeField()]
    private SpriteRenderer spriteRenderer;
    [SerializeField()]
    private Image uiImage;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.uiImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (spriteRenderer || uiImage)
        {
            if (inputTexture)
            {
                this.ConvertToSprite(inputTexture);
            } else if (inputRenderTexture)
            {
                Texture2D convTex = this.RenderToTexture2D(inputRenderTexture);
                this.ConvertToSprite(convTex);
            }
        }
    }

    private void ConvertToSprite(Texture2D input)
    {
        Sprite s = Sprite.Create(input, new Rect(Vector2.zero, new Vector2(input.width, input.height)), spritePivot, 32);
        s.texture.filterMode = FilterMode.Point;

        if (this.spriteRenderer)
        {
            this.spriteRenderer.sprite = s;
        }
        
        if (this.uiImage)
        {
            this.uiImage.sprite = s;
        }
    }

    private Texture2D RenderToTexture2D(RenderTexture rt)
    {
        Texture2D tex = new Texture2D(rt.width, rt.height);
        Graphics.CopyTexture(rt, 0, 0, tex, 0, 0);
        
        return tex;
    }
}
