using Assets.Scripts;
using Assets.Scripts.Unapplied;
using UnityEngine;
using System.Collections;

public class DeathBar : MonoBehaviour 
{
	void Update () {

	
	}

    void OnColliderEnter(Collision other)
    {
        Debug.Log("COLLIDER");
        Destroy(other.gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == Tags.Player)
        {
            var player = col.gameObject.GetComponent<Player>();
            player.Dead = true;
            Debug.Log("Game Over!");
            Time.timeScale = 0;
        }
        else 
        {
            if (col.gameObject.tag != Tags.Floor)
            {
                Destroy(col.transform.parent.gameObject);     
            }
        }

    }

}
