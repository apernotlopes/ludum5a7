using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRender : MonoBehaviour 
{
    public Material PostProcessMaterial;

    public Camera BackgroundCamera;
    public Camera MainCamera;

    private RenderTexture mainRenderTexture;

    void Start()
    {
        mainRenderTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
        mainRenderTexture.Create();

        BackgroundCamera.targetTexture = mainRenderTexture;
        MainCamera.targetTexture = mainRenderTexture;
    }

    void OnPostRender()
    {
        Graphics.Blit(mainRenderTexture, PostProcessMaterial);
    }
}
