public interface IHittable
{
    bool Hit(int damage);

    bool IsDead();
}