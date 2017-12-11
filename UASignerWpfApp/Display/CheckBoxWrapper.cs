using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
namespace UASigner.WpfApp.Display
{
    public class CheckBoxWrapper : INotifyPropertyChanged
    {
        public object WrappedObject { get; private set; }
        private bool selected;

        public CheckBoxWrapper()
        { 
        }
        public CheckBoxWrapper(object wrappedObject)
        {
            this.WrappedObject = wrappedObject;
        }
        public bool Selected 
        {
            get { return selected; }
            set 
            {
                if (selected != value)
                {
                    selected = value;
                    OnPropertyChanged();
                }
            }
        }
        public string DispName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
