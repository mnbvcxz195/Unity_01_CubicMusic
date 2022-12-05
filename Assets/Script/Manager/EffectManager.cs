using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;
    
    string hit = "Hit";

    [SerializeField] Animator judgementAnimator = null;
    [SerializeField] Image judgementImage = null;
    [SerializeField] Sprite[] judgementSprite = null;
    [SerializeField] Image noteHitImage = null;

    public void JudgementEffect(int p_num)
    {
        judgementImage.gameObject.SetActive(true);
        judgementImage.sprite = judgementSprite[p_num];
        judgementAnimator.SetTrigger(hit);
    }
    public void NoteHitEffect()
    {
        noteHitImage.gameObject.SetActive(true);
        noteHitAnimator.SetTrigger(hit);
    }


}
