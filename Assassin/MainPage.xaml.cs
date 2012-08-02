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
using System.IO;
using System.IO.IsolatedStorage;
using Tophat;

namespace Assassin
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            string email = txt_Email.Text;
            string password = txt_Password.Text;
            if (email == "" || password == "")
            {
                //Either or both of the email and password fields are empty.
                //TODO: Check format of email address before sending it
                MessageBox.Show("Please fill in an email and password please!", "Error", MessageBoxButton.OK);
                return;
            }

            Networking.Login(email, password, OnLoggedIn);
        }


        private void hbtn_SignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SignUp.xaml", UriKind.Relative));
        }

        private void hbtn_ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TODO: Create a forgot password page");
            NavigationService.Navigate(new Uri("/SignUp.xaml", UriKind.Relative));
        }

        public void OnLoggedIn(object sender, UploadStringCompletedEventArgs e)
        {
            lock (this)
            {
                CheckLoggedIn();
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            CheckLoggedIn();
        }

        private void CheckLoggedIn()
        {
            if (Networking.Apitoken != null && Networking.Apitoken != "")
            {
                NavigationService.Navigate(new Uri("/LobbyScreen.xaml", UriKind.Relative));
                StorageManager.SaveData("User.txt", Newtonsoft.Json.JsonConvert.SerializeObject(Networking.LocalUser));
                StorageManager.SaveData("Apitoken.txt", Networking.Apitoken);
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Add the event handlers here because they are removed when navigating from this page
            btn_Login.Click += btn_Login_Click;
            hbtn_SignUp.Click += hbtn_SignUp_Click;
            hbtn_ForgotPassword.Click += hbtn_ForgotPassword_Click;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //Remove these event handlers to prevent the user from taking any action while
            //the page is navigating away
            btn_Login.Click -= btn_Login_Click;
            hbtn_SignUp.Click -= hbtn_SignUp_Click;
            hbtn_ForgotPassword.Click -= hbtn_ForgotPassword_Click;

            base.OnNavigatingFrom(e);
        }

    }
}