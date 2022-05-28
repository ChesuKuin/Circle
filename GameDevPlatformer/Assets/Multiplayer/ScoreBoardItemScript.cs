using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class ScoreBoardItemScript : MonoBehaviour
{
    public TeleportScript Death;
    public LevelAdvanceScript Win;
    public TMP_Text usernameText;
    public TMP_Text winText;
    public TMP_Text deathText;
    // Update is called once per frame
    public void Initialize(Player player)
    {
        usernameText.text = player.NickName;
        winText.text = Win.Win1;
        deathText.text = Death.death1;
    }
}
