using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class LockedCameraScript : MonoBehaviour
{
    public Player Player;

    void Awake()
    {
        transform.position = new Vector3(0, Player.transform.position.y, -1f);
    }

    void Update()
    {
        if (!Player.Falling)
        {
            transform.position = new Vector3(0, Player.transform.position.y, -1f);
        }
    }

}
