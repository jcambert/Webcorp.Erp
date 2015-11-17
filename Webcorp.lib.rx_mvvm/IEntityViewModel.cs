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

    public interface IEntityViewModel<T> :  ICloseable,IViewModel<T> where T : IEntity
    {

        

        IEntityController<T> Controller { get; set; }

        
    }

    public interface IViewModel:IViewModel<object>
    {
        
    }

    public interface IViewModel<T> : IInitializable, IStandardCommand, IRegionMemberLifetime,INotifyPropertyChanged,IPropertyChanged,IShouldDisposable,IDisposable
    {
        T Model { get; set; }
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