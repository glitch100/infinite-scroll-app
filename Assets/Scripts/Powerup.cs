using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

    public int Value;
    public PowerupType Type;

    protected virtual void Start()
    {
        
    }    
    
    protected virtual void Update()
    {
        
    }
}

public enum PowerupType
{
    Score,
    Life,
    Multiplier
}
