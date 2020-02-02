using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController mainPlayer;
    public static PlayerController player;


    private void Start()
    {
        if(mainPlayer !=null)
            player = mainPlayer;
    }
    public static PlayerController GetPlayer()
    {
        return player;
    }
}
