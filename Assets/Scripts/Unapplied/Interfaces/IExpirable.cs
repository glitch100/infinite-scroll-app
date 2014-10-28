namespace Assets.Scripts.Unapplied.Interfaces
{
    public interface IExpirable
    {
        float TimeAlive { get; set; }
        float ExpiryTime { get; set; }
        void Expire();

    }
}