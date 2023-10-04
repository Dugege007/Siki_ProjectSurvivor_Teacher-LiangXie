
namespace ProjectSurvivor
{
    public interface IEnemy
    {
        void Hurt(float hurtValue, bool force = false, bool critical = false);

        void SetSpeedScale(float speedScale);

        void SetHPScale(float hpScale);
    }
}
