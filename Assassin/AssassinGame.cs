using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using Tophat;

namespace Assassin
{
    public class AssassinGame : Game
    {
        public int kills;
        public AssassinGame(int id, int maxplayers, User creator, DateTime time, int gameType, string name, List<Player>players , int kills)
            : base(id, creator, time, gameType, name, players, maxplayers)
        {
            this.kills = kills;
        }
    }
    
}
