using System.Linq;
using Assets.Scripts.Unapplied;
using UnityEngine;
using System.Collections;
using Random = System.Random;

public class BlockSpawner : MonoBehaviour
{
    public Transform[] BlocksToSpawn;

    private Random _random;
    private Transform _blockGroup;
    void Start()
    {
        _random = new Random();
        _blockGroup = GameObject.FindGameObjectWithTag(Tags.BlockGroup).transform;
    }

    void Update()
    {
        var blocks = GameObject.FindGameObjectsWithTag(Tags.Block).ToList();

        if (blocks.Count < 6)
        {
            var chance = _random.NextDouble();
            if (chance > 0.86)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
        var block = Instantiate(BlocksToSpawn[_random.Next(0, BlocksToSpawn.Length)], transform.position, Quaternion.identity) as GameObject;
    }
}
