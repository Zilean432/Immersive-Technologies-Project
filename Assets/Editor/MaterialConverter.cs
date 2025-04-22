using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialConverter : MonoBehaviour
{
    [MenuItem("Tools/Convert Materials to URP")]
    static void ConvertMaterials()
    {
        string[] materialGuids = AssetDatabase.FindAssets("t:Material"); // Finds all materials in the project
        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (material != null)
            {
                // Switch to URP shader if it's not already
                if (material.shader.name != "Universal Render Pipeline/Lit")
                {
                    material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    // Re-assign textures to the new shader if necessary
                    AssignTextures(material);
                    EditorUtility.SetDirty(material); // Mark as dirty to save changes
                }
            }
        }

        AssetDatabase.SaveAssets(); // Save all the changes
        Debug.Log("Materials converted to URP!");
    }

    // Optional: You can add more texture reassign logic here
    static void AssignTextures(Material material)
    {
        // Just as an example, reassign Albedo texture (you can expand this to other maps too)
        if (material.HasProperty("_BaseMap"))
        {
            Texture texture = material.GetTexture("_BaseMap");
            if (texture != null)
            {
                material.SetTexture("_BaseMap", texture);
            }
        }
    }
}
