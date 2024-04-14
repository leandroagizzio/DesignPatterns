using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class Document
    {

    }

    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document d) {
            //
        }

        public void Print(Document d) {
            //
        }

        public void Scan(Document d) {
            //
        }
    }

    public class OldFashionedPrinter : IMachine
    {
        public void Fax(Document d) {
            throw new NotImplementedException();
        }

        public void Print(Document d) {
            //
        }

        public void Scan(Document d) {
            throw new NotImplementedException();
        }
    }

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface IFax
    {
        void Fax(Document d);
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d) {
            //
        }

        public void Scan(Document d) {
            //
        }
    }

    public interface IMultiFunctionDevice : IScanner, IPrinter
    {

    }

    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner) {
            this.printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
            this.scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(scanner));
        }
        public void Print(Document d) {
            printer.Print(d); //decorator pattern
        }

        public void Scan(Document d) {
            scanner.Scan(d);
        }
    }

    public class InterfaceSegregation : IRunner
    {
        public void Run() {
            
        }
    }
}
