using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class ListExtension
{   
    /// <summary>
    /// возвращает последний элемент
    /// </summary>
    public static T Pop<T>(this List<T> list) {
        T item = list[list.Count-1];
        list.Remove(item);
        return item;
    }
    /// <summary>
    /// возвращает первый элемент
    /// </summary>
    
    public static T Shift<T>(this List<T> list){
        T item = list[0];
        list.Remove(item);
        return item;
    }
}
