using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    
    public static bool player1 = false, player2 = false, player3 = false;

    public static void ResetPlayers() {
        player1 = false;
        player2 = false;
        player3 = false;
    }

}
