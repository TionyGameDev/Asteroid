using PropertySystem;

namespace Bullets
{
    public interface IBullet
    {
        void Init(ImpactSetting impact);
        void Dispose();
    }
}