namespace WorldsDev
{
    public interface IDamageable
    {
        bool Alive();

        void OnHit(int dmg);

        void OnDeath();
    }
}