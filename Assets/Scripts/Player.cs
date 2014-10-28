using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Light _bottomLight;
        private float lightIntensity = 10f;

        public bool Firing = false;
        public bool WeaponLocked = false;
        public float FireRate = 0.7f;

        public GameObject Projectile;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _bottomLight = GetComponentInChildren<Light>();
            _bottomLight.enabled = false;
        }

        void Start()
        {

        }

        void FixedUpdate()
        {
            Movement();
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
                        projComponent.Point = enemy.transform.position;
                    }
                }
            }
        }

        IEnumerator FireWeapon()
        {
            if (!Firing)
            {
                Firing = true;
                yield return new WaitForSeconds(FireRate);
                Firing = false;
            }
        }

        private GameObject GetEnemy()
        {
            var visionRadius = Physics.OverlapSphere(transform.up + transform.position, 3);
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
                _rigidbody.AddForce((Vector3.up * 18), ForceMode.Acceleration);

                var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 4.5f * Time.deltaTime);
                
            }
            else
            {
                _bottomLight.intensity = 0;
                _bottomLight.enabled = false;

            }
        }
    }
}
