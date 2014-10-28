using System;
using Assets.Scripts.Unapplied;
using Assets.Scripts.Unapplied.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Projectile : MonoBehaviour, IExpirable
    {
        public Vector3 Point;
        public float TimeAlive { get; set; }
        public float ExpiryTime { get; set; }
        public float MovementSpeed;

        public void Start()
        {
            transform.LookAt(Point);
            rigidbody.freezeRotation = true;
            MovementSpeed = 10f;
            ExpiryTime = 1.8f;
            InvokeRepeating("Expire",0,1.0f);
        }

        public void FixedUpdate()
        {
            transform.Translate(0, 0, MovementSpeed * Time.deltaTime);
        }

        public void Expire()
        {
            if (ExpiryTime >= TimeAlive)
            {
                TimeAlive++;
            }
            else
            {
                Destroy(this);
            }
        }

        void OnTriggerEnter(Collider collision)
        {
            if (collider.gameObject.tag.Equals(Tags.Enemy))
            {
                Debug.Log("Valid Hit!");
                //TODO: Damage enemy
            }
            if (!collision.gameObject.tag.Equals(Tags.Player))
            {
                Destroy(transform.parent.gameObject);               
            }

        }

    }
}
