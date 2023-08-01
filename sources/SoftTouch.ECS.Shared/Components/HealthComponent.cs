
namespace SoftTouch.ECS.Shared.Components
{
    public record struct HealthComponent
    {
        public float LifePoints {get;set;}
        public float Shield {get;set;}
        public HealthComponent(float lp, float sh)
        {
            LifePoints = lp;
            Shield = sh;
        }
        public override string ToString()
        {
            return $"[{LifePoints},{Shield}]";
        }

    }
    
    public record struct ROHealthComponent
    {
        public float LifePoints {get;}
        public float Shield {get;}
        public ROHealthComponent(float lp, float sh)
        {
            LifePoints = lp;
            Shield = sh;
        }
    }
}