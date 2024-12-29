using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeE : MonoBehaviour
{
    private bool clickgo = false;
    private bool clickokay = true; 
    private float shaketime;

    public Image FirImage; //기존이미지
    public Sprite shake1; //흔들이미지
    public Sprite shake2;
    public Sprite shake3;
    public Sprite shake4;
    public Sprite shake5;
    public Sprite shake6;
    public Sprite shake7;
    public Sprite shake8;
    public Sprite shake9;
    public Sprite shake10;
    public Sprite shake11;
    public Sprite shake12;
    public Sprite shake13;
    public Sprite shake14;
    public Sprite shake15;

    public Sprite NoneSprite; // 열매 없 이미지
    public Sprite grow1Sprite; //열매자람이미지1
    public Sprite grow2Sprite; //열매자람이미지2
    public Sprite grownSprite; //열매다 참 이미지

    public GameObject coinText; // 코인
    public bool onefall= true;
    
    void Start(){
        coinText = GameObject.Find("coinE");
    }

    public void shake(){
        if (clickokay){
            //시간 카운트 시작
            shaketime = 0;
            clickgo = true;

            //클릭 제한 걸기
            clickokay = false;
        }
    }

    void Update()
    {
        if (clickgo == true){
            shaketime += Time.deltaTime; //시간 업데이트
            //열매 떨어지는 애니
            if (shaketime >= 0 && shaketime < 0.1f){
                FirImage.sprite = shake1;
            }else if ( shaketime >= 0.1f && shaketime < 0.2f){
                FirImage.sprite = shake2;
            }else if ( shaketime >= 0.2f && shaketime < 0.3f){
                FirImage.sprite = shake3;
            }else if ( shaketime >= 0.3f && shaketime < 0.4f){
                FirImage.sprite = shake4;
            }else if ( shaketime >= 0.4f && shaketime < 0.5f){
                FirImage.sprite = shake5;
            }else if ( shaketime >= 0.5f && shaketime < 0.6f){
                FirImage.sprite = shake6;
            }else if ( shaketime >= 0.6f && shaketime < 0.7f){
                FirImage.sprite = shake7;
            }else if ( shaketime >= 0.7f && shaketime < 0.8f){
                FirImage.sprite = shake8;
            }else if ( shaketime >= 0.8f && shaketime < 0.9f){
                FirImage.sprite = shake9;
            }else if ( shaketime >= 0.9f && shaketime < 1f){
                FirImage.sprite = shake10;
            }else if ( shaketime >= 1f && shaketime < 1.1f){
                FirImage.sprite = shake11;
            }else if ( shaketime >= 1.1f && shaketime < 1.2f){
                FirImage.sprite = shake12;
            }else if ( shaketime >= 1.2f && shaketime < 1.3f){
                FirImage.sprite = shake13;
            }else if ( shaketime >= 1.3f && shaketime < 1.4f){
                FirImage.sprite = shake14;
            }else if ( shaketime >= 1.4f && shaketime < 1.5f){
                FirImage.sprite = shake15;
            }
            else if ( shaketime >= 1.8f && shaketime < 5){  // 열매 없
                FirImage.sprite = NoneSprite;
                if(onefall){
                    //점수 오르기
                    coinText.GetComponent<coinE>().coinUp();
                    onefall = false;
                }
            }else if ( shaketime >= 5 && shaketime < 13 ){ // 열매 자람 1
                FirImage.sprite = grow1Sprite;
            }else if ( shaketime >= 13 && shaketime < 20 ){ // 열매 자람 1
                FirImage.sprite = grow2Sprite;
            }else if( shaketime >= 20 ){// 열매 자람 3(원래)
                FirImage.sprite = grownSprite;

                //클릭 풀기!
                clickokay = true;
            }else{
                //pass
            }
        }
    }
}
