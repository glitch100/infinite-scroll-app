using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Unapplied;
using Assets.Scripts.Unapplied.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour, ILiving, IWeapon
    {        
        public int HealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }
        public bool Firing { get; set; }
        public float FireRate { get; set; }

        public GameObject Projectile;

        void Awake()
        {
            FireRate = 1.12f;
        }

        void FixedUpdate()
        {
            transform.RotateAround(transform.position,new Vector3(0,360,0),1f);

            var enemy = GetPlayer();
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

        private Player GetPlayer()
        {
            var player = GameObject.FindGameObjectWithTag(Tags.Player);
            if (player != null && Vector3.Distance(transform.position,player.transform.position) < 4.3f)
            {
                RaycastHit raycastHit;
                if (Physics.Linecast(transform.position, player.transform.position, out raycastHit))
                {
                    if (raycastHit.collider.tag != "Obstacle")
                    {
                        Debug.DrawLine(transform.position, player.transform.position, Color.blue);
                        return player.GetComponent<Player>();
                    }
                }
            }

            return null;
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
