using System.Collections;

namespace Assets.Scripts
{
    public interface IWeapon
    {
        IEnumerator FireWeapon();
        bool Firing { get; set; }
        float FireRate { get; set; }
    }
}