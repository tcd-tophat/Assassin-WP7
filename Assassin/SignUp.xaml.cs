using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Tophat;

namespace Assassin
{
    public partial class SignUp : PhoneApplicationPage
    {
        public SignUp()
        {
            InitializeComponent();
        }

        public void CreateAccount(object sender, EventArgs e)
        {
            if (txt_Email.Text == "" || txt_Password.Password == "")
                MessageBox.Show("Please enter an email and password");
            else
                Networking.CreateUser(txt_Email.Text.ToLower(), txt_Password.Password, txt_Name.Text, txt_Photo.Text, eventhandler);
        }

        public void eventhandler(object sender, UploadStringCompletedEventArgs e)
        {
            lock (this)
            {
                if (Networking.Apitoken != "")
                {
                    StorageManager.SaveData("User.txt", Newtonsoft.Json.JsonConvert.SerializeObject(Networking.LocalUser));
                    StorageManager.SaveData("Apitoken.txt", Networking.Apitoken);
                    NavigationService.Navigate(new Uri("/LobbyScreen.xaml", UriKind.Relative));
                }
            }
        }

    }
}