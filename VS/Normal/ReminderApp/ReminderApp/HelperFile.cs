﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReminderApp
{
    public class HelperFile
    {
        public readonly string FolderName = "JsonFiles";
        public readonly string FolderPath;

        public HelperFile()
        {
            FolderPath = AppDomain.CurrentDomain.BaseDirectory + "\\" + FolderName + "\\";
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
        }

        public bool SaveTheJsonFile(string jsonData, string saveFilePath)
        {
            File.WriteAllText(saveFilePath, jsonData);
            return true;
        }

        public string CreateTheJsonFile(object objectData)
        {
            string stringjson = "";
            if (objectData != null)
            {
                stringjson = JsonConvert.SerializeObject(objectData);
            }
            return stringjson;
        }

        public T ConvertJsonStringToObject<T>(string jsonString) where T : class
        {
            T result = JsonConvert.DeserializeObject<T>(jsonString);
            return result;
        }

        public string ReadTheJsonFile(string readFilePath)
        {
            return File.ReadAllText(readFilePath);
        }

        public string GetLatestFile()
        {
            var directory = new DirectoryInfo(FolderPath);
            return directory.GetFiles()
                             .OrderByDescending(f => f.LastWriteTime)
                             .FirstOrDefault()?.FullName.ToString();
        }

        public void PlayNotificationSound(Stream stream)
        {
            SoundPlayer player = new SoundPlayer();
            //Add TestSound.wav file to the built-in resource file Project>Properties>Resources.resx
            player.Stream = stream;
            //Add TestSound.wav file to a new resource file Resource1.resx
            //player.Stream = Resource1.TestSound;
            player.Play();
        }

        public void SetStartup(bool setStartUp, string AppName)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (setStartUp)
                rk.SetValue(AppName, Assembly.GetEntryAssembly().Location);
            else
                rk.DeleteValue(AppName, false);

        }

        public void ShowNotifyBalloon(string tipTitle, string Message, Icon icon)
        {
            NotifyIcon notifyIcon = new NotifyIcon()
            {
                Visible = true,               
                Icon = icon
            }; 

            notifyIcon.ShowBalloonTip(5000, tipTitle, Message, ToolTipIcon.Info);
       
            // Dispose on event
            notifyIcon.BalloonTipClosed += (sender, e) => notifyIcon.Dispose();
        }
    }
}
