using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class ClipBoard : MonoBehaviour
{
    public Transform target;
    public float smoothFactor = 10f;
    Vector3 offset;

    public float offRot = 90;
    public float onRot = 7.83f;
    public float onRotOther = -28.3f;

    public int show = 0;

    public GameObject MidWinScreen;
    public GameObject WinScreen;
    public GameObject MenuScreen;
    public GameObject DieScreen;

    public TextMeshPro winScreenScoreText;
    public TextMeshPro midWinScreenScoreText;

    [Header("MidWinStars")]
    public StarSystem Parking;
    public StarSystem Timing;
    public StarSystem Confort;
    public StarSystem Bonus;

    [Header("FinalWinStars")]
    public StarSystem FinalParking;
    public StarSystem FinalTiming;
    public StarSystem FinalConfort;
    public StarSystem FinalBonus;


    private void Start()
    {
        DOTween.Init();
        offset = transform.position - target.transform.position;
        MidWinScreen.SetActive(false);
        WinScreen.SetActive(false);
        DieScreen.SetActive(false);
        MenuScreen.SetActive(true);
    }
    private void Update()
    {
        // Vector3 rot = transform.eulerAngles;
        // float rotZ = transform.eulerAngles.z;
        if (show == 1)
        {
            // rotZ = Mathf.Lerp(rotZ, onRot, smoothFactor * Time.deltaTime * .5f);
            transform.DORotate(new Vector3(0, 0, onRot), 1);
        }
        else if (show == 2)
        {
            // rotZ = Mathf.Lerp(rotZ, onRotOther, smoothFactor * Time.deltaTime * .5f);
            transform.DORotate(new Vector3(0, 0, onRotOther), 1);
        }
        else
        {
            // rotZ = Mathf.Lerp(rotZ, offRot, smoothFactor * Time.deltaTime * .5f);
            transform.DORotate(new Vector3(0, 0, offRot), 1);
        }
        // rot.z = rotZ;

        // transform.eulerAngles = rot;

        Vector3 smoothedPos = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
        transform.position = smoothedPos;
    }

    public void ShowWin(List<int> MaxAccelerationScores, List<int> ParkingJobScores, List<int> TimingScores, List<int> ScanScores)
    {
        WinScreen.SetActive(true);
        MidWinScreen.SetActive(false);
        MenuScreen.SetActive(false);
        DieScreen.SetActive(false);

        int MaxAccelerationScore = 0;
        int ParkingJobScore = 0;
        int TimingScore = 0;
        int ScanScore = 0;

        int sum = 0;
        for (int i = 0; i < MaxAccelerationScores.Count; i++)
        {
            sum += MaxAccelerationScores[i];
        }
        MaxAccelerationScore = sum / MaxAccelerationScores.Count;

        sum = 0;
        for (int i = 0; i < ParkingJobScores.Count; i++)
        {
            sum += ParkingJobScores[i];
        }
        ParkingJobScore = sum / ParkingJobScores.Count;

        sum = 0;
        for (int i = 0; i < TimingScores.Count; i++)
        {
            sum += TimingScores[i];
        }
        TimingScore = sum / TimingScores.Count;

        sum = 0;
        for (int i = 0; i < ScanScores.Count; i++)
        {
            sum += ScanScores[i];
        }
        ScanScore = sum / ScanScores.Count;

        FinalParking.setScore(ParkingJobScore);
        FinalTiming.setScore(TimingScore);
        FinalConfort.setScore(MaxAccelerationScore);
        FinalBonus.setScore(ScanScore);

        winScreenScoreText.SetText("");

        show = 1;
    }

    float map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }

    public void ShowMidWin(int parkingScore, int conScore, int timeScore, int scanScore, string TimingText)
    {
        WinScreen.SetActive(false);
        MidWinScreen.SetActive(true);
        MenuScreen.SetActive(false);
        DieScreen.SetActive(false);

        Parking.setScore(parkingScore);
        Timing.setScore(timeScore);
        Confort.setScore(conScore);
        Bonus.setScore(scanScore);

        if (TimingText == "Too Early")
        {
            TimingText = TimingText + "...";
        }
        if (TimingText == "Early")
        {
            TimingText = TimingText + "?";
        }
        if (TimingText == "Perfect")
        {
            TimingText = TimingText + "!!";
        }
        if (TimingText == "Late")
        {
            TimingText = TimingText + "...";
        }
        midWinScreenScoreText.SetText(TimingText);

        show = 1;
    }

    public void ShowDieScreen()
    {
        WinScreen.SetActive(false);
        MidWinScreen.SetActive(false);
        MenuScreen.SetActive(false);
        DieScreen.SetActive(true);
        show = 1;
    }

    public void ClickedPlay()
    {
        show = 0;
    }

    public void ClickedBack()
    {
        show = 1;

    }

    public void ClickedModeChooser()
    {
        show = 2;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadSceneL1(string scene)
    {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene(scene);
    }
    public void LoadSceneL2(string scene)
    {
        PlayerPrefs.SetInt("Level", 2);
        SceneManager.LoadScene(scene);
    }
    public void LoadSceneL3(string scene)
    {
        PlayerPrefs.SetInt("Level", 3);
        SceneManager.LoadScene(scene);
    }
}
