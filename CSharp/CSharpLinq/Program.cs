// See https://aka.ms/new-console-template for more information
List<int> listA = new List<int> { 1, 2, 3, 5, 9 };
List<int> listB = new List<int> { 4, 3, 9 };
var listC = listA.Intersect(listB).ToList();
listC.ForEach(i => Console.WriteLine(i));

Console.WriteLine("------------------------------------");

listC =listA.Except(listB).ToList();
listC.ForEach(i => Console.WriteLine(i));

Console.WriteLine("------------------------------------");
listC=listB.Except(listA).ToList();
listC.ForEach(i => Console.WriteLine(i));
Console.WriteLine("----------------Union--------------------");
List<int> unionList = listA.Union(listB).ToList<int>();//剔除重复项 
unionList.ForEach(i => Console.WriteLine(i));
Console.WriteLine("-----------------Concat-------------------");
List<int> concatList = listA.Concat(listB).ToList<int>();//保留重复项
concatList.ForEach(i => Console.WriteLine(i));

Console.ReadLine();
