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
        public double Chance;
        public float Timer;

        void Start()
        {
            Random = new Random();
        }

        private void Update()
        {
            if (!HaveSpawned)
            {
                var chance = Random.NextDouble();
                if (chance > Chance)
                {
                    Spawn();
                    HaveSpawned = true;
                    StartCoroutine(Wait());
                }
            }
        }

        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(Timer);
            HaveSpawned = false;
        }

        public void Spawn()
        {
            var spawnPos = UnityEngine.Random.Range(-10.5f, 10.1f);
            if (PowerUpGameObject != null)
            {
                Instantiate(PowerUpGameObject, new Vector3(spawnPos, transform.position.y, transform.position.z), Quaternion.identity);
            }
        }
    }
}
