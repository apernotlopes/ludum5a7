// KinoTube - Old TV/video artifacts simulation
// https://github.com/keijiro/KinoTube

using UnityEngine;
using System;
using UnityEngine.Rendering.PostProcessing;

namespace Kino
{
    [ExecuteInEditMode]
    public class Tube : MonoBehaviour
    {
        //#region Editable attributes

        //[SerializeField, Range(0, 1)] float _bleeding = 0.5f;
        //[SerializeField, Range(0, 1)] float _fringing = 0.5f;
        //[SerializeField, Range(0, 1)] float _scanline = 0.5f;

        //#endregion

        //#region Private resources

        //[SerializeField] Shader _shader;
        //Material _material;

        //#endregion

        //#region MonoBehaviour methods

        //void OnDestroy()
        //{
        //    if (_material == null)
        //    {
        //        if (Application.isPlaying)
        //            Destroy(_material);
        //        else
        //            DestroyImmediate(_material);
        //    }
        //}

        //void OnRenderImage(RenderTexture source, RenderTexture dest)
        //{
        //    if (_material == null)
        //    {
        //        _material = new Material(_shader);
        //        _material.hideFlags = HideFlags.DontSave;
        //    }

        //    var bleedWidth = 0.04f * _bleeding;  // width of bleeding
        //    var bleedStep = 2.5f / source.width; // max interval of taps
        //    var bleedTaps = Mathf.CeilToInt(bleedWidth / bleedStep);
        //    var bleedDelta = bleedWidth / bleedTaps;
        //    var fringeWidth = 0.0025f * _fringing; // width of fringing

        //    _material.SetInt("_BleedTaps", bleedTaps);
        //    _material.SetFloat("_BleedDelta", bleedDelta);
        //    _material.SetFloat("_FringeDelta", fringeWidth);
        //    _material.SetFloat("_Scanline", _scanline);

        //    Graphics.Blit(source, dest, _material, 0);
        //}

        //#endregion

        [Serializable]
        [PostProcess(typeof(TubeRenderer), PostProcessEvent.AfterStack, "Custom/Tube")]
        public sealed class TubeSetting : PostProcessEffectSettings
        {
            [SerializeField, Range(0, 0.04f)] public FloatParameter _bleeding = new FloatParameter { value = 0.02f };
            [SerializeField, Range(0, 0.0025f)] public FloatParameter _fringing = new FloatParameter { value = 0.00125f };
            [SerializeField, Range(0, 1)] public FloatParameter _scanline = new FloatParameter { value = 0.5f };
        }

        public sealed class TubeRenderer : PostProcessEffectRenderer<TubeSetting>
        {
            public override void Render(PostProcessRenderContext context)
            {
                //var sheet = context.propertySheets.Get(Shader.Find("Hidden/Kino/Tube"));
                var _material = new Material(Shader.Find("Hidden/Kino/Tube")); ;

                FloatParameter bleedStep = new FloatParameter { value = 2.5f / context.screenWidth };
                IntParameter bleedTaps = new IntParameter { value = Mathf.CeilToInt(settings._bleeding / bleedStep) };
                FloatParameter bleedDelta = new FloatParameter { value = settings._bleeding / bleedTaps };

                _material.SetInt("_BleedTaps", bleedTaps);
                _material.SetFloat("_BleedDelta", bleedDelta);
                _material.SetFloat("_FringeDelta", settings._fringing);
                _material.SetFloat("_Scanline", settings._scanline);

                context.command.Blit(context.source, context.destination, _material, 0);
            }
        }
    }
}
