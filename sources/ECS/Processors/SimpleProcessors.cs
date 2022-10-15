namespace ECSharp;

public abstract class SimpleProcessor : Processor
{
    public delegate void UpdateDelegate<T1>(World w, ref T1 arg1)
            where T1 : struct;
    public delegate void UpdateDelegate<T1, T2>(World w, ref T1 arg1, ref T2 arg2)
        where T1 : struct
        where T2 : struct;
    public delegate void UpdateDelegate<T1, T2, T3>(World w, ref T1 arg1, ref T2 arg2, ref T3 arg3)
        where T1 : struct
        where T2 : struct
        where T3 : struct;
    public delegate void UpdateDelegate<T1, T2, T3, T4>(World w, ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct;
}
public sealed class SimpleProcessor<T> : SimpleProcessor
    where T : struct
{
    readonly ArchetypeID aid = new(typeof(T));
    public UpdateDelegate<T>? Updater;

    public override void Update()
    {
        if (Updater != null)
        {
            foreach (var arch in World.QueryArchetypes(aid))
            {
                var array = arch.GetComponentSpan<T>();
                for (int i = 0; i < array.Length; i++)
                {
                    Updater(World, ref array[i]);
                }
            }
        }
    }
}

public sealed class SimpleProcessor<T1, T2> : SimpleProcessor
    where T1 : struct
    where T2 : struct
{
    readonly ArchetypeID aid = new(typeof(T1), typeof(T2));
    public UpdateDelegate<T1, T2>? Updater;

    public override void Update()
    {
        if (Updater != null)
        {
            foreach (var arch in World.QueryArchetypes(aid))
            {
                var array1 = arch.GetComponentSpan<T1>();
                var array2 = arch.GetComponentSpan<T2>();
                for (int i = 0; i < array1.Length; i++)
                {
                    Updater(World, ref array1[i], ref array2[i]);
                }
            }
        }
    }
}
public sealed class SimpleProcessor<T1, T2, T3> : SimpleProcessor
    where T1 : struct
    where T2 : struct
    where T3 : struct
{
    readonly ArchetypeID aid = new(typeof(T1), typeof(T2), typeof(T3));
    public UpdateDelegate<T1, T2, T3>? Updater;

    public override void Update()
    {
        if (Updater != null)
        {
            foreach (var arch in World.QueryArchetypes(aid))
            {
                var array1 = arch.GetComponentSpan<T1>();
                var array2 = arch.GetComponentSpan<T2>();
                var array3 = arch.GetComponentSpan<T3>();
                for (int i = 0; i < array1.Length; i++)
                {
                    Updater(World, ref array1[i], ref array2[i], ref array3[i]);
                }
            }
        }
    }
}

public sealed class SimpleProcessor<T1, T2, T3, T4> : SimpleProcessor
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
{
    readonly ArchetypeID aid = new(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
    public UpdateDelegate<T1, T2, T3, T4>? Updater;

    public override void Update()
    {
        if (Updater != null)
        {
            foreach (var arch in World.QueryArchetypes(aid))
            {
                var array1 = arch.GetComponentSpan<T1>();
                var array2 = arch.GetComponentSpan<T2>();
                var array3 = arch.GetComponentSpan<T3>();
                var array4 = arch.GetComponentSpan<T4>();
                for (int i = 0; i < array1.Length; i++)
                {
                    Updater(World, ref array1[i], ref array2[i], ref array3[i], ref array4[i]);
                }
            }
        }
    }
}
