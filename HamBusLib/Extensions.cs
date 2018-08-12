using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public static class Extensions
    {
        public static IDisposable Inspect<T>(this IObservable<T> self, string name)
        {
            var rc = self.Subscribe(
                x => Console.WriteLine($"{name} has generated value {x}"),
                ex => Console.WriteLine($"{name} has generated exception {ex}"),
                () => Console.WriteLine($"{name} has completed"));
            return rc;
        }
        public static IObserver<T> OnNext<T>(this IObserver<T> self, params T[] args)
        {
            foreach (var arg in args)
                self.OnNext(arg);
            return self;
        }
    }
}
