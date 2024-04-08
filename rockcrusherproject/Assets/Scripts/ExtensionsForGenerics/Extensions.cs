using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;


public static class Extensions
{
    #region ListsExtensions
    public static void CopyThisListElementsToListInParameter<T>(this List<T> source, List<T> otherList)
    {
        foreach (T element in source)
        {
            T newElement = element;
            otherList.Add(newElement);
        }
    }

    public static void CopyListFromParameterElementsToThisList<T>(this List<T> source, List<T> otherList)
    {
        foreach (T element in otherList)
        {
            T newElement = element;
            source.Add(newElement);
        }
    }

    public static void RemoveFromThisListElementsFromParameterListByTheirValue<T>(this List<T> source, List<T> otherList)
    {
        foreach(T otherListElement in otherList)
        {
            if(source.Contains(otherListElement))
            {
                source.Remove(otherListElement);
            }
        }
    }
    #endregion
}
