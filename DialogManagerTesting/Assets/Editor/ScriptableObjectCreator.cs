using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility
{
    /// <summary>
    //	This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    public static T CreateAsset<T>(string fileName) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = "Assets/ParsedConversations";
        //if (path == "")
        //{
        //    path = "Assets";
        //}
        //else if (Path.GetExtension(path) != "")
        //{
        //    path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        //}

        string assetPathAndName = path + "/" + fileName + ".asset";

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
        //EditorUtility.FocusProjectWindow();

        return asset;
    }
}
