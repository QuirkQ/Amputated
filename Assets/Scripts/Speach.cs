using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speach : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;
    public GameObject obj;

    void Start()
    {
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            textDisplay.text = "";
            char[] letters = sentences[i].ToCharArray();
            for (int c = 0; c < letters.Length; c++)
            {
                textDisplay.text += letters[c];
                if (c == letters.Length - 1)
                {
                    yield return new WaitForSeconds(typingSpeed + 1.5f);
                }
                else
                {
                    yield return new WaitForSeconds(typingSpeed);
                }
            }
        }
  
        textDisplay.text = "";
        Destroy(obj);
    }
}
