using System;
using System.Collections.Generic;
using System.Linq;

namespace Delegate
{
    public class NotifyEventArgs : EventArgs
    {
        public enum NotifyType
        {
            NotifyTypeMassage,
            NotifyTypeWran,
            NotifyTypeAlert
        }
        public NotifyType Type { set; get; }
        public string Message { set; get; }
        public NotifyEventArgs(NotifyType type, string message)
        {
            this.Type = type;
            this.Message = message;
        }
    }
}
