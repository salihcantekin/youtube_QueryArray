

using QueryArray;

var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

QueryArray<int> q = new QueryArray<int>();
q.LoadFromArray(arr);

q.AddRange(new[] { 10, 11 });

//foreach (var item in q)
//{
//    Console.Write(item + " ");
//}

var l = new List<int>();

//foreach (var item in l)
//{

//}

while (q.Next())
{
    Console.Write(q.Current + " ");
}

q.RemoveAt(5);

Console.WriteLine("");

while (q.Previous())
{
    Console.Write(q.Current + " ");
}

//Console.WriteLine(q.Current);


Console.ReadLine();

