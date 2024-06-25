using DevExpress.Mvvm;

namespace MVVMDemo.ViewModelsInteraction {
    public class MessengerViewModel {
        int messageIndex;
        public void SendMessage() {
            Messenger.Default.Send(new Message("Message " + messageIndex));
            messageIndex++;
        }
    }
    public class ReceiverViewModel {
        public virtual string Status { get; protected set; }
        protected ReceiverViewModel() {
            Messenger.Default.Register<Message>(this, OnMessage);
        }
        void OnMessage(Message message) {
            Status = "Received: " + message.Content;
        }
    }
    public class Message {
        public Message(string content) {
            Content = content;
        }
        public string Content { get; private set; }
    }
}
