using Autofac;

public class AutofacContainerConfig: Module
{

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EquationParser>().As<IEquationParser>();
        builder.RegisterType<CalcFlowManagerSimple>().As<ICalcFlowManager>();
        builder.RegisterType<Multiply>().Keyed<ICalcOperator>("X");
        builder.RegisterType<Divide>().Keyed<ICalcOperator>("/");
        builder.RegisterType<Add>().Keyed<ICalcOperator>("+");
        builder.RegisterType<Substract>().Keyed<ICalcOperator>("-");
    }
}
