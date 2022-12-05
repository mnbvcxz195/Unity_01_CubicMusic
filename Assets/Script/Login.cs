using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class Login : MonoBehaviour
{
    [SerializeField] InputField id = null;
    [SerializeField] InputField pw = null;
    public Text Re;

    BackendReturnObject bro = null;
    bool isInit = false;

    void Start()
    {
        bro = Backend.Initialize(true);
    }

    private void Update()
    {
        if (bro == null || isInit == true)
            return;

        if (bro.IsSuccess())
        {
            isInit = true;
            // �ʱ�ȭ ���� �� ����
            Debug.Log("�ʱ�ȭ ����: " + bro.ToString());
        }
        else
        {
            // �ʱ�ȭ ���� �� ����
            Debug.LogError("�ʱ�ȭ ����: " + bro.ToString());
        }
    }

    void InitializeCallback()
    {
        if (Backend.IsInitialized)
        {
            Debug.Log(Backend.Utils.GetServerTime());
            Debug.Log(Backend.Utils.GetGoogleHash());
        }
        else
            Debug.Log("�ʱ�ȭ ����");
    }

    public void BtnRegist()
    {
        string t_id = id.text;
        string t_pw = pw.text;

        BackendReturnObject bro = Backend.BMember.CustomSignUp(t_id, t_pw, "TEST");

        if (bro.IsSuccess())
        {
            Re.gameObject.SetActive(true);
            Re.text = "ȸ������ ����";

        }
        else
        {
            Re.gameObject.SetActive(true);
            Re.text = "ȸ������ ����";

        }
    }

    public void BtnLogin()
    {
        string t_id = id.text;
        string t_pw = pw.text;

        BackendReturnObject bro = Backend.BMember.CustomLogin(t_id, t_pw);

        if (bro.IsSuccess())
        {
            Re.gameObject.SetActive(true);
            Re.text = "�α��� ����";
            Invoke("DisAppoint", 1);
        }
        else
        {
            Re.gameObject.SetActive(true);
            Re.text = "�α��� ����";
        }
    }

    public void DisAppoint()
    {
        this.gameObject.SetActive(false);
    }
}
