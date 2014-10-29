using Assets.Scripts.Unapplied;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelBlock : MonoBehaviour
    {
        private Player _player;
        void Awake()
        {
            _player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
        }

        void Update()
        {
            if (!renderer.isVisible && Vector3.Distance(_player.transform.position, transform.position) > 15f)
            {
                Destroy(transform.parent.gameObject);
            }        
        }

    }
}
