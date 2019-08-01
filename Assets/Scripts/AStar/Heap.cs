using System.Collections;
using System;
using UnityEngine;

/// <summary>
/// Author: Sebastian Lague @ https://www.youtube.com/watch?v=3Dw5d7PlcTM&index=4&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW
/// Modified by: Robin Chabouk
/// The heap used for storing nodes.
/// </summary>
/// <typeparam name="T">Takes type T, could be used for more than just Nodes.</typeparam>
public class Heap<T> where T : IHeapItem<T> {

    /// <summary>
    /// An array of T, in this case it will be of Nodes.
    /// </summary>
    T[] items;

    /// <summary>
    /// The current size of the heap.
    /// </summary>
    int currentItemCount;

    /// <summary>
    /// Construct the heap.
    /// </summary>
    /// <param name="maxHeapSize">The maximum size of the heap.</param>
    public Heap(int maxHeapSize) {
        items = new T[maxHeapSize];
    }

    /// <summary>
    /// Add an item to the heap.
    /// </summary>
    /// <param name="item">The item to be added to the heap.</param>
    public void Add(T item) {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    /// <summary>
    /// Remove the first item in the heap.
    /// </summary>
    /// <returns>The first item of the heap.</returns>
    public T RemoveFirst() {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    /// <summary>
    /// Update the position of an item if its priority is increased.
    /// </summary>
    /// <param name="item">The item whose position is to be updated.</param>
    public void UpdateItem(T item) {
        SortUp(item);
    }

    /// <summary>
    /// The number of items currently in the heap.
    /// </summary>
    public int Count {
        get {
            return currentItemCount;
        }
    }

    /// <summary>
    /// return whether the heap contains the given item.
    /// </summary>
    /// <param name="item">The item to check for in the heap.</param>
    /// <returns></returns>
    public bool Contains(T item) {
        return Equals(items[item.HeapIndex], item);
    }

    /// <summary>
    /// swap an item with one of its children if it has a lower priority than its children.
    /// </summary>
    /// <param name="item">The item to compare to its children.</param>
    void SortDown(T item) {
        while (true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;

                if (childIndexRight < currentItemCount)
                {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0)
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

    /// <summary>
    /// Swap an item with its parent if it has a higher priority than its parent.
    /// </summary>
    /// <param name="item">The item to compare to its parent.</param>
    void SortUp(T item) {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }

            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    /// <summary>
    /// Swap the position of two items in the heap.
    /// </summary>
    /// <param name="itemA">Item A to be swapped.</param>
    /// <param name="itemB">Item B to be swapped.</param>
    void Swap(T itemA, T itemB) {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

/// <summary>
/// Each item in the heap can keep track of its index in the heap.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IHeapItem<T> : IComparable<T> {
    int HeapIndex {
        get;
        set;
    }
}
