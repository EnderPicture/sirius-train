using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarSystem : MonoBehaviour
{
    public SpriteRenderer star1;
    public SpriteRenderer star2;
    public SpriteRenderer star3;
    public SpriteRenderer star4;
    public SpriteRenderer star5;

    public void setScore(int score)
    {
        DOTween.Init();
        score = Mathf.Clamp(score, 0, 5);
        if (score > 0)
        {
            star1.DOFade(0, .25f);
        }
        if (score > 1)
        {
            star2.DOFade(0, .25f);
        }
        if (score > 2)
        {
            star3.DOFade(0, .25f);
        }
        if (score > 3)
        {
            star4.DOFade(0, .25f);
        }
        if (score > 4)
        {
            star5.DOFade(0, .25f);
        }
    }
}
