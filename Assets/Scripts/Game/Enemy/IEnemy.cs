
namespace ProjectSurvivor
{
    public interface IEnemy
    {
        void Hurt(float hurtValue, bool force = false);

        void SetSpeedScale(float speedScale);

        void SetHPScale(float hpScale);
    }
}
