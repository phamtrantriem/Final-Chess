using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardMenu : MonoBehaviour
{
    [SerializeField] private GameObject _rankViewHolder;
    [SerializeField] private LeaderboardMenuItem _leaderboardMenuItemPref;
    [SerializeField] private TMP_InputField _userFullname;
    [SerializeField] private TMP_Text _rankTxt;
    [SerializeField] private TMP_Text _winMatchTxt;

    // Start is called before the first frame update
    void Start()
    {
        _userFullname.text = UserManager.instance.fullName;
        SendGetRank();
    }
    
    private async void SendGetRank(){
        
        var response = await ApiRequest.instance.SendGetRankRequest();
        _rankTxt.text = "NaN";
        _winMatchTxt.text = "NaN";
        if (response.success)
        {
            int index = 1;
            foreach (var rank in response.ranking)
            {
                bool yours = rank.userData._id == UserManager.instance.id;
                string name = rank.userData.fullName;
                int _winMatch = rank.count;

                var rankItem = Instantiate(_leaderboardMenuItemPref, _rankViewHolder.transform);
                rankItem.InitData(name, rank.count, index, yours);
                rankItem.gameObject.SetActive(true);
                
                if (yours)
                {
                    _rankTxt.text = index.ToString();
                    _winMatchTxt.text = _winMatch.ToString();
                }
                index++;
            }
        }
        else
        {
            Debug.Log(response.message);
        }
    }

    
}
