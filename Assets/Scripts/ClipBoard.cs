using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClipBoard : MonoBehaviour
{
    public Transform target;
    public float smoothFactor = 10f;
    Vector3 offset;

    public float offRot = 90;
    public float onRot = 7.83f;

    public bool show = false;

    public GameObject MidWinScreen;
    public GameObject WinScreen;
    public GameObject MenuScreen;
    public GameObject DieScreen;

    public TextMeshPro winScreenScoreText;


    private void Start()
    {
        offset = transform.position - target.transform.position;
        MidWinScreen.SetActive(false);
        WinScreen.SetActive(false);
        DieScreen.SetActive(false);
        MenuScreen.SetActive(true);
    }
    private void LateUpdate()
    {
        Vector3 rot = transform.eulerAngles;
        float rotZ = transform.eulerAngles.z;
        if (show)
        {
            rotZ = Mathf.Lerp(rotZ, onRot, smoothFactor * Time.deltaTime * .5f);
        }
        else
        {
            rotZ = Mathf.Lerp(rotZ, offRot, smoothFactor * Time.deltaTime * .5f);
        }
        rot.z = rotZ;
        transform.eulerAngles = rot;

        Vector3 smoothedPos = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
        transform.position = smoothedPos;
    }

    public void ShowWin(float timeUsed, float maxAcc, List<float> parkingJobScore)
    {
        WinScreen.SetActive(true);
        MidWinScreen.SetActive(false);
        MenuScreen.SetActive(false);
        DieScreen.SetActive(false);

        float totalParkingScore = 0;
        foreach (float score in parkingJobScore)
        {
            totalParkingScore += Mathf.Abs(score);
        }
        float avgParkingScore = totalParkingScore / parkingJobScore.Count;

        float timeScore = Mathf.Round(Mathf.Clamp(map(timeUsed, 50, 250, 40, 0), 0, 40));
        float accScore = Mathf.Round(Mathf.Clamp(map(maxAcc, 0.01f, 0.03f, 30, 0), 0, 30));
        float parkingScore = Mathf.Round(Mathf.Clamp(map(avgParkingScore, 0, 7f, 30, 0), 0, 30));
        Debug.Log(timeUsed+ " " + maxAcc +" "+ avgParkingScore);

        winScreenScoreText.SetText(
            "Time Used: " + timeScore + "/40\n" +
            "Confort Level: " + accScore + "/30\n" +
            "Parking Job: " + parkingScore + "/30\n" +
            "\n" +
            "Final Score: " + (timeScore+accScore+parkingScore) +"/100"
        );

        show = true;
    }

    float map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }

    public void ShowMidWin()
    {
        WinScreen.SetActive(false);
        MidWinScreen.SetActive(true);
        MenuScreen.SetActive(false);
        DieScreen.SetActive(false);
        show = true;
    }

    public void ShowDieScreen()
    {
        WinScreen.SetActive(false);
        MidWinScreen.SetActive(false);
        MenuScreen.SetActive(false);
        DieScreen.SetActive(true);
        show = true;
    }

    public void ClickedPlay()
    {
        show = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
