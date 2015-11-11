using Ninject;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Webcorp.Controller;
using Webcorp.Model;

namespace Webcorp.rx_mvvm
{

    public interface IEntityViewModel : IEntityViewModel<IEntity>
    {

    }

    public interface IEntityViewModel<T> : INotifyPropertyChanged, IDisposable, ICloseable, IInitializable, IStandardCommand where T : IEntity
    {

        T Model { get; set; }

        IEntityController<T> Controller { get; set; }

        void ShouldDispose(IDisposable disposable);

        void OnPropertyChanged(string propertyName);
    }


}