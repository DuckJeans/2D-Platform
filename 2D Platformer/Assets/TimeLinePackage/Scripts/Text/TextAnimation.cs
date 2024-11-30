using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    public TextMeshProUGUI _text;
    public float typeSpeed = 0.05f;

    private string full;

    // Start is called before the first frame update
    void Start()
    {
        //full = _text.text;
        //
        //StartCoroutine(TypeText());
    }

    public void StartTypeAni()
    {
        full = _text.text;

        StartCoroutine(TypeText());
    }

   IEnumerator TypeText()
    {
        _text.text = "";
        foreach (char letter in full.ToCharArray())
        {         
            _text.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
