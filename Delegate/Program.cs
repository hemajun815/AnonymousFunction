using System;
using System.Collections.Generic;
using System.Linq;

namespace Delegate
{
    class Program
    {
        static void Main()
        {
            Car car = new Car(10);
            Alerter alerter = new Alerter();
            car.notify += new Car.NotifyEventHandler(alerter.ShowNotify);
            car.Run(50);
            Console.ReadKey();
        }
    }
}
