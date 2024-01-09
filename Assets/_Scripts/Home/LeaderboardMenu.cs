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

    private int _selfRank;
    private int _winMatch;
    
    // Start is called before the first frame update
    void Start()
    {
        _userFullname.text = UserManager.instance.fullName;
        SendGetRank();
    }
    
    private async void SendGetRank(){
        
        var response = await ApiRequest.instance.SendGetRankRequest();
        
        if (response.success)
        {
            _selfRank = response.rank.selfRank;
            foreach (var user in response.rank.top10)
            {
                bool yours = user._id == UserManager.instance.id;

                string name = user.fullName;
                var rankItem = Instantiate(_leaderboardMenuItemPref, _rankViewHolder.transform);

                rankItem.InitData(name, user.win, user.rank, yours);
                rankItem.gameObject.SetActive(true);
            }

            _rankTxt.text = _selfRank.ToString();
            _winMatchTxt.text = _winMatch.ToString();
            
        }
        else
        {
            Debug.Log(response);
        }
    }

    
}
