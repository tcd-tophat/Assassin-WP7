﻿#pragma checksum "C:\Users\Eoin\WP7-Assassin\Assassin\LobbyScreen.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "547D2FFE2731C73D03B10DAED857A8A8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Assassin {
    
    
    public partial class LobbyScreen : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBox txt_FilterGames;
        
        internal System.Windows.Controls.TextBlock lbl_LobbyGames;
        
        internal System.Windows.Controls.ListBox lst_LobbyGames;
        
        internal System.Windows.Controls.TextBlock lbl_LobbyGameStatus;
        
        internal System.Windows.Controls.TextBox txt_Name;
        
        internal System.Windows.Controls.TextBlock lbl_NumPlayers;
        
        internal System.Windows.Controls.TextBox txt_NumPlayers;
        
        internal System.Windows.Controls.TextBlock lbl_GameType;
        
        internal System.Windows.Controls.ListBox lst_Types;
        
        internal System.Windows.Controls.TextBox txt_FilterMyGames;
        
        internal System.Windows.Controls.TextBlock lbl_MyGames;
        
        internal System.Windows.Controls.ListBox lst_MyGames;
        
        internal System.Windows.Controls.TextBlock lbl_MyGameStatus;
        
        internal System.Windows.Controls.TextBlock lbl_Name;
        
        internal System.Windows.Controls.TextBlock lbl_Email;
        
        internal System.Windows.Controls.TextBlock lbl_Created;
        
        internal System.Windows.Controls.Image img_Photo;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton btn_Create;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton btn_Refresh;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Assassin;component/LobbyScreen.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.txt_FilterGames = ((System.Windows.Controls.TextBox)(this.FindName("txt_FilterGames")));
            this.lbl_LobbyGames = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_LobbyGames")));
            this.lst_LobbyGames = ((System.Windows.Controls.ListBox)(this.FindName("lst_LobbyGames")));
            this.lbl_LobbyGameStatus = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_LobbyGameStatus")));
            this.txt_Name = ((System.Windows.Controls.TextBox)(this.FindName("txt_Name")));
            this.lbl_NumPlayers = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_NumPlayers")));
            this.txt_NumPlayers = ((System.Windows.Controls.TextBox)(this.FindName("txt_NumPlayers")));
            this.lbl_GameType = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_GameType")));
            this.lst_Types = ((System.Windows.Controls.ListBox)(this.FindName("lst_Types")));
            this.txt_FilterMyGames = ((System.Windows.Controls.TextBox)(this.FindName("txt_FilterMyGames")));
            this.lbl_MyGames = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_MyGames")));
            this.lst_MyGames = ((System.Windows.Controls.ListBox)(this.FindName("lst_MyGames")));
            this.lbl_MyGameStatus = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_MyGameStatus")));
            this.lbl_Name = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_Name")));
            this.lbl_Email = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_Email")));
            this.lbl_Created = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_Created")));
            this.img_Photo = ((System.Windows.Controls.Image)(this.FindName("img_Photo")));
            this.btn_Create = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("btn_Create")));
            this.btn_Refresh = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("btn_Refresh")));
        }
    }
}

