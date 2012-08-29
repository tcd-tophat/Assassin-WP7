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
using Coding4Fun.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Devices;
using System.IO;

namespace Assassin
{
    public partial class GamePage : PhoneApplicationPage
    {
        private PhotoCamera cam;
        private Game CurrentGame;
        private Player CurrentPlayer;

        private string name;
        private int gameId;
        private string photo;

        public GamePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (cam != null)
            {
                cam.Dispose();
                cam.Initialized -= cam_Initialized;
                cam.CaptureImageAvailable -= cam_CaptureImageAvailable;
                CameraButtons.ShutterKeyHalfPressed -= OnButtonHalfPress;
                CameraButtons.ShutterKeyPressed -= OnButtonFullPress;
                CameraButtons.ShutterKeyReleased -= OnButtonRelease;
            }
            
            base.OnNavigatingFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            cam = new PhotoCamera(CameraType.Primary);
            cam.Initialized += cam_Initialized;

            // Event is fired when the capture sequence is complete and an image is available.
            cam.CaptureImageAvailable += cam_CaptureImageAvailable;

            // The event is fired when the shutter button receives a half press.
            CameraButtons.ShutterKeyHalfPressed += OnButtonHalfPress;

            // The event is fired when the shutter button receives a full press.
            CameraButtons.ShutterKeyPressed += OnButtonFullPress;

            // The event is fired when the shutter button is released.
            CameraButtons.ShutterKeyReleased += OnButtonRelease;

            videobrush.SetSource(cam);
            videobrush.RelativeTransform =
                new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 90 };

            if (NavigationContext.QueryString.ContainsKey("JoinGame"))
            {
                gameId = int.Parse(NavigationContext.QueryString["JoinGame"]);
                CurrentGame = Networking.GetGameById_Local(gameId);

                getName();
            }
            else if (NavigationContext.QueryString.ContainsKey("ContinueGame"))
            {
                gameId = int.Parse(NavigationContext.QueryString["ContinueGame"]);

                CurrentPlayer = Networking.GetPlayerByGameId_Local(gameId);
                CurrentGame = Networking.GetGameById_Local(gameId);

                lbl_MyName.Text = CurrentPlayer.name.ToString();
                lbl_MyScore.Text = CurrentPlayer.score.ToString();
                lbl_MyTime.Text = CurrentPlayer.time.ToString();
            }


            base.OnNavigatedTo(e);
        }

        private void cam_Initialized(object sender, CameraOperationCompletedEventArgs e)
        {

        }

        private void ShutterButton_Click(object sender, RoutedEventArgs e)
        {
            if (cam != null)
            {
                try
                {
                    // Start image capture.
                    cam.CaptureImage();
                }
                catch (Exception)
                {
                    //Must wait for previous capture to finish
                }
            }
        }

        // Informs when full resolution picture has been taken, saves to local media library and isolated storage.
        void cam_CaptureImageAvailable(object sender, Microsoft.Devices.ContentReadyEventArgs e)
        {
            try
            {
                Networking.UploadImage(e.ImageStream);

                // Set the position of the stream back to start
                e.ImageStream.Seek(0, SeekOrigin.Begin);
            }
            finally
            {
                // Close image stream
                e.ImageStream.Close();
            }

        }

        private void OnButtonHalfPress(object sender, EventArgs e)
        {
            if (cam != null)
            {
                try
                {
                    cam.Focus();
                }
                catch
                {
                    // Cannot focus when a capture is in progress.
                }
            }
        }
        // Capture the image with a full button press using the hardware shutter button.
        private void OnButtonFullPress(object sender, EventArgs e)
        {
            try
            {
                if (cam != null)
                {
                    cam.CaptureImage();
                }
            }
            catch
            {
                //Can't capture a new image until the previous one is completed
            }
        }

        // Cancel the focus if the half button press is released using the hardware shutter button.
        private void OnButtonRelease(object sender, EventArgs e)
        {
            if (cam != null)
            {
                cam.CancelFocus();
            }
        }

        private void getName()
        {
            var input = new InputPrompt();
            input.Title = "Name";
            input.Message = "What nickname would you like to use for this game?";
            input.Value = Networking.LocalUser.name;
            input.Completed += inputCompleted;
            input.Show();
        }

        private void inputCompleted(object sndr, PopUpEventArgs<string, PopUpResult> evt)
        {
            if (evt.PopUpResult == PopUpResult.Cancelled)
            {
                //The user used the back button to cancel, so bring them back to the lobby screen
                if (MessageBox.Show("Are you sure you want to quit them game? You must enter a name to play", "Quit Game", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    NavigationService.GoBack();
                else
                    getName();
            }
            if (evt.Result == "")
                getName();
            else
            {
                name = evt.Result;
                getPhoto();
            }
        }

        private void getPhoto()
        {
            if (Networking.LocalUser.photo == "" || 
                Networking.LocalUser.photo == "None" ||
                MessageBox.Show("Would you like to choose a different photo for this game or use the one you signed up with?", "Photo", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                PhotoChooserTask pct = new PhotoChooserTask();
                pct.ShowCamera = true;
                pct.Completed += pct_Completed;
                pct.Show();
            }
            else
            {
                var dict = new Dictionary<string, object>();
                dict["photo"] = Networking.LocalUser.photo;
                Networking.JoinGame<Player>(name, gameId, dict, OnJoinGame);
            }
        }

        private void pct_Completed(object sender, PhotoResult Result)
        {
            var dict = new Dictionary<string, object>();
            dict["photo"] = "abcdefghijklmnopqrstuvwxyz012345";

            //TODO: Upload an actual photo and get its ID back
            //TODO: Check if a photo was actually chosen(May just default to the default photo if cancelled)
            Networking.JoinGame<Player>(name, gameId, dict, OnJoinGame);
        }

        private void OnJoinGame(object sender, UploadStringCompletedEventArgs e)
        {
            CurrentPlayer = Networking.GetPlayerByGameId_Local(CurrentGame.id);
            if (CurrentPlayer != null)
            {
                lbl_MyName.Text = CurrentPlayer.name.ToString();
                lbl_MyScore.Text = CurrentPlayer.score.ToString();
                lbl_MyTime.Text = CurrentPlayer.time.ToString();

                //Save the list of players for later
            }
            else
                MessageBox.Show("Failed to load the player");
        }
    }
}