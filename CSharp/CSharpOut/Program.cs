using System;

namespace CSharpOut
{
    class Program
    {
        static void Main(string[] args)
        {
            ICovariant<Object> iobj = new Sample<Object>();
            ICovariant<String> istr = new Sample<String>();
            iobj = istr;
        }
    }

    interface ICovariant<out R>
    {
    }
    interface IExtCovariant<out R> : ICovariant<R>
    {

    }
    class Sample<R> : ICovariant<R>
    {

    }

}
