using Autofac;

public class ContainerConfig
{
    public static IContainer ConfigureController()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<Calculator>().As<ICalc>();
        builder.RegisterType<EquationParser>().As<IEquationParser>();


        return builder.Build();
    }

    public static IContainer ConfigureOperator()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<Multiply>().Keyed<ICalcOperator>("X");
        builder.RegisterType<Divide>().Keyed<ICalcOperator>("/");
        builder.RegisterType<Add>().Keyed<ICalcOperator>("+");
        builder.RegisterType<Substract>().Keyed<ICalcOperator>("-");

        return builder.Build();
    }

}
