///////////////////////////////////////////////////////////////////////////////
///
/// SettingMenuItems.cs
/// 
/// (c)2016 Kim, Hyoun Woo
///
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace ShaderVariant.Tool
{
    public class ShaderVariantCollectionSettingMenuItems
    {

        [MenuItem("Tools/ShaderVariant/Create ShaderVariantCollection Setting")]
        static public void CreateShaderVariantCollectionSetting()
        {
            ShaderVariantTool.CreateSetting();
        }
    }
}