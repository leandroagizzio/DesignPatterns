using Autofac;
using Autofac.Features.Metadata;
using System;
using static System.Console;

namespace DesignPatterns.Structural.Adapter
{
    public interface ICommand
    {
        void Execute();
    }
    public class SaveCommand : ICommand
    {
        public void Execute()
        {
            WriteLine("Saving a file.");
        }
    }
    public class OpenCommand : ICommand
    {
        public void Execute()
        {
            WriteLine("Opening a file.");
        }
    }
    public class Button
    {
        private ICommand _command;
        private string _name;

        public Button(ICommand command, string name)
        {
            _command = command;
            _name = name;
        }
        public void Click()
        {
            _command.Execute();
        }
        public void PrintMe()
        {
            WriteLine($"I am a button called {_name}");
        }
    }

    public class Editor
    {
        private IEnumerable<Button> _buttons;
        public IEnumerable<Button> Buttons => _buttons;
        //{
        //    get { return _buttons; }
        //}

        public Editor(IEnumerable<Button> buttons)
        {
            _buttons = buttons;
        }
        public void ClickAll()
        {
            foreach (var button in _buttons)
                button.Click();
        }
    }

    public class AdapterInDepInj : IRunner
    {
        public void Run()
        {
            var b = new ContainerBuilder();
            b.RegisterType<SaveCommand>().As<ICommand>().WithMetadata("Name", "Save");
            b.RegisterType<OpenCommand>().As<ICommand>().WithMetadata("Name", "Open");
            //b.RegisterType<Button>();
            //b.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd));
            b.RegisterAdapter<Meta<ICommand>, Button>(cmd =>
                new Button(cmd.Value, (string)cmd.Metadata["Name"])
            );
            b.RegisterType<Editor>();

            using (var c = b.Build())
            {
                var editor = c.Resolve<Editor>();
                //editor.ClickAll();

                foreach (var btn in editor.Buttons)
                {
                    btn.PrintMe();
                }
            }
            
        }
    }
}
