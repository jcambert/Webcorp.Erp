using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public static class ReactiveExtensions
    {

        /*  public static PropertyInfo GetPropertyInfo<TSource, TValue>(this Expression<Func<TSource, TValue>> property)
          {
              if (property == null)
              {
                  throw new ArgumentNullException("property");
              }

              var body = property.Body as MemberExpression;
              if (body == null)
              {
                  throw new ArgumentException("Expression is not a property", "property");
              }

              var propertyInfo = body.Member as PropertyInfo;
              if (propertyInfo == null)
              {
                  throw new ArgumentException("Expression is not a property", "property");
              }

              return propertyInfo;
          }

          public static IObservable<TProperty> OnPropertyChanges<T, TProperty>(this T source, Expression<Func<T, TProperty>> property)
       where T : INotifyPropertyChanged
          {
              return Observable.Create<TProperty>(o =>
              {
                  var propertyName = property.GetPropertyInfo().Name;
                  var propertySelector = property.Compile();

                  return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                                  handler => handler.Invoke,
                                  h => source.PropertyChanged += h,
                                  h => source.PropertyChanged -= h)
                              .Where(e => e.EventArgs.PropertyName == propertyName)
                              .Select(e => propertySelector(source))
                              .Subscribe(o);
              });
          }*/
    }
}
