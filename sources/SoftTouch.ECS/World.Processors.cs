using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.ComponentData;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS
{
    public partial class World
    {
        public void AddProcessor(Processor p)
        {
            Processors.Add(p.With(this));
        }        
        public void AddStartupProcessor(Processor processor)
        {
            processor.World = this;
            StartupProcessors.Add(processor);
        }


        public void AddProcessor<T>() where T : Processor, new()
        {
            Processors.Add(new T().With(this));
        }
        public void AddStartupProcessor<T>() where T : Processor, new()
        {
            StartupProcessors.Add(new T() { World = this });
        }

        public void AddProcessor<Q1>(UpdaterFunc<Q1> func) 
            where Q1 : Query, new()
        {
            AddProcessor(new DelegateProcessor<Q1>(func));
        }
        public void AddProcessor<Q1, Q2>(UpdaterFunc<Q1, Q2> func)
            where Q1 : Query, new()
            where Q2 : Query, new()
        {
            AddProcessor(new DelegateProcessor<Q1, Q2>(func));
        }
        public void AddProcessor<Q1, Q2, Q3>(UpdaterFunc<Q1, Q2, Q3> func)
            where Q1 : Query, new()
            where Q2 : Query, new()
            where Q3 : Query, new()
        {
            AddProcessor(new DelegateProcessor<Q1, Q2, Q3>(func));
        }
        public void AddProcessor<Q1,Q2,Q3,Q4>(UpdaterFunc<Q1,Q2,Q3,Q4> func) 
            where Q1 : Query, new()
            where Q2 : Query, new()
            where Q3 : Query, new()
            where Q4 : Query, new()
        {
            AddProcessor(new DelegateProcessor<Q1,Q2,Q3,Q4>(func));
        }



        public void RemoveProcessor(Processor p) => Processors.Remove(p);

    }
}