﻿using System;
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
using System.Collections.ObjectModel;
using Microsoft.Phone.Controls;
using System.Text;
using Tophat;

namespace Assassin
{
    public partial class LobbyScreen : PhoneApplicationPage
    {
        List<Game> MyGames;

        public LobbyScreen()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Networking.GetUserDetails(OnGetUserDetails);
            var Data_lst_Types = new List<string>();

            for (int i = 0; i < Networking.GameTypes.Length; i++)
            {
                Data_lst_Types.Add(Networking.GameTypes[i].name);
            }
            lst_Types.ItemsSource = Data_lst_Types;
        }

        private void OnGetGames(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                //Nudge the response to check for an exception
                string x = e.Result;
                BindGameList();
            }
            catch (WebException)
            {
                //TODO: Start project for mango
                if (Networking.isStopped)
                    MessageBox.Show("Couldn't connect to the server.");
                //ProgressIndicator p = new ProgressIndicator{ Text="Couldn't connect to the server." }
                //Microsoft.Shell.SystemTray.SetProgressIndicator(this, p);
                else
                    MessageBox.Show("Failed to connect. Retrying...");
                //ProgressIndicator p = new ProgressIndicator{ Text="Couldn't connect to the server." }
                //Microsoft.Shell.SystemTray.SetProgressIndicator(this, p);
            }
        }

        private void OnGetUserDetails(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                //Nudge the response to check for an exception
                string x = e.Result;
                Networking.GetGames(OnGetGames);
            }
            catch (WebException)
            {
                //TODO: Start project for mango
                if (Networking.isStopped)
                    MessageBox.Show("Couldn't connect to the server.");
                    //ProgressIndicator p = new ProgressIndicator{ Text="Couldn't connect to the server." }
                    //Microsoft.Shell.SystemTray.SetProgressIndicator(this, p);
                else
                    MessageBox.Show("Failed to connect. Retrying...");
                    //ProgressIndicator p = new ProgressIndicator{ Text="Couldn't connect to the server." }
                    //Microsoft.Shell.SystemTray.SetProgressIndicator(this, p);
            }
        }

        private void Refresh(object sender, EventArgs e)
        {
            Networking.GetUserDetails(OnGetUserDetails);
        }
        private void CreateGame(object sender, EventArgs e)
        {
            if (txt_Name.Text == "Name")
                MessageBox.Show("Please enter a name for the game!");
            else if (lst_Types.SelectedIndex == -1)
                MessageBox.Show("Please select a game type!");
            else
                Networking.CreateGame(txt_Name.Text, Networking.GameTypes[lst_Types.SelectedIndex].id);
        }

        private void BindGameList()
        {
            //Refreshes both the list of joinable games AND the list of owned games

            System.Text.StringBuilder sideInfo_Lobby = new System.Text.StringBuilder();
            System.Text.StringBuilder sideInfo_MyGames = new System.Text.StringBuilder();

            MyGames = new List<Game>();
            for (int i = 0; i < Networking.Games.Count; i++)
            {
                //Check if the user is playing this game
                for (int j = 0; j < Networking.LocalUser.joined_games.Count; j++)
                {
                    if (Networking.LocalUser.joined_games[j].Equals(Networking.Games[i]))
                        Networking.Games[i].isPlaying = true;
                }

                bool isCreator = Networking.Games[i].creator.Equals(Networking.LocalUser);
                sideInfo_Lobby.Append(isCreator ? "Creator\n" : (Networking.Games[i].isPlaying ? "Playing\n" : "\n"));


                if (Networking.Games[i].creator.Equals(Networking.LocalUser))
                {
                    sideInfo_MyGames.Append("Creator\n");
                    MyGames.Add(Networking.Games[i]);
                }
            }

            lbl_LobbyGameStatus.Text = sideInfo_Lobby.ToString();
            lbl_MyGameStatus.Text = sideInfo_MyGames.ToString();

            //TODO: Separate this from the games event handler
            //Refreshes the User data
            lbl_Name.Text = "Name: " + Networking.LocalUser.name;
            lbl_Email.Text = "Email: " + Networking.LocalUser.email;
            lbl_Created.Text = "Created: " + Networking.LocalUser.created;

            lst_LobbyGames.ItemsSource = Networking.Games;
            lst_MyGames.ItemsSource = MyGames;
        }

        private void OnGotFocusText(object sender, EventArgs e)
        {
            TextBox txtbox = (TextBox)sender;

            if (txtbox.Text == "Name")
            {
                txtbox.Text = "";
                txtbox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void OnLostFocusText(object sender, EventArgs e)
        {
            TextBox txtbox = (TextBox)sender;

            if (txtbox.Text == "")
            {
                txtbox.Text = "Name";
                txtbox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }



        private void GameList_Click(object sender, EventArgs e)
        {
            if ((sender as ListBox).SelectedIndex == -1)
                return;
            ShowGameDetails(Networking.Games[((ListBox)sender).SelectedIndex]);
        }
        private void MyGameList_Click(object sender, EventArgs e)
        {
            if ((sender as ListBox).SelectedIndex == -1)
                return;
            ShowGameDetails(MyGames[((ListBox)sender).SelectedIndex]);
        }

        private void ShowGameDetails(Game selected)
        {
            //Now, check if the user is already playing
            string message;
            string query;

            if (selected.isPlaying)
            {
                query = "?ContinueGame=" + selected.id;
                message = String.Format("\nCreator: {0}\nPlayers: {1}/{2}\nStarted: {3}\nGame Type: {4}\n\n                   {5}",
                    "Me",
                    selected.players.Count,
                    selected.maxplayers,
                    selected.time.ToString(),
                    Networking.GameTypes[selected.gameType].name,
                    "Continue This Game?");
            }
            else
            {
                query = "?JoinGame=" + selected.id;
                message = String.Format("\nCreator: {0}\nPlayers: {1}/{2}\nStarted: {3}\nGame Type: {4}\n\n                   {5}",
                    selected.creator.name,
                    selected.players.Count,
                    selected.maxplayers,
                    selected.time.ToString(),
                    Networking.GameTypes[selected.gameType].name,
                    "Join This Game?");
            }


            if (MessageBox.Show(message, (string)selected.name, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                NavigationService.Navigate(new Uri("/GamePage.xaml" + query, UriKind.Relative));
        }
        
        private void ShowPlayerList(object sender, EventArgs e)
        {
            Game selected = (Game)(sender as MenuItem).DataContext;

            StringBuilder sb = new StringBuilder(String.Format("People playing {0}:", selected.name));

            for (int i = 0; i < selected.players.Count; i++)
            {
                sb.Append("\n");
                sb.Append(selected.players[i].name);
            }

            MessageBox.Show(sb.ToString());
        }

        private void DeleteGame(object sender, RoutedEventArgs e)
        {
            int GameId = ((Game)(sender as MenuItem).DataContext).id;
            Networking.DeleteGame(GameId, OnDeleteGame);
        }
        
        private static void OnDeleteGame(IAsyncResult e)
        {
            if (e.IsCompleted)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("Deleted game successfully");
                }
                );
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("Failed to delete the game");
                });
            }
        }


        private const int MAX_PLAYERS = 100;
        /// <summary>
        /// Prevents invalid characters from being entered and limits the amount that can be entered
        /// </summary>
        private void txt_NumPlayers_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Unknown)
            {
                txt_NumPlayers.Text = txt_NumPlayers.Text.Replace(".", "");
                txt_NumPlayers.SelectionStart = txt_NumPlayers.Text.Length;
            }

            if (txt_NumPlayers.Text != "")
            {
                int entry = int.Parse(txt_NumPlayers.Text);
                if (entry > MAX_PLAYERS)
                {
                    txt_NumPlayers.Text = MAX_PLAYERS.ToString();
                    txt_NumPlayers.SelectionStart = 3;
                }
                else 
                    txt_NumPlayers.Text = entry.ToString();
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Logging out", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Networking.Apitoken = "";
                //Erase the stored apitoken
                StorageManager.SaveData("Apitoken.txt", "");
            }
            else
                e.Cancel = true;
        }
    }
}