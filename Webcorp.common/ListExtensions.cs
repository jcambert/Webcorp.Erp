using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public static class ListExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (T item in enumeration)
        {
            action(item);
        }
    }
    public static async Task ForEachAsync<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        await Task.Run( () =>
        {
            enumeration.ForEach(action);
            
        });
        
    }
}

