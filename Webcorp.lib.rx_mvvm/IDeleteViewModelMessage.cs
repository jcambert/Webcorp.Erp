using Webcorp.Model;

namespace Webcorp.rx_mvvm
{
    public interface IDeleteViewModelMessage<T> where T : IEntity
    {
        T Model { get; set; }
    }
}