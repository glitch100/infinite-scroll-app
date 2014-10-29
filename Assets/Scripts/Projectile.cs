using System;
using Assets.Scripts.Unapplied;
using Assets.Scripts.Unapplied.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Projectile : MonoBehaviour, IExpirable
    {
        private GameManager _gameManager;
        public Transform Point;
        public bool PlayerProj;
        public float TimeAlive { get; set; }
        public float ExpiryTime { get; set; }
        public float MovementSpeed;
        private bool _locked;
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
                Die();
            }
        }

        public void Die()
        {
            Destroy(transform.parent.gameObject);
        }

        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag.Equals(Tags.Enemy, StringComparison.OrdinalIgnoreCase) && PlayerProj)
            {
                _gameManager.KillCount++;
                Destroy(collision.gameObject);
                Die();
                //TODO: Damage enemy
            }else if (collision.gameObject.tag.Equals(Tags.Player, StringComparison.OrdinalIgnoreCase) && !PlayerProj)
            {
                if (!_locked)
                {
                    var player = collision.gameObject.GetComponent<Player>();
                    if (player != null)
                    {
                        _locked = true;
                        collision.gameObject.GetComponent<Player>().Lives -= 1;
                        Die();
                        _locked = false;
                    }
                }

                //TODO: Damage player          
            }

        }

    }
}
