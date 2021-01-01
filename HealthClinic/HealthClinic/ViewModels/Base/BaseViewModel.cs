using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using AsyncAwaitBestPractices;

namespace HealthClinic
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        readonly WeakEventManager _notifyPropertyChangedEventManager = new WeakEventManager();

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => _notifyPropertyChangedEventManager.AddEventHandler(value);
            remove => _notifyPropertyChangedEventManager.RemoveEventHandler(value);
        }

        protected void SetProperty<T>(ref T backingStore, T value, Action? onChanged = null, [CallerMemberName] string propertyname = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyname);
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            _notifyPropertyChangedEventManager?.RaiseEvent(this, new PropertyChangedEventArgs(propertyName), nameof(INotifyPropertyChanged.PropertyChanged));
    }
}