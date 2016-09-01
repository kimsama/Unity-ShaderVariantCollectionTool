using UnityEngine;
using UnityEditor;
using System.Collections;

namespace ShaderVariant.Tool
{
    /// <summary>
    /// Enable to add multiple shader files into .shadervariant asset file.
    /// See: http://qiita.com/vui/items/33673b3187baf08f7ca3
    /// </summary>
    public class AddShaderVariantsWindow : EditorWindow
    {
        private ShaderVariantCollection svc;

        [MenuItem("Tools/ShaderVariant/Open ShaderVariants Window")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(AddShaderVariantsWindow));
        }

        void OnGUI()
        {
            svc = (ShaderVariantCollection)EditorGUILayout.ObjectField("ShaderVariantCollection", svc, typeof(ShaderVariantCollection), false);

            if (svc)
            {
                if (GUILayout.Button("Add Select Shader"))
                {
                    Object[] objs = Selection.objects;
                    foreach (Object obj in objs)
                    {
                        Shader shader = obj as Shader;
                        if (shader == null) { continue; }

                        ShaderVariantCollection.ShaderVariant sv = new ShaderVariantCollection.ShaderVariant();
                        sv.shader = shader;
                        svc.Add(sv);
                    }
                }
            }
        }
    }
}