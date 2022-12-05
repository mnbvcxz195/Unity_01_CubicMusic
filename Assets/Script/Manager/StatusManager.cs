using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [SerializeField] float blickSpeed = 0.1f;
    [SerializeField] int blinkCount = 10;
    int currentBlinkCount = 0;
    bool isBlink = false;

    bool isDead = false;

    int maxHP = 3;
    int currentHP = 3;

    int maxShield = 3;
    int currentShield = 0;

    [SerializeField] Image[] hpImage = null;
    [SerializeField] Image[] shieldImage = null;

    [SerializeField] int shieldIncreaseCombo = 20;
    int currentShieldCombo = 0;
    [SerializeField] Image shieldGauge = null;

    Result theResult;
    NoteManager theNote;
    [SerializeField] MeshRenderer playerMesh = null;
    public Slider hpBar;

    float curval = 1;

    private void Start()
    {
        theResult = FindObjectOfType<Result>(true);
        theNote = FindObjectOfType<NoteManager>();
        hpBar = GetComponentInChildren<Slider>(true);
    }

    public void Initialized()
    {
        currentHP = maxHP;
        currentShield = 0;
        currentShieldCombo = 0;
        shieldGauge.fillAmount = 0;
        isDead = false;
        SettingHPImage();
        SettingShieldImage();
    }

    public void CheckShield()
    {
        currentShieldCombo++;

        if(currentShieldCombo >= shieldIncreaseCombo)
        {
            currentShieldCombo = 0;
            IncreaseShield();
        }

        shieldGauge.fillAmount = (float)currentShieldCombo / shieldIncreaseCombo;
    }

    public void ResetShieldCombo()
    {
        currentShieldCombo = 0;
        shieldGauge.fillAmount = (float)currentShieldCombo / shieldIncreaseCombo;
    }

    public void IncreaseShield()
    {
        currentShield++;

        if (currentShield >= maxShield)
            currentShield = maxShield;

        SettingShieldImage();
    }

    public void DecreasShield(int p_num)
    {
        currentShield -= p_num;

        if (currentShield <= 0)
            currentShield = 0;

        SettingShieldImage();
    }

    public void IncreaseHP(int p_num)
    {
        currentHP += p_num;
        if (currentHP >= maxHP)
            currentHP = maxHP;

        SettingHPImage();
    }

    public void DecreaseHp(int p_num)
    {
        if (!isBlink)
        {
            if (currentShield > 0)
                DecreasShield(p_num);
            else
            {
                currentHP -= p_num;

                if (currentHP <= 0)
                {
                    isDead = true;
                    theResult.ShowResult();
                    theNote.RemoveNote();
                }
                else
                {
                    StartCoroutine(BlickCo());
                }

                SettingHPImage();
            }
            
        }
        
    }

    void SettingHPImage()
    {
        for(int i = 0; i < hpImage.Length; i++)
        {
            if (i < currentHP)
                hpImage[i].gameObject.SetActive(true);
            else
                hpImage[i].gameObject.SetActive(false);
        }
    }

    void SettingShieldImage()
    {
        for (int i = 0; i < shieldImage.Length; i++)
        {
            if (i < currentShield)
                shieldImage[i].gameObject.SetActive(true);
            else
                shieldImage[i].gameObject.SetActive(false);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    IEnumerator BlickCo()
    {
        isBlink = true;

        while (currentBlinkCount <= blinkCount)
        {
            playerMesh.enabled = !playerMesh.enabled;
            yield return new WaitForSeconds(blickSpeed);
            currentBlinkCount++;
        }

        playerMesh.enabled = true;
        currentBlinkCount = 0;
        isBlink = false;
    }

    public void DecreaseHpBar()
    {
        curval = hpBar.value - 0.1f;
        hpBar.value = curval;
        if (hpBar.value <= 0)
        {
            isDead = true;
            theResult.ShowResult();
            theNote.RemoveNote();
        }
    }

    public void IncreaseHpBar()
    {
        curval = hpBar.value + 0.1f;
        hpBar.value = curval;
        if (hpBar.value >= 1)
        {
            hpBar.value = 1;
            curval = 1;
        }
    }
}
