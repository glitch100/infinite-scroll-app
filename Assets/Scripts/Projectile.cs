using System;
using Assets.Scripts.Unapplied;
using Assets.Scripts.Unapplied.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Projectile : MonoBehaviour, IExpirable
    {
        private GameManager _gameManager;
        public Vector3 Point;
        public float TimeAlive { get; set; }
        public float ExpiryTime { get; set; }
        public float MovementSpeed;

        void Awake()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        void Start()
        {
            transform.LookAt(Point);
            rigidbody.freezeRotation = true;
            MovementSpeed = 10f;
            ExpiryTime = 1.8f;
            InvokeRepeating("Expire",0,1.0f);
        }

        void FixedUpdate()
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
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.tag.Equals(Tags.Enemy, StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("Valid Hit!");
                _gameManager.KillCount++;
                Destroy(collision.gameObject);
                //TODO: Damage enemy
            }
            if (collision.gameObject.tag.Equals(Tags.Player, StringComparison.OrdinalIgnoreCase))
            {
                collision.gameObject.GetComponent<Player>().Lives--;
                //TODO: Damage player          
            }

        }

    }
}
