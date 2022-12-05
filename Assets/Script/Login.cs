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
            // 초기화 성공 시 로직
            Debug.Log("초기화 성공: " + bro.ToString());
        }
        else
        {
            // 초기화 실패 시 로직
            Debug.LogError("초기화 실패: " + bro.ToString());
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
            Debug.Log("초기화 실패");
    }

    public void BtnRegist()
    {
        string t_id = id.text;
        string t_pw = pw.text;

        BackendReturnObject bro = Backend.BMember.CustomSignUp(t_id, t_pw, "TEST");

        if (bro.IsSuccess())
        {
            Re.gameObject.SetActive(true);
            Re.text = "회원가입 성공";

        }
        else
        {
            Re.gameObject.SetActive(true);
            Re.text = "회원가입 실패";

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
            Re.text = "로그인 성공";
            Invoke("DisAppoint", 1);
        }
        else
        {
            Re.gameObject.SetActive(true);
            Re.text = "로그인 실패";
        }
    }

    public void DisAppoint()
    {
        this.gameObject.SetActive(false);
    }
}
