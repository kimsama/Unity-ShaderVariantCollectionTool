///////////////////////////////////////////////////////////////////////////////
///
/// ShaderVariantTool.cs
/// 
/// (c)2016 Kim, Hyoun Woo
///
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.IO;

namespace ShaderVariant.Tool
{
    /// <summary>
    /// Collect shader variants from shader files which are under the given path 
    /// and create .shadervariants asset file.
    /// </summary>
    public class ShaderVariantTool : ScriptableObject
    {
        // folder where shader files are.
        public string[] folders;

        // shader keyword
        public string[] keywords;

        // rendering pass type.
        [HideInInspector]
        public PassType passType = PassType.ForwardAdd;

        // output path where the created .shadervariants asset file will be put.
        [HideInInspector]
        public string outputPath = "Assets/";
        
        private readonly string assetFileExt = ".shadervariants";
        [HideInInspector]
        public string assetFileName = "NewShaderVariants";

        /// <summary>
        /// Collect shader variants from shader files which are under the given path.
        /// </summary>
        public void CollectShaderVariants()
        {
            if (folders.Length <= 0)
            {
                string msg = "Empty folders.\nSpecify folder where shader files are.\nNote the path should start with 'Assets/'.";
                EditorUtility.DisplayDialog("Error", msg, "OK");
                return;
            }

            if (keywords.Length <= 0)
            {
                string msg = "Empty keywords.\nSet the necessary 'keywords'.";
                EditorUtility.DisplayDialog("Error", msg,"OK");
                return;
            }

            var collection = new ShaderVariantCollection();
            
            var shaders = AssetDatabase.FindAssets("t:Shader", folders);
            foreach (var guid in shaders)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var shader = AssetDatabase.LoadAssetAtPath<Shader>(path);
                var variant = new ShaderVariantCollection.ShaderVariant(shader, passType, keywords);
                collection.Add(variant);
            }
            
            // save as asset.
            string assetPath = Path.Combine(outputPath, assetFileName + assetFileExt);
            AssetDatabase.CreateAsset(collection, assetPath);
        }

        /// <summary>
        /// Retrievs current path of this project.
        /// Unity editor expects the current folder to be set to the project folder at all times.
        /// </summary>
        /// <returns></returns>
        public static string GetProjectPath()
        {
            string projectPath = System.IO.Directory.GetCurrentDirectory();
            projectPath = projectPath.Replace('\\', '/');
            projectPath += "/"; // last trail
            return projectPath;
        }

#if UNITY_EDITOR
        public static void CreateSetting()
        {
            string path = "Assets/ShaderVariantTool/Editor/";
            string file = "ShaderVariantTool.asset";
            string filePath = path + file;

            ShaderVariantTool instance = ScriptableObject.CreateInstance<ShaderVariantTool>();
            AssetDatabase.CreateAsset(instance, filePath);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}