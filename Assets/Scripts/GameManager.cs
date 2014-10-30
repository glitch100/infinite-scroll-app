using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public int KillCount;

    public Transform Floor;
    public Player Player;

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUI.Label(new Rect(10, 10, 200, 50), string.Format("Kills: {0}", KillCount));
        GUI.Label(new Rect(10, 30, 200, 50), string.Format("Score: {0}", Player.Score));
        GUI.Label(new Rect(10, 50, 200, 50), string.Format("Lives: {0}", Player.Lives));

        if (Player.Dead)
        {
            GUI.Label(new Rect(60, 100, 200, 50), string.Format("Final Score: Kills * Score = {0}", KillCount * Player.Score));
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndArea();
        
    }

    void Update()
    {

        var d = Vector3.Distance(Floor.position, Player.transform.position);
        if (d < 10)
        {
            Player.Score = 10;
        }else if (d < Player.Score)
        {
            return;
        }
        else
        {
            Player.Score = (int)d;
        }


    }
	
}
