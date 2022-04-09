using System;

namespace CSharpIn
{
    class Program
    {
        static void Main(string[] args)
        {
            IContravariant<Object> iobj = new Sample<Object>();
            IContravariant<String> istr = new Sample<String>();
            istr = iobj;
            Console.WriteLine("Hello World!");
            Method(5); // OK, temporary variable created.
            //Method(5L); // CS1503: no implicit conversion from long to int
            short s = 0;
            Method(s); // OK, temporary int created with the value 0
            //Method(in s); // CS1503: cannot convert from in short to in int
            int i = 42;
            Method(i); // passed by readonly reference
            Method(in i); // passed by readonly reference, explicitly using `in`
        }
        static void Method(in int argument)
        {
            // implementation removed
        }

        
    }
    #region 逆变泛型接口
    interface IContravariant<in A>
    {

    }
    interface IExtContravariant<in A> : IContravariant<A>
    {

    }
    class Sample<A> : IContravariant<A>
    {

    }
    #endregion

    #region 逆变泛型委托
    // Contravariant delegate.
    //public delegate void DContravariant<in A>(A argument);

    //// Methods that match the delegate signature.
    //public static void SampleControl(Control control)
    //{ }
    //public static void SampleButton(Button button)
    //{ }

    //public void Test()
    //{

    //    // Instantiating the delegates with the methods.
    //    DContravariant<Control> dControl = SampleControl;
    //    DContravariant<Button> dButton = SampleButton;

    //    // You can assign dControl to dButton
    //    // because the DContravariant delegate is contravariant.
    //    dButton = dControl;

    //    // Invoke the delegate.
    //    dButton(new Button());
    //}
    #endregion
}
