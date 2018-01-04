using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp.Model
{
    public class CheckBoxWrapper:NotifyModelBase
    {
        public NotifyModelBase WrappedModel { get; private set; }

        string displayName;
        public string DisplayName
        {
            get 
            { 
                return displayName; 
            }
            set 
            { 
                this.displayName = value; 
                OnPropertyChanged(); 
            }
        }
        bool isChecked;
        public bool IsChecked
        {
            get 
            {
                return isChecked;
            }
            set 
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        public CheckBoxWrapper(NotifyModelBase modelToWrap)
        {
            this.WrappedModel = modelToWrap;

            if (WrappedModel is ICheckBoxWrappable)
            {
                this.DisplayName = ((ICheckBoxWrappable)WrappedModel).ExposedDisplayName;
            }
        }
    }
}
