using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Unapplied.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour, ILiving
    {
        void FixedUpdate()
        {
            transform.RotateAround(transform.position,new Vector3(0,360,0),1f);
        }

        public int HealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }
    }
}
