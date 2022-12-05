using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;

public class DatabaseManager : MonoBehaviour
{
    public int[] score;



    private void Start()
    {
        LoadScore();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score1", score[0]);
        PlayerPrefs.SetInt("Score2", score[1]);
        PlayerPrefs.SetInt("Score3", score[2]);
        PlayerPrefs.SetInt("Score4", score[3]);
        PlayerPrefs.SetInt("Score5", score[4]);
    }

    public void LoadScore()
    {
        if (PlayerPrefs.HasKey("Score1"))
        {
            score[0] = PlayerPrefs.GetInt("Score1");
            score[1] = PlayerPrefs.GetInt("Score2");
            score[2] = PlayerPrefs.GetInt("Score3");
            score[3] = PlayerPrefs.GetInt("Score4");
            score[4] = PlayerPrefs.GetInt("Score5");
        }
    }
}
