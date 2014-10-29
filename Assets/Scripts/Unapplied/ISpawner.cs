using System;
using System.Collections;

namespace Assets.Scripts.Unapplied
{
    public interface ISpawner
    {
        IEnumerator Wait();
        Random Random { get; set; }
        bool HaveSpawned { get; set; }
        void Spawn();
    }
}