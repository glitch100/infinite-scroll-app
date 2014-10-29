using System.Collections;
using Assets.Scripts.Unapplied;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class PowerupSpawner : MonoBehaviour, ISpawner
    {

        public Transform PowerUpGameObject;
        public bool HaveSpawned { get; set; }
        public Random Random { get; set; }

        void Start()
        {
            Random = new Random();
        }

        private void Update()
        {
            if (!HaveSpawned)
            {
                var chance = Random.NextDouble();
                if (chance > 0.71)
                {
                    Spawn();
                    HaveSpawned = true;
                    StartCoroutine(Wait());
                }
            }
        }

        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(10f);
            HaveSpawned = false;
        }

        public void Spawn()
        {
            Instantiate(PowerUpGameObject, transform.position, Quaternion.identity);
        }
    }
}
