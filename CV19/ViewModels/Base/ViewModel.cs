using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CV19.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropetyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropetyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropetyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropetyName);
            return true;
        }

        public void Dispose()
        {
            Dispose(false);
        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || disposed) return;
            disposed = true;
            // Освобождение управляемых ресурсов
        }
    }
}
