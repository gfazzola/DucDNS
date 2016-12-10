using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTC.Nucleo.DucDNS
{
    // https://www.codeproject.com/articles/866547/publisher-subscriber-pattern-with-event-delegate-a

    #region publisher
    public class MessageArgument<T> : EventArgs
    {
        public T Message { get; private set; }
        public MessageArgument(T message)
        {
            Message = message;
        }
    }

    public interface IPublisher<T>
    {
        event EventHandler<MessageArgument<T>> DataPublisher;
        void PublishData(T data);
    }

    public class Publisher<T> : IPublisher<T>
    {
        //Defined datapublisher event
        public event EventHandler<MessageArgument<T>> DataPublisher;

        private void OnDataPublisher(MessageArgument<T> args)
        {
            var handler = DataPublisher;
            if (handler != null)
                handler(this, args);
        }


        public void PublishData(T data)
        {
            MessageArgument<T> message = (MessageArgument<T>)Activator.CreateInstance(typeof(MessageArgument<T>), new object[] { data });
            OnDataPublisher(message);
        }
    }

    #endregion

    #region suscriber
    public class Subscriber<T>
    {
        public IPublisher<T> Publisher { get; private set; }
        public Subscriber(IPublisher<T> publisher)
        {
            Publisher = publisher;
        }
    }
    #endregion
}
