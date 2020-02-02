using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RobotParts : MonoBehaviour
{
    public IconNumberManager gear;
    public IconNumberManager spring;
    public IconNumberManager battery;

    public PlayerController playerInfo;

    private void Update()
    {
        gear.IconsExistence(playerInfo.numGear);
        spring.IconsExistence(playerInfo.numSpring);
        battery.IconsExistence(playerInfo.numBattery);
    }

}
