using Ninject;
using Prism.Regions;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Webcorp.Controller;
using Webcorp.Model;

namespace Webcorp.rx_mvvm
{

    public interface IEntityViewModel : IEntityViewModel<IEntity>
    {

    }

    public interface IEntityViewModel<T> : ICloseable, IViewModel, IStandardCommand where T : IEntity
    {

        T Model { get; set; }

        IEntityController<T> Controller { get; set; }

    }

    public interface IViewModel : IInitializable, IRegionMemberLifetime,INotifyPropertyChanged,IPropertyChanged,IShouldDisposable,IDisposable
    {
        
    }

    public interface IShouldDisposable
    {
        void ShouldDispose(IDisposable disposable);
    }

    public interface IPropertyChanged
    {
        void OnPropertyChanged([CallerMemberName]  string propertyName="");
    }

   
}