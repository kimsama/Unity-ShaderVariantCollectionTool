///////////////////////////////////////////////////////////////////////////////
///
/// ShaderVariantToolEditor.cs
/// 
/// (c)2016 Kim, Hyoun Woo
///
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using System.Collections;
using System.IO;

namespace ShaderVariant.Tool
{
    [CustomEditor(typeof(ShaderVariantTool))]
    public class ShaderVariantToolEditor : Editor
    {
        ShaderVariantTool tool = null;

        protected SerializedObject targetObject;
        protected SerializedProperty foldersProp;
        protected SerializedProperty keywordsProp;

        void OnEnable()
        {
            tool = target as ShaderVariantTool;

            // serialzied things.
            targetObject = new SerializedObject(tool);
            foldersProp = targetObject.FindProperty("folders");
            keywordsProp = targetObject.FindProperty("keywords");
        }

        public override void OnInspectorGUI()
        {
            if (target == null)
                return;

            // Header Title
            GUIStyle headerStyle = GUITool.GUIHelper.MakeHeader();
            EditorGUILayout.LabelField("ShaderVariant Collection Tool", headerStyle, GUILayout.Height(20));
            EditorGUILayout.Space();

            // Specify the place where all shader files are.
            EditorGUILayout.LabelField("Shader:", EditorStyles.boldLabel);
            GUITool.GUIHelper.DrawSerializedProperty(foldersProp);
            GUITool.GUIHelper.DrawSerializedProperty(keywordsProp);

            EditorGUILayout.Space();

            // Specify rendering pass type.
            EditorGUILayout.LabelField("PassType:", EditorStyles.boldLabel);
            tool.passType = (PassType)EditorGUILayout.EnumPopup(tool.passType, GUILayout.MaxWidth(200));

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Output:", EditorStyles.boldLabel);
            float originalWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 50f;

            // output folder
            using (new EditorGUILayout.HorizontalScope())
            {
                string path = string.Empty;
                if (string.IsNullOrEmpty(tool.outputPath))
                    path = ShaderVariantTool.GetProjectPath();
                else
                    path = tool.outputPath;

                tool.outputPath = EditorGUILayout.TextField("  path: ", tool.outputPath, GUILayout.MinWidth(250));
                if (GUILayout.Button("...", GUILayout.Width(20)))
                {
                    string projectFolder = Path.Combine(ShaderVariantTool.GetProjectPath(), tool.outputPath);
                    path = EditorUtility.OpenFolderPanel("Select folder", projectFolder, "");
                    if (path.Length != 0)
                    {
                        tool.outputPath = path.Replace(ShaderVariantTool.GetProjectPath(), "");
                    }
                }
            }

            // Specify the asset filename which will be created.
            tool.assetFileName = EditorGUILayout.TextField("  file: ", tool.assetFileName, GUILayout.MinWidth(300));
            EditorGUIUtility.labelWidth = originalWidth;
            EditorGUILayout.Space();

            // Collect!
            EditorGUILayout.LabelField("ShaderVariant:", EditorStyles.boldLabel);
            if (GUILayout.Button("Collect"))
            {
                tool.CollectShaderVariants();
            }

            // Save if there are any changes.
            if (GUI.changed)
            {
                if (targetObject.ApplyModifiedProperties())
                {
                    EditorUtility.SetDirty(tool);
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}