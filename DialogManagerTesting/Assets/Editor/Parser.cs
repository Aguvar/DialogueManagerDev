using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class Parser {

    [MenuItem("Assets/Parse conversation text thingie")]
    public static void Parse()
    {
        //Syntaxis
        //$(ConversationName)$#$(Speaker1)$|$(Speakabletext1)$|(CharDelay)#$(Speaker2)$|$(Speakabletext2)$|(CharDelay)#$(Speaker1)$|$(Speakabletext3)|(CharDelay)...

        string filePath = Path.Combine(Application.dataPath, "Conversations/ConvFile.txt");

        string[] convLines = File.ReadAllLines(filePath);

        foreach (var line in convLines)
        {
            if (line.StartsWith("%") || line.Length <= 1)
            {
                continue;
            }

            string[] parts = line.Split('#');
            string convName = ParseString(parts[0],"$");

            SpeakableText[] speakableTexts = new SpeakableText[parts.Length - 1];

            for (int i = 1; i < parts.Length; i++)
            {
                string filename = "/" + convName + i;
                SpeakableText asset = ScriptableObjectUtility.CreateAsset<SpeakableText>(filename);
                string[] conversation = parts[i].Split('|');
                string parsedSpeaker = ParseString(conversation[0],"$");
                string convText = ParseString(conversation[1], "$");
                float textDelay = float.Parse(conversation[2]);

                asset.text = convText;
                asset.speaker = parsedSpeaker;
                asset.charDelay = textDelay;

                EditorUtility.SetDirty(asset);

                speakableTexts[i - 1] = asset;
            }

            Conversation convAsset = ScriptableObjectUtility.CreateAsset<Conversation>(convName);

            EditorUtility.SetDirty(convAsset);
            convAsset.texts = speakableTexts;

        }

        AssetDatabase.SaveAssets();
    }

    public static string ParseString(string input, string separator) {

        int separatorIndex = input.IndexOf(separator);
        int separatorLastIndex = input.LastIndexOf(separator);

        return input.Substring(separatorIndex + separator.Length, separatorLastIndex-(separatorIndex + separator.Length));

    }
}
