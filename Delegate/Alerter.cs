using System;
using System.Collections.Generic;
using System.Linq;

namespace Delegate
{
    public class Alerter
    {
        public void ShowNotify(NotifyEventArgs e)
        {
            switch (e.Type)
            {
                case NotifyEventArgs.NotifyType.NotifyTypeWran: this.Warn(e.Message); break;
                case NotifyEventArgs.NotifyType.NotifyTypeAlert: this.Alert(e.Message); break;
                case NotifyEventArgs.NotifyType.NotifyTypeMassage: this.Message(e.Message); break;
            }
        }
        private void Alert(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Alert:" + message);
            Console.ResetColor();
        }
        private void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warn:" + message);
            Console.ResetColor();
        }
        private void Message(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Message:" + message);
            Console.ResetColor();
        }
    }
}
