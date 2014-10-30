using System.Collections;
using System.Linq;
using Assets.Scripts.Unapplied;
using Assets.Scripts.Unapplied.Interfaces;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour, IWeapon, ILiving
    {
        private Rigidbody _rigidbody;
        private Light _bottomLight;
        private float lightIntensity = 10f;

        public int HealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }

        public bool Firing { get; set; }
        public bool Falling = false;
        public bool WeaponLocked = false;
        public float FireRate { get; set; }
        public const int MaxSpeed = 27;
        public GameObject Projectile;
        public bool Dead;
        private GameManager _gameManager;
        public  int Lives;
        public int Score;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _bottomLight = GetComponentInChildren<Light>();
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _bottomLight.enabled = false;
            _rigidbody.freezeRotation = true;
            FireRate = 0.5f;
            Lives = 3;
        }

        void Start()
        {

        }

        void Update()
        {
            if (Lives <= 0 && !Dead)
            {
                Dead = true;
                Time.timeScale = 0;
            }
        }

        void FixedUpdate()
        {
            Movement();

            Falling = _rigidbody.velocity.y < -20f;

            var enemy = GetEnemy();
            if (enemy != null)
            {
                if (!Firing)
                {
                    StartCoroutine(FireWeapon());
                    Projectile = Resources.Load("Prefabs/HomingProj") as GameObject;
                    if (Projectile != null)
                    {
                        var projGo = (GameObject)Instantiate(Projectile, transform.position + transform.forward, Quaternion.identity);
                        var projComponent = projGo.GetComponentInChildren<Projectile>();
                        projComponent.PlayerProj = true;
                        projComponent.Point = enemy.transform;
                    }
                }
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == Tags.PowerUp)
            {
                //TODO: Make generic system
                var powerup = col.gameObject.GetComponent<Powerup>();
                if (powerup.Type == PowerupType.Life)
                {
                   Lives += powerup.Value;
                }
                else if (powerup.Type == PowerupType.Score)
                {
                    Score += powerup.Value;
                }
                
                Destroy(col.transform.parent.gameObject);
            }
        }

        private GameObject GetEnemy()
        {
            var visionRadius = Physics.OverlapSphere(transform.up + transform.position, 5);
            if (visionRadius != null)
            {
                var validObjects = visionRadius.Where(go => go.tag == "Enemy");
                var closest = validObjects.FirstOrDefault();
                if (closest != null)
                {
                    RaycastHit raycastHit;
                    if (Physics.Linecast(transform.position, closest.transform.position, out raycastHit))
                    {
                        if (raycastHit.collider.tag != "Obstacle")
                        {
                            Debug.DrawLine(transform.position, closest.transform.position, Color.red);
                            return closest.gameObject;
                        }
                    }
                }
            }
            return null;
        }

        private void Movement()
        {
            if (Input.GetMouseButton(1))
            {
                var mouseMovement = Input.mousePosition;
                _bottomLight.enabled = true;
                _bottomLight.intensity = lightIntensity;
                _rigidbody.AddForce((Vector3.up * 15), ForceMode.Acceleration);

                var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 4.5f * Time.deltaTime);
                if (rigidbody.velocity.magnitude > 20)
                {
                    rigidbody.velocity = rigidbody.velocity.normalized*MaxSpeed;
                }
                
            }
            else
            {
                _bottomLight.intensity = 0;
                _bottomLight.enabled = false;

            }
        }

        public IEnumerator FireWeapon()
        {
            if (!Firing)
            {
                Firing = true;
                yield return new WaitForSeconds(FireRate);
                Firing = false;
            }
        }

    }
}
