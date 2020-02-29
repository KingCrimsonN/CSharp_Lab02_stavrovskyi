using System.ComponentModel;
using System.Windows;

namespace Sharp_lab02_stavrovskyi.Tools
{
        internal interface ILoaderOwner : INotifyPropertyChanged
        {
            Visibility LoaderVisibility { get; set; }
            bool IsControlEnabled { get; set; }
        }
    }
