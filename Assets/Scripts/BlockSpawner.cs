using System.Collections;
using System.Linq;
using Assets.Scripts.Unapplied;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class BlockSpawner : MonoBehaviour, ISpawner
    {
        public Transform[] BlocksToSpawn;

        public Random Random { get; set; }
        private Transform _blockGroup;
        public bool HaveSpawned { get; set; }
        void Start()
        {
            Random = new Random();
            //TODO: Have a block manager
            //_blockGroup = GameObject.FindGameObjectWithTag(Tags.BlockGroup).transform;
        }

        private void Update()
        {
            var blocks = GameObject.FindGameObjectsWithTag(Tags.Block).ToList();

            if (blocks.Count < 12 && !HaveSpawned)
            {
                var chance = Random.NextDouble();
                if (chance > 0.86)
                {
                    Spawn();
                    HaveSpawned = true;
                    StartCoroutine(Wait());
                }
            }

        }

        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(1.2f);
            HaveSpawned = false;
        }

        public void Spawn()
        {
            var block = Instantiate(BlocksToSpawn[Random.Next(0, BlocksToSpawn.Length)], transform.position, Quaternion.identity) as GameObject;
        }
    }
}
