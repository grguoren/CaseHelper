using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace CaseHelp_Core.Helper
{
    public class MessageHelper<T>
    {
        private IMessageFormatter formatter = new System.Messaging.BinaryMessageFormatter();
        private MessageQueue queue;

        /// <summary>
        /// 队列名
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 队列说明
        /// </summary>
        public string Label { get; set; }

        public string ManchineName { get; set; }


        public MessageHelper()
        {
            this.QueueName = ".\\private$\\WebCacheRefresh";
            CreateMessageQueue();
        }

        public MessageHelper(string path)
        {
            this.QueueName = path;
            CreateMessageQueue();
        }

        private void CreateMessageQueue()
        {
            if (MessageQueue.Exists(this.QueueName))
            {
                queue = new MessageQueue(this.QueueName);
            }
            else
            {
                queue = MessageQueue.Create(this.QueueName, false);
                if (!string.IsNullOrEmpty(this.Label))
                    queue.Label = this.Label;
                queue.Path = this.QueueName;
            }
            queue.SetPermissions("Administrators", MessageQueueAccessRights.FullControl);
        }

        private System.Messaging.Message CreateMessage(T msg)
        {
            System.Messaging.Message message = new System.Messaging.Message();
            message.Body = msg;
            message.Formatter = this.formatter;
            return message;
        }

        public bool SendMessage(T msg)
        {
            if (!MessageQueue.Exists(this.QueueName))
            {
                CreateMessageQueue();
                return false;
            }
            else
            {
                System.Messaging.Message message = CreateMessage(msg);
                queue.Send(message);
                return true;
            }
        }

        public T GetMessageText()
        {
            System.Messaging.Message message = ReceiveMessage(this.QueueName);
            if (message != null)
                return (T)message.Body;
            return default(T);
        }

        private System.Messaging.Message ReceiveMessage(string queuePath)
        {
            if (!MessageQueue.Exists(queuePath))
            {
                CreateMessageQueue();
                return null;
            }

            System.Messaging.Message message = this.queue.Receive();
            return message;
        }
    }
}