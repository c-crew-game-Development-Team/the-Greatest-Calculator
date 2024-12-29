using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class rankinganiDia : MonoBehaviour
{
    AudioSource audioSource;/////////////소리
    public GameObject backImage;
    public Dialogue info;
    public GameObject textBox;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtSentences;
    bool num = true;

     public void Start() //게임 시작 초기화
    {
        audioSource = GetComponent<AudioSource>();
        var system = FindObjectOfType<rankinganiDia>();
        system.Begin(info);
    } 

    Queue<string>sentences = new Queue<string>();
    public void Begin(Dialogue info)
    {
        sentences.Clear();

        foreach(var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }

        Next();
    }

    public void Next()
    {
        if(sentences.Count == 0)
        {
            num = false;
            End();
        }
        if (num){
            audioSource.Play();
            txtSentences.text = sentences.Dequeue();
        }
    }
    
    public void End()
    {
         textBox.SetActive(false);
    }

}
