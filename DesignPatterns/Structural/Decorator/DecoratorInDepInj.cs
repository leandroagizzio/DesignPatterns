using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Decorator
{
    public interface IReportingService
    {
        void Report();
    }

    public class ReportingService : IReportingService
    {
        public void Report()
        {
            WriteLine("Here is your report!");
        }
    }

    public class ReportingServiceWithLogging : IReportingService
    {
        private IReportingService _decorated;

        public ReportingServiceWithLogging(IReportingService decorated)
        {
            _decorated = decorated;
        }

        public void Report()
        {
            WriteLine("Starting");
            _decorated.Report();
            WriteLine("Ending");
        }
    }

    public class DecoratorInDepInj : IRunner
    {
        public void Run()
        {
            var b = new ContainerBuilder();
            b.RegisterType<ReportingService>().Named<IReportingService>("reporting");
            b.RegisterDecorator<IReportingService>(
                (context, service) => new ReportingServiceWithLogging(service) , "reporting"
            );

            using var c = b.Build();
            var r = c.Resolve<IReportingService>();
            r.Report();
        }
    }
}
