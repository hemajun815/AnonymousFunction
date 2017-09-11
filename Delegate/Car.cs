using System;

namespace Delegate
{
    class Car
    {
        private int petrol;
        public delegate void NotifyEventHandler(NotifyEventArgs e);
        public event NotifyEventHandler notify;
        public int Petrol
        {
            get { return petrol; }
            set 
            {
                if (value < 0) petrol = 0;
                else petrol = value;
                if (petrol <= 5 && petrol > 0) 
                {
                    const string message = "Petrol is less than 5";
                    if (null != notify) this.notify(new NotifyEventArgs(NotifyEventArgs.NotifyType.NotifyTypeWran, message));
                    else Console.WriteLine("Default:" + message);
                }
                else if (petrol <= 0)
                {
                    const string message = "Petrol is used up";
                    if (null != notify) this.notify(new NotifyEventArgs(NotifyEventArgs.NotifyType.NotifyTypeAlert, message));
                    else Console.WriteLine("Default:" + message);
                }
            }
        }
        public Car(int petrol)
        {
            this.Petrol = petrol;
        }
        public void Run(int speed)
        {
            int distance = 0;
            string message = "Car is running at a speed of " + speed;
            if (null != notify) this.notify(new NotifyEventArgs(NotifyEventArgs.NotifyType.NotifyTypeMassage, message));
            while (this.Petrol > 0)
            {
                System.Threading.Thread.Sleep(500);
                distance += speed;
                Console.WriteLine("Car is running...Distance is " + distance);
                this.Petrol--;
            }
        }
    }
}
