using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{ 
    public Text m_TypingText; 
    public string m_Message;     
    public float m_Speed = 0.2f; 

    // Start is called before the first frame update 
    void Start() 
    { 
        m_Message = @" AR 기술을 기반으로 한 'Libroom'!
나만의 방식으로 재미있고 활기찬 방을 만들어보아요
더 많은 보상이 기다리고 있습니다."; 

        StartCoroutine(Typing(m_TypingText, m_Message, m_Speed)); 
    } 

    IEnumerator Typing(Text typingText, string message, float speed) 
    { 
        for (int i = 0; i < message.Length; i++) 
        { 
            typingText.text = message.Substring(0, i + 1); 
            yield return new WaitForSeconds(speed); 
        } 
    } 
}