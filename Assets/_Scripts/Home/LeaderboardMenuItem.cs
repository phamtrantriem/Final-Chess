using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderboardMenuItem : MonoBehaviour
{
    [SerializeField] private GameObject _top3;
    [SerializeField] private GameObject _rank1;
    [SerializeField] private GameObject _rank2;
    [SerializeField] private GameObject _rank3;
    [SerializeField] private GameObject _rankN;
    [SerializeField] private TMP_Text _nameTxt;
    [SerializeField] private TMP_Text _winTxt;
    [SerializeField] private TMP_Text _rankTxt;
    [SerializeField] private GameObject _iconYours;

    public void InitData(string username, int win, int rank, bool yours)
    {
        switch (win)
        {
            case 1:
                _rank1.SetActive(true);
                _rank2.SetActive(false);
                _rank3.SetActive(false);
                _rankN.SetActive(false);
                break;
            case 2:
                _rank1.SetActive(false);
                _rank2.SetActive(true);
                _rank3.SetActive(false);
                _rankN.SetActive(false);
                break;
            case 3:
                _rank1.SetActive(false);
                _rank2.SetActive(false);
                _rank3.SetActive(true);
                _rankN.SetActive(false);
                break;
            case 4:
                _top3.SetActive(false);
                _rankN.SetActive(true);
                break;
        }

        _nameTxt.text = username;
        _winTxt.text = "Win: " + win.ToString();
        _iconYours.SetActive(yours);
        _rankTxt.text = rank.ToString();
    }
}
