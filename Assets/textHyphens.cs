using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textHyphens : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public int maxLineLength = 20;

    void Start()
    {
        string originalText = textMeshPro.text;
        string hyphenatedText = AddHyphens(originalText, maxLineLength);
        textMeshPro.text = hyphenatedText;
    }

    public string AddHyphens(string text, int maxLength)
    {
        string[] words = text.Split(' ');
        string result = "";
        string line = "";

        foreach (string word in words)
        {
            if (line.Length + word.Length > maxLength)
            {
                int splitIndex = maxLength - line.Length;
                result += line + word.Substring(0, splitIndex) + "-\n";
                line = word.Substring(splitIndex);
            }
            else
            {
                line += (line.Length > 0 ? " " : "") + word;
            }
        }

        result += line;
        return result;
    }
}
