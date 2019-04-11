using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public static class Texture2DUtils
{
    public static Sprite[] CreateSpriteSheet(this Texture2D texture)
    {
        string assetPath = AssetDatabase.GetAssetPath(texture);
        Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(assetPath).OfType<Sprite>().ToArray();
        return sprites;
    }

    public static Texture2D ToTexture(this Sprite sprite)
    {
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels((int)math.ceil(sprite.textureRect.x), (int)math.ceil(sprite.textureRect.y), (int)math.ceil(sprite.textureRect.width), (int)math.ceil(sprite.textureRect.height));
        Debug.Log(pixels.Length + " " + sprite.textureRect);
        texture.SetPixels(pixels);
        texture.Apply();
        texture.filterMode = sprite.texture.filterMode;
        return texture;
    }
}
