using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.Structural.Proxy
{
    public class ViewModel : IRunner
    {
        public void Run() {
            
        }

        // mvvm

        // model
        public class Person //: INotifyPropertyChanged, IDataErrorInfo
        {
            public string FirstName, LastName;

            //methods
        }

        // view = ui

        // viewmodel
        public class PersonViewModel : INotifyPropertyChanged {
            private readonly Person _person;

            public PersonViewModel(Person person) {
                _person = person;
            }

            public string FirstName {
                get => _person.FirstName;
                set {
                    if (_person.FirstName == value) return;
                    _person.FirstName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(FullName));
                }
            }

            public string LastName {
                get => _person.LastName;
                set {
                    if (_person.LastName == value) return;
                    _person.LastName = value;
                    OnPropertyChanged();
                }
            }

            public string FullName {
                get => $"{FirstName} {LastName}".Trim();
                set {
                    if (value == null) {
                        FirstName = LastName = null;
                        return;
                    }
                    var items = value.Split();
                    if (items.Length > 0)
                        FirstName = items[0];
                    if (items.Length > 1)
                        LastName = items[1];
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
