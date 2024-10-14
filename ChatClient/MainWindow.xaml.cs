using System.ServiceModel;
using System.Windows;
using System.Windows.Media;
using ChatClient.ChatService;
using ChatClient.UserControls;


namespace ChatClient
{
    public partial class MainWindow : Window, ISessionServiceCallback, IMessageServiceCallback
    {

        private string username;

        private SessionServiceClient sessionClient;
        private MessageServiceClient messageClient; 

        public MainWindow()
        {
            InitializeComponent();

            InstanceContext context = new InstanceContext(this);
            sessionClient = new SessionServiceClient(context);
            messageClient = new MessageServiceClient(context);
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            string message = txt_Message.Text;

            if (!string.IsNullOrWhiteSpace(message))
            {
                messageClient.SendMessage(message, username);

                var control = new Message_UserControl();
                control.txt_Message.Text = message;
                control.txt_Username.Text = "Yo";
                control.HorizontalAlignment = HorizontalAlignment.Right;
                List_Message.Children.Add(control);
                control.Border_main.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#52C9DC"));
                ScrollToBottom();
            }
            else
            {
                MessageBox.Show("El mensaje debe llevar un contenido", "Mensaje invalido");
            }
            
        }

        private void ScrollToBottom()
        {
            Scroll_Messages.ScrollToBottom();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            username = txt_Username.Text;

            var loginCorrect = sessionClient.LogIn(txt_Username.Text);

            if (loginCorrect)
            {
                Grid_Login.Visibility = Visibility.Collapsed;
                Grid_Messages.Visibility = Visibility.Visible;

                messageClient.StartMessaging(username);
            }
            else
            {
                MessageBox.Show("Puede que ya haya alguien con tu username en el chat", "Inicio de sesión fallido"); 
            }
            
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {

            Grid_Login.Visibility = Visibility.Visible;
            Grid_Messages.Visibility = Visibility.Collapsed;
            txt_Username.Text = "";
            List_Message.Children.Clear(); 

            sessionClient.LogOut(username);
            messageClient.StopMessaging(username);

            //sessionClient.Close();
            //messageClient.Close();
        }

        //Callbacks
        public void JoinToChat(string username)
        {
            var control = new Message_UserControl(); 
            control.txt_Message.Text = $"{username} se ha unido al chat";
            control.txt_Username.Visibility = Visibility.Collapsed;
            control.HorizontalAlignment = HorizontalAlignment.Center;
            control.Border_main.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33BBFF"));


            List_Message.Children.Add(control);
        }

        public void LeaveTheChat(string username)
        {
            var control = new Message_UserControl();
            control.txt_Message.Text = $"{username} ha dejado el chat";
            control.txt_Username.Visibility = Visibility.Collapsed;
            control.HorizontalAlignment = HorizontalAlignment.Center;
            control.Border_main.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33BBFF"));


            List_Message.Children.Add(control);
        }

        public void ReciveMessage(string message, string username)
        {
            var control = new Message_UserControl();
            control.txt_Message.Text = message;
            control.txt_Username.Text = username;
            control.HorizontalAlignment = HorizontalAlignment.Left;
            List_Message.Children.Add(control);
            ScrollToBottom();
        }
    }
}
