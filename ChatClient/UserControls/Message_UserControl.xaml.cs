using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClient.UserControls
{
    /// <summary>
    /// Lógica de interacción para Message_UserControl.xaml
    /// </summary>
    public partial class Message_UserControl : UserControl
    {
        public Message_UserControl()
        {
            InitializeComponent();
        }

        public Message_UserControl(string username, string message)
        {
            InitializeComponent(); 

            txt_Message.Text = message;
            txt_Username.Text = username;
        }
    }
}
