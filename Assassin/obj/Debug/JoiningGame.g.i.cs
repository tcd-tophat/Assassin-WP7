﻿#pragma checksum "C:\Users\Eoin\WP7-Assassin\Assassin\JoiningGame.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "05C98A83D11C6CCBA6EA041070D2482D"
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
    
    
    public partial class GameDetails : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock lbl_Creator;
        
        internal System.Windows.Controls.TextBlock lbl_MinPlayers;
        
        internal System.Windows.Controls.TextBlock lbl_Started;
        
        internal System.Windows.Controls.TextBlock lbl_GameType;
        
        internal System.Windows.Controls.Button btn_Join;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Assassin;component/JoiningGame.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.lbl_Creator = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_Creator")));
            this.lbl_MinPlayers = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_MinPlayers")));
            this.lbl_Started = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_Started")));
            this.lbl_GameType = ((System.Windows.Controls.TextBlock)(this.FindName("lbl_GameType")));
            this.btn_Join = ((System.Windows.Controls.Button)(this.FindName("btn_Join")));
        }
    }
}
