
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveManager : MonoBehaviour
{
    public GameObject target;
    public GameObject mask;
    public GameObject title;
    public GameObject text;
    private float down = 4;
    private float OffsetX = 0;
    private bool IsPress = false;
    private float role = 0f;
    private bool Playing = false;
    private bool Roling = false;
    private TextMesh Ttitle;
    private TextMesh Ttext;
    // Use this for initialization
    void Start()
    {
        Ttitle = title.GetComponent<TextMesh>();
        Ttext = text.GetComponent<TextMesh>();
        Input.multiTouchEnabled = true;
        Input.simulateMouseWithTouches = true;
        Debug.Log("MoveManager Start");
    }

    private void ShowFlag(Vector4 vector4)
    {
        if (vector4.x != 0)
            GameManager.Instance.Speed.transform.Find("change").gameObject.SetActive(true);
        if (vector4.y != 0)
            GameManager.Instance.Healthy.transform.Find("change").gameObject.SetActive(true);
        if (vector4.z != 0)
            GameManager.Instance.Opinion.transform.Find("change").gameObject.SetActive(true);
        if (vector4.w != 0)
            GameManager.Instance.Dangerous.transform.Find("change").gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Playing)
        {
            float speed = 0.5f;
            if (down > 0)
            {
                down -= speed;
                target.transform.position += new Vector3(0f, -speed, 0f);
            }
            else
            {
                Playing = false;
                down = 4;
                Roling = true;
                target.transform.position = new Vector3(0f, -1.7f, 0f);
                target.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                Ttitle.text = GameManager.Instance.card.Name;
            }
        }

        if (Roling)
        {
            target.transform.rotation = Quaternion.Slerp(target.transform.rotation, Quaternion.Euler(new Vector3()), (float)(Time.deltaTime * 10));
            if (target.transform.rotation == Quaternion.Euler(new Vector3()))
            {
                Roling = false;
            }
        }

        if (!Playing && !Roling && Input.GetMouseButton(0))
        {
            IsPress = true;
            OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
            OffsetX = OffsetX > 50 ? 0 : OffsetX;
            target.transform.RotateAround(new Vector2(0f, -10f), new Vector3(0f, 0f, 1f), -OffsetX);
            if (Mathf.Abs(role) > 1)
            {
                if (role > 0)
                {
                    Ttext.text = GameManager.Instance.card.YES;
                    Vector4 vector4 = GameManager.Instance.card.YESValue;
                    ShowFlag(vector4);
                }
                else
                {
                    Ttext.text = GameManager.Instance.card.NO;
                    Vector4 vector4 = GameManager.Instance.card.NOValue;
                    ShowFlag(vector4);
                }               
            }
            role += OffsetX;
            if(role < 0)
            {
                mask.transform.position += new Vector3(OffsetX * 0.3f, OffsetX * 0.3f, 0f);
            }
            else
            {
                mask.transform.position += new Vector3(OffsetX * 0.3f, -OffsetX * 0.3f, 0f);
            }
        }
        if (!IsPress)
        {
            GameManager.Instance.Speed.transform.Find("change").gameObject.SetActive(false);
            GameManager.Instance.Healthy.transform.Find("change").gameObject.SetActive(false);
            GameManager.Instance.Opinion.transform.Find("change").gameObject.SetActive(false);
            GameManager.Instance.Dangerous.transform.Find("change").gameObject.SetActive(false);
            mask.transform.position = new Vector3(0, 5.3f, 0);
            Ttext.text = string.Empty;
            if (Mathf.Abs(role) > 8)
            {
                Playing = true;
                if (role > 0)
                {
                    GameManager.Instance.NextCard(true);
                }
                else
                {
                    GameManager.Instance.NextCard(false);
                }
                role = 0;
            }
            else if(!Playing)
            {
                role = 0;
                target.transform.position = new Vector3(0f, -1.7f, 0f);
                target.transform.rotation = Quaternion.Euler(new Vector3());
            }
        }
        IsPress = false;
    }
}
