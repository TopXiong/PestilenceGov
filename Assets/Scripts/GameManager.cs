using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Speed;
    public GameObject Healthy;
    public GameObject Opinion;
    public GameObject Dangerous;
    INIParser ini;
    private static GameManager _instance;

    public int index = 1;
    public Vector4 Value { get; private set; }
    public Card card;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                //则创建一个
                _instance = GameObject.Find("background").GetComponent<GameManager>();
            //返回这个实例
            return _instance;
        }
    }

    void Awake()
    {
        if (!Instance)
            _instance = this;
    }

    void Start()
    {
        Value = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
        ini = new INIParser();
        TextAsset textFile = Resources.Load<TextAsset>("event");
        ini.OpenFromString(textFile.text);
        card = new Card(1,ini);
    }

    public void NextCard(bool IsYes)
    {
        if(card == null)
        {
            card = new Card(index,ini);
        }
        if (IsYes)
            Value += card.YESValue;
        else
            Value += card.NOValue;
        Speed.transform.GetChild(2).transform.localScale = new Vector3(1f,Value.x, 1f);
        Healthy.transform.GetChild(2).transform.localScale = new Vector3(1f, Value.y, 1f);
        Opinion.transform.GetChild(2).transform.localScale = new Vector3(1f, Value.z, 1f);
        Dangerous.transform.GetChild(2).transform.localScale = new Vector3(1f, Value.w, 1f);
        if (Value.x==0 || Value.y==0 || Value.z == 0 || Value.w == 0)
        {
            Debug.Log("Dead");
        }
        index++;
        card = new Card(index,ini);
    }

}
