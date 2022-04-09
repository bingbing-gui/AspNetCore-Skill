using System;
using System.Collections.Generic;

namespace CSharpOverrideNew
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseClass bc = new BaseClass();
            DerivedClass dc = new DerivedClass();
            BaseClass bcdc = new DerivedClass();
            bc.Method1();//base-Method 1
            bc.Method2();//Base - Method2
            dc.Method1();//Base - Method1
            dc.Method2();//Derived - Method2
            bcdc.Method1();
            bcdc.Method2();
            TestCars1();
            Console.WriteLine("----------");


            Car c = new BMWMinnivan();
            c.DescribeCar();
        }
        public static void TestCars1()
        {
            Console.WriteLine("\nTestCars1");
            Console.WriteLine("----------");
            Car car1 = new Car();
            car1.DescribeCar();
            Console.WriteLine("----------");
            ConvertibleCar car2 = new ConvertibleCar();
            car2.DescribeCar();
            Console.WriteLine("----------");
            Minivan car3 = new Minivan();
            car3.DescribeCar();
            System.Console.WriteLine("----------");
        }
        public static void TestCars2()
        {
            System.Console.WriteLine("\nTestCars2");
            System.Console.WriteLine("----------");
            var cars = new List<Car> { new Car(), new ConvertibleCar(), new Minivan() };
            foreach (var car in cars)
            {
                car.DescribeCar();
                System.Console.WriteLine("----------");
            }
        }
        // Output:  
        // TestCars2  
        // ----------  
        // Four wheels and an engine.  
        // Standard transportation.  
        // ----------  
        // Four wheels and an engine.  
        // Standard transportation.  
        // ----------  
        // Four wheels and an engine.  
        // Carries seven people.  
        // ----------  
        public static void TestCars3()
        {
            System.Console.WriteLine("\nTestCars3");
            System.Console.WriteLine("----------");
            ConvertibleCar car2 = new ConvertibleCar();
            Minivan car3 = new Minivan();
            car2.ShowDetails();
            car3.ShowDetails();
        }
        // Output:  
        // TestCars3  
        // ----------  
        // A roof that opens up.  
        // Carries seven people.  
        public static void TestCars4()
        {
            Console.WriteLine("\nTestCars4");
            Console.WriteLine("----------");
            Car car2 = new ConvertibleCar();
            Car car3 = new Minivan();
            car2.ShowDetails();
            car3.ShowDetails();
        }
        // Output:  
        // TestCars4  
        // ----------  
        // Standard transportation.  
        // Carries seven people. 
    }
    #region
    /// <summary>
    /// 基类
    /// </summary>
    //class BaseClass
    //{
    //    public void Method1()
    //    {
    //        Console.WriteLine("Base - Method1");
    //    }
    //    public void Method2()
    //    {
    //        Console.WriteLine("Base - Method2");
    //    }
    //}
    ///// <summary>
    ///// 继承类
    ///// </summary>
    //class DerivedClass : BaseClass
    //{
    //    public new void Method2()
    //    {
    //        Console.WriteLine("Derived - Method2");
    //    }
    //}
    #endregion
    class BaseClass
    {
        public virtual void Method1()
        {
            Console.WriteLine("Base - Method1");
        }
        public void Method2()
        {
            Console.WriteLine("Base - Method2");
        }
    }
    /// <summary>
    /// 继承类
    /// </summary>
    class DerivedClass : BaseClass
    {
        /// <summary>
        /// 扩展基类方法
        /// </summary>
        public override void Method1()
        {
            Console.WriteLine("DerivedClass - Method1");
        }
        /// <summary>
        ///方法名称和基类相同时,用new 特殊标记,否则编译器会给警告
        /// </summary>
        public new void Method2()
        {
            Console.WriteLine("Derived - Method2");
        }
    }
    class Car
    {
        public void DescribeCar()
        {
            Console.WriteLine("Four wheels and engine.");
            ShowDetails();
        }
        public virtual void ShowDetails()
        {
            Console.WriteLine("Standard transportation");
        }
    }
    class ConvertibleCar : Car
    {
        public new void ShowDetails()
        {
            Console.WriteLine("A roof that opens up.");
        }
    }
    class Minivan : Car
    {
        public override void ShowDetails()
        {
            System.Console.WriteLine("Carries seven people.");
        }
    }

    class BMWMinnivan : Minivan
    {
        public override void ShowDetails()
        {
            System.Console.WriteLine("BMWMinnivan Carries seven people.");
        }
    }
}

