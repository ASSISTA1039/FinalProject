using System;
using System.Collections;
using System.Collections.Generic;


public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}
//Generic constraints on binomial heaps
public class Heap<T> where T : IHeapItem<T>
{
    T[] items;

    //Current tree size
    int currentItemCount;

    /// Specify tree capacity
    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    //Add new element, sort up
    public void Add(T item)
    {
        //Inserted at the end first
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        //Sorting upwards
        SortUp(item);
        currentItemCount++;
    }

    //Remove root node, sort down
    public T RemoveFirst()
    {
        //Root Node
        T firstItem = items[0];
        currentItemCount--;
        //Transfer the last element to the first element
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    //Update tree reordering
    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    //Returns the current number of elements
    public int Count()
    {
        return currentItemCount;
    }

    //Presence or absence of elements
    public bool Contains(T item)
    {
        //Compare if incoming and found are the same
        return Equals(items[item.HeapIndex], item);
    }

    //Sort down to find child nodes
    void SortDown(T item)
    {
        while (true)
        {
            //Left leaf
            int childIndexLeft = item.HeapIndex * 2 + 1;
            //Right leaf
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;
            //If there are still child nodes Return if there are no child nodes
            if (childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentItemCount)
                {
                    //a.compareto(b) a<b=-1 a=b=0 a>b=1
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) > 0)
                    {
                        swapIndex = childIndexRight;//Get small nodes
                    }
                }
                //Compare with smaller nodes Return if child node is larger
                //a.compareto(b) a<b=-1 a=b=0 a>b=1
                if (item.CompareTo(items[swapIndex]) > 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    //Sort upwards to find the parent node
    void SortUp(T item)
    {
        //Get parent node
        int parentIndex = (int)((item.HeapIndex - 1) * 0.5f);
        while (true)
        {
            T parentItem = items[parentIndex];
            //Exchange if the current node is smaller
            //a.compareto(b) a<b=-1 a=b=0 a>b=1
            if (item.CompareTo(parentItem) < 0)
                Swap(item, parentItem);
            else
                break;
            //Continue to compare upwards
            parentIndex = (int)((item.HeapIndex - 1) * 0.5f);
        }
    }

    //Exchange of positions and data in the tree
    void Swap(T itemA, T itemB)
    {
        //Exchange of reference types (array elements), directly changing the object referred to
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        //Exchange of value types requires the introduction of intermediate variables
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}
