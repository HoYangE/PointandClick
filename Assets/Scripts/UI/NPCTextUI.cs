using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class NPCTextUI : MonoBehaviour
{
    [FormerlySerializedAs("CharacterName")] public TMP_Text characterName;
    [FormerlySerializedAs("ChatText")] public TMP_Text chatText;

    private string _writerText;
    private bool _skip;
    
    IEnumerator NormalChat(string narrator, string narration)
    {
        characterName.text = narrator;
        _writerText = "";
        
        foreach (var t in narration)
        {
            _writerText += t;
            chatText.text = _writerText;
            yield return new WaitForSeconds(0.1f);
            if(_skip)
                break;
        }

        chatText.text = narration;
        _skip = false;
    }

    IEnumerator TextPractice()
    {
        yield return StartCoroutine(NormalChat("char1","sdvb4wgber-9bner98gnwev98rhgnbve9"));
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(NormalChat("char2","23fwnvw9g0934gnw-evn-34g34h"));
    }

    void Start()
    {
        StartCoroutine(TextPractice());
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _skip = true;
    }
}
