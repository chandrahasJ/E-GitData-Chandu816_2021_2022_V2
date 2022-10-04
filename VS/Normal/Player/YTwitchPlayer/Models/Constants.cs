﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTwitchPlayer.Models
{
    public static class Constants
    {
        public static string ApplicationName = "Twitchy Player";
        public static string EmailAddress = "purplepills777p@gmail.com";
        public static string ApplicationId = ".cjsoftwares.ytwitchplayer";
        public static string ApiServiceURL = @"https://youtube.googleapis.com/youtube/v3/";
        public static string ApiKey = @"AIzaSyAT7UDu2LYMDWHPP09sD7Rq6G-lUSX4CA8";

        public static uint MicroDuration { get; set; } = 100;
        public static uint SmallDuration { get; set; } = 300;
        public static uint MediumDuration { get; set; } = 600;
        public static uint LongDuration { get; set; } = 1200;
        public static uint ExtraLongDuration { get; set; } = 1800;
    }
}
