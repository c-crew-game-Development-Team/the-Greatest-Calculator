//�������� 3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingStage : MonoBehaviour
{
    public GameObject BackgroundMusic;
    AudioSource backmusic;
    AudioSource audioSource;/////////////소리
    public AudioClip stagegogo;
    public AudioClip stageclear;
    public AudioClip sword;
    public AudioClip pauseclick;
    public AudioClip monappear;

    void PlaySound(string action){
        switch (action){
            case "stagegogo":
                audioSource.clip = stagegogo;
                break;
            case "stageclear":
                audioSource.clip = stageclear;
                break;
            case "sword":
                audioSource.clip = sword;
                break;
            case "pauseclick":
                audioSource.clip = pauseclick;
                break;
            case "monappear":
                audioSource.clip = monappear;
                break;
        }
        audioSource.Play();
    }
    public GameObject canvas;
    public GameObject ending;
    public GameObject FightBar;
    public GameObject HpPlayer;
    public GameObject punch;

    public GameObject Pause;
    public GameObject Resume;
    public bool pausemode;

    public int stage; //���� �ܰ� Ȯ�ο� ����
    public bool stagemove; //�ܰ� ���� �̵� �ð� Ȯ���� ����
    public int fortime; //�ð� �帣�� �ϱ� ���� ����

    public int remain; //�ܰ躰 ���� ���� �� Ȯ�ο� ����

    public GameObject monster;
    public GameObject monster2;
    public GameObject monster3;
    public GameObject monster4;
    public int monstermove;
    GameObject mon1;
    GameObject mon2;
    GameObject mon3;
    GameObject mon4;
    float x1 = 20f;
    float x2 = 20f;
    float x3 = 20f;
    float x4 = 20f;
    float y1 = -0.8f;
    float y2 = -1.4f;
    float y3 = -0.6f;
    float y4 = -1.2f;

    //Į ���󰡱�~
    public GameObject AttackBar1;
    GameObject fly;
    float xk = -6.5f;
    float yk = 2.8f;
    public float xf;
    public float yf;
    float speed = 7.5f;
    bool flymode;

    //ŧ
    public GameObject Cul;

    public int monnum2;
    public int numnum;

    public float story;

    public GameObject TimeCount;
    public GameObject TimeBox;

    GameObject ForDestroy;

    private GameObject target; //���콺 Ŭ�� Ȯ�ο� ����
    void CastRay() //���콺 Ŭ�� Ȯ�ο� �Լ�
    {
        target = null;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
        }
    }

    public void Start() //���� ���� �ʱ�ȭ
    {
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();/////////////소리
        FightBar.SetActive(false);

        ResumeMode();

        stage = 2;
        stagemove = false;
        remain = 1;

        monstermove = 0;
        
        monnum2 = 0;

        story = 2;
        
        fly = Instantiate(AttackBar1, new Vector2(xk, yk), transform.rotation);
        fly.SetActive(false);
        flymode = false;
    }

    void FixedUpdate()
    {
        if (flymode == true)
        {
            fly.transform.position = Vector2.Lerp(fly.transform.position, new Vector2(xf, yf), Time.deltaTime * speed);
            PlaySound("sword");/////////////소리
        }
        if (fly.transform.position.x >= xf - 1f && flymode == true)
        {
            Flyoff();
        }
    }

    void Update()
    {
        if (stage == 2 && stagemove == false) //2�ܰ� ����
        {
            // GameObject.Find("Stage").GetComponent<Stage3>().PauseMode();
            GameObject.Find("Stage").GetComponent<RankingStage>().story = 2;

            // canvas.SetActive(true);
            fortime = 1;
            TimeCount.transform.position = new Vector2(-8.5f, TimeCount.transform.position.y);
            TimeBox.transform.position = new Vector2(-8.5f, TimeBox.transform.position.y);

            HpPlayer.SetActive(false);

            punch.GetComponent<PunchScript>().ScrollChange2();
            GameObject.Find("Story").GetComponent<RankingStoryScript>().zadfjladsfzjflkdzjflkajsdflkjasldfjaslfdjl();
            // GameObject.Find("Story").GetComponent<RankingStoryScript>().Story2On();
            Cul.GetComponent<CulScriptRanking>().AttackBarOn();
            punch.GetComponent<PunchScript>().punchmode = 1;
            punch.GetComponent<PunchScript>().PunchMode();
            CulSkill2();
            stagemove = true;
        }

        if (stage == 3 && stagemove == false) //���丮 ���
        {
            fortime = 0;
            TimeCount.transform.position = new Vector2(0, TimeCount.transform.position.y);
            TimeBox.transform.position = new Vector2(0, TimeBox.transform.position.y);
            PlaySound("stageclear");/////////////
            Cul.transform.position = new Vector2(7, Cul.transform.position.y);

            punch.GetComponent<PunchScript>().punchmode = 0;
            punch.GetComponent<PunchScript>().PunchMode();
            punch.GetComponent<PunchScript>().re();
            punch.GetComponent<PunchScript>().ScrollChange3();
            Cul.GetComponent<CulScriptRanking>().AttackBarOff();
            Invoke("Story2_2", 4f);
            Invoke("WinAni", 1f);

            stagemove = true;
        }

        if (stage == 3 && remain == 0) //Ŭ����
        {
            PlaySound("stagegogo");/////////////소리
            StageEnding();

            stagemove = true;
        }
    }

    public void PauseMode()
    {
        Time.timeScale = 0;
        pausemode = true;
        Pause.SetActive(false);
        Resume.SetActive(true);
    }
    public void MusicPauseMode()//음악도 같이
    {
        backmusic.Pause();
        Time.timeScale = 0;
        pausemode = true;
        //GameObject.Find("buttonclick").GetComponent<Buttonclick2>().pausemode = true;
        Pause.SetActive(false);
        Resume.SetActive(true);
    }
    public void ResumeMode()
    {
        Time.timeScale = 1;
        pausemode = false;
        Pause.SetActive(true);
        Resume.SetActive(false);
    }
    public void MusicResumeMode() //음악도 같이
    {
        backmusic.Play();
        Time.timeScale = 1;
        pausemode = false;
        //GameObject.Find("buttonclick").GetComponent<Buttonclick2>().pausemode = false;
        Pause.SetActive(true);
        Resume.SetActive(false);
    }


    public void StageMove() //�ܰ� ���� �̵� �ð� Ȯ���� �Լ�
    {
        stage++;
        stagemove = false;
    }

    public void Fly()
    {
        GameObject.Find("Player").GetComponent<Animator>().SetTrigger("attack");
        GameObject.Find("Player").GetComponent<KalScript>().AttackEffect();
        fly.SetActive(true);
        flymode = true;

        float r1 = Mathf.Atan2(yf - yk, xf - xk) * Mathf.Rad2Deg;
        if (r1 < -30)
        {
            r1 = -30;
        }
        fly.transform.rotation = Quaternion.Euler(0, 0, r1);
    }
    public void Flyoff()
    {
        if (monnum2 == 1)
        {
            mon1.GetComponent<MonsterScript>().OnDamaged();
        }
        else if (monnum2 == 2)
        {
            mon2.GetComponent<MonsterScript>().OnDamaged();
        }
        else if (monnum2 == 4)
        {
            mon3.GetComponent<MonsterScript2>().OnDamaged();
        }
        else if (monnum2 == 5)
        {
            mon4.GetComponent<MonsterScript2>().OnDamaged();
        }
        else if (monnum2 == 6)
        {
            if(numnum == 1)
            {
                GameObject.Find("NumberBundle").GetComponent<RankingNumberBundleScript>().num1setting();
            }
            else if (numnum == 2)
            {
                GameObject.Find("NumberBundle").GetComponent<RankingNumberBundleScript>().num2setting();
            }
            else if (numnum == 3)
            {
                GameObject.Find("NumberBundle").GetComponent<RankingNumberBundleScript>().num3setting();
            }
            else if (numnum == 4)
            {
                GameObject.Find("NumberBundle").GetComponent<RankingNumberBundleScript>().num4setting();
            }
            else if (numnum == 5)
            {
                GameObject.Find("NumberBundle").GetComponent<RankingNumberBundleScript>().num5setting();
            }
        }
        monnum2 = 0;

        fly.transform.position = new Vector2(xk, yk);
        fly.transform.Rotate(0, 0, 0);
        fly.SetActive(false);
        flymode = false;
        punch.GetComponent<PunchScript>().punchmode = 1;
        punch.GetComponent<PunchScript>().PunchMode();
        punch.GetComponent<PunchScript>().ScrollChange2();
    }
    public void realFlyoff()
    {
        fly.SetActive(false);
        flymode = false;
    }

    public void CulSkill2()
    {
        GameObject.Find("NumberBundle").GetComponent<RankingNumberBundleScript>().numbunOn();
        GameObject.Find("Cul").GetComponent<CulScriptRanking>().Timer();
        FightBar.SetActive(true);
        fortime = 1;
    }

    public void Win()
    {
        FightBar.SetActive(false);
        GameObject.Find("NumberBundle").GetComponent<RankingNumberBundleScript>().numbunOff();
        StageMove();

        Rankingapi rankingapi = Rankingapi.Instance;

        rankingapi.UpdateScore("arduinocc04", 100);
    }
    void WinAni()
    {
        Cul.GetComponent<Animator>().SetTrigger("hit");
    }

    public void Lose()
    {
        punch.GetComponent<PunchScript>().punchmode = 0;
        punch.GetComponent<PunchScript>().PunchMode();
        punch.GetComponent<PunchScript>().re();
        punch.GetComponent<PunchScript>().ScrollChange3();
        FightBar.SetActive(false);
        Cul.GetComponent<CulScriptRanking>().AttackBarOff();
        Cul.GetComponent<CulScriptRanking>().move2();
        Invoke("LoseAni", 1f);
    }
    void LoseAni()
    {
        GameObject.Find("Player").GetComponent<Animator>().SetTrigger("hit");
        GameObject.Find("ending").GetComponent<endingscene>().Playerpowerend();
    }
    public void TimeOut()
    {
        punch.GetComponent<PunchScript>().punchmode = 0;
        punch.GetComponent<PunchScript>().PunchMode();
        punch.GetComponent<PunchScript>().re();
        punch.GetComponent<PunchScript>().ScrollChange3();
        FightBar.SetActive(false);
        GameObject.Find("NumberBundle").GetComponent<RankingNumberBundleScript>().numbunOff();
        Cul.GetComponent<CulScriptRanking>().AttackBarOff();
        Invoke("TimeLoseAni", 1f);
    }
    void TimeLoseAni()
    {
        GameObject.Find("Player").GetComponent<Animator>().SetTrigger("hit");
        GameObject.Find("ending").GetComponent<endingscene>().Stagetimeout();
    }

    public void Story2_2()
    {
        GameObject.Find("Story").GetComponent<RankingStoryScript>().Story2_2On();
        Invoke("NextStory", 1f);
    }
    public void NextStory()
    {
        Cul.GetComponent<CulScriptRanking>().KerWongiOn();
    }

    public void StageEnding()
    {
        if (ending != null)
        {
            ending.GetComponent<endingscene>().stage = 3;
            ending.GetComponent<endingscene>().endingStart();
        }
    }
}