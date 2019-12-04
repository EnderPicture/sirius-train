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

    float delay = 1;
    float delayBetween = .25f;

    public void setScore(int score, float extraDelay)
    {
        DOTween.Init();
        star1.color = new Color(star1.color.r, star1.color.g, star1.color.b, 0);
        star2.color = new Color(star2.color.r, star2.color.g, star2.color.b, 0);
        star3.color = new Color(star3.color.r, star3.color.g, star3.color.b, 0);
        star4.color = new Color(star4.color.r, star4.color.g, star4.color.b, 0);
        star5.color = new Color(star5.color.r, star5.color.g, star5.color.b, 0);

        score = Mathf.Clamp(score, 0, 5);
        StartCoroutine(delayAnimation(score, extraDelay));
    }
    IEnumerator delayAnimation(int score, float customDelay)
    {
        yield return new WaitForSeconds(customDelay);
        StartCoroutine(showStars1(score));
        StartCoroutine(showStars2(score));
        StartCoroutine(showStars3(score));
        StartCoroutine(showStars4(score));
        StartCoroutine(showStars5(score));
    }
    IEnumerator showStars1(int score)
    {
        yield return new WaitForSeconds(delay);
        if (score > 0)
        {
            star1.DOFade(1, 1f);
        }
    }
    IEnumerator showStars2(int score)
    {
        yield return new WaitForSeconds(delay + delayBetween);
        if (score > 1)
        {
            star2.DOFade(1, 1f);
        }
    }
    IEnumerator showStars3(int score)
    {
        yield return new WaitForSeconds(delay + delayBetween * 2);
        if (score > 2)
        {
            star3.DOFade(1, 1f);
        }
    }
    IEnumerator showStars4(int score)
    {
        yield return new WaitForSeconds(delay + delayBetween * 3);
        if (score > 3)
        {
            star4.DOFade(1, 1f);
        }
    }
    IEnumerator showStars5(int score)
    {
        yield return new WaitForSeconds(delay + delayBetween * 4);
        if (score > 4)
        {
            star5.DOFade(1, 1f);
        }
    }
}
