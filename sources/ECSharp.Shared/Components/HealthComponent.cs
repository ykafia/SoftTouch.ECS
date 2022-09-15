
namespace ECSharp.Components
{
    public struct HealthComponent
    {
        public float LifePoints {get;set;}
        public float Shield {get;set;}
        public HealthComponent(float lp, float sh)
        {
            LifePoints = lp;
            Shield = sh;
        }

    }
    
    public readonly struct ROHealthComponent
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