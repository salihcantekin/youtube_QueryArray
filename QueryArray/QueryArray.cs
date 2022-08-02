using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryArray;
public class QueryArray<T> : IEnumerable<T>, IEnumerator<T>
{
    private T[] arr;

    private int arrSize;
    private const int defaultCapacity = 1_000;

    private int currentIndex;
    private bool eof;
    private bool bof;
    private int capacity => arr.Length;

    public QueryArray(int capacity)
    {
        arr = new T[capacity];
        //arrSize = capacity;
        Reset();
    }

    public QueryArray() : this(defaultCapacity)
    {

    }

    public T Current
    {
        get
        {
            if (currentIndex == -1)
                currentIndex = 0;
            else if (currentIndex == arrSize)
                currentIndex = arrSize - 1;

            return arr[currentIndex];
        }
    }

    object IEnumerator.Current => Current;

    public T this[int index] => arr[index];


    public int Add(T item)
    {
        if (arrSize == capacity)
        {
            ReSize(arrSize * 2);
        }

        arr[arrSize] = item;
        arrSize++;

        return arrSize;
    }

    public int AddRange(T[] items)
    {
        if (arrSize + items.Length >= capacity)
        {
            ReSize(arrSize + items.Length);
        }

        for (int i = 0; i < items.Length; i++)
        {
            arr[arrSize] = items[i];
            arrSize++;
        }

        return arrSize;
    }

    public void RemoveAt(int index)
    {
        // { 1, 2, 3, 4, 0, 0, 0, }

        // { 2, 3, 4, 5, 6, 7, 8, 9, 0 }


        // { 2, 3, 4, 5, 6, 7, 8, 9, 9 }

        for (int i = index; i < arrSize - 1; i++)
        {
            arr[i] = arr[i + 1];
        }

        arrSize--;
    }



    private void ReSize(int newSize)
    {
        Array.Resize(ref arr, newSize);
    }


    public void Reset()
    {
        SetIndex(-1);
    }

    private void SetCurrent(int increase)
    {
        currentIndex += increase;
        eof = currentIndex > 0 && currentIndex >= arrSize - 1;
        bof = currentIndex == 0;
    }

    private void SetIndex(int index)
    {
        currentIndex = 0;
        SetCurrent(index);
    }

    public bool Next()
    {
        if (!eof)
        {
            SetCurrent(1);
            return true;
        }

        GotoEnd();
        return false;
    }

    public bool Previous()
    {
        if (!bof)
        {
            SetCurrent(-1);
            return true;
        }

        GotoStart();
        return false;
    }


    public void GotoStart()
    {
        Reset();
    }

    public void GotoEnd()
    {
        SetIndex(arrSize);
    }

    public void LoadFromArray(T[] newArr)
    {
        Load(newArr);
    }

    private void Load(T[] newArr)
    {
        ArgumentNullException.ThrowIfNull(newArr);
        arr = newArr;
        arrSize = newArr.Length;
        Reset();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        return Next();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(arr);
    }

    public static implicit operator QueryArray<T>(T[] newArr)
    {
        var q = new QueryArray<T>();
        q.LoadFromArray(newArr);
        return q;
    }

}
