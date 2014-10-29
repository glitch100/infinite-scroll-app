using Assets.Scripts;
using Assets.Scripts.Unapplied;
using UnityEngine;
using System.Collections;

public class DeathBar : MonoBehaviour 
{
	void Update () {

	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == Tags.Player)
        {
            var player = col.gameObject.GetComponent<Player>();
            player.Dead = true;
            Debug.Log("Game Over!");
            Debug.Break();
        }
        else 
        {
            Destroy(col.transform.parent.gameObject);
        }

    }

}
