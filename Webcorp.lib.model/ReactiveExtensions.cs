using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;
using ReactiveUI;

public static class ReactiveExtensions
{
    //
    // Résumé :
    //     RaiseAndSetIfChanged fully implements a Setter for a read-write property on a
    //     ReactiveObject, using CallerMemberName to raise the notification and the ref
    //     to the backing field to set the property.
    //
    // Paramètres :
    //   This:
    //     The ReactiveUI.ReactiveObject raising the notification.
    //
    //   backingField:
    //     A Reference to the backing field for this property.
    //
    //   newValue:
    //     The new value.
    //
    //   propertyName:
    //     The name of the property, usually automatically provided through the CallerMemberName
    //     attribute.
    //
    // Paramètres de type :
    //   TObj:
    //     The type of the This.
    //
    //   TRet:
    //     The type of the return value.
    //
    // Retourne :
    //     The newly set value, normally discarded.
    public static TRet SetAndRaise<TObj, TRet>(this TObj This, ref TRet backingField, TRet newValue, [CallerMemberName] string propertyName = null) where TObj : Entity
    {
        if (EqualityComparer<TRet>.Default.Equals(backingField, newValue))
        {
            return newValue;
        }

        if (This.EnableEvents) This.RaisePropertyChanging(propertyName);
        backingField = newValue;
        if (This.EnableEvents) This.RaisePropertyChanged(propertyName);
        return newValue;
    }

    
}

