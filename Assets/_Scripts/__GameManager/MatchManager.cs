using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoSingleton<MatchManager>
{
    public string userBlue;
    public int userBlueCoin;
    public string userRed;
    public int userRedCoin;
    public int round;
}
