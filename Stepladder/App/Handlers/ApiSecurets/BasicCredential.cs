﻿using App.Settings;
using System.Text;

namespace App.Handlers.ApiSecurets
{
    public class BasicCredential
    {
        private static BasicCredential CURRENT;
        private static string AUTH_BASE64;
        public string User { get; private set; }
        public string Password { get; private set; }

        public static BasicCredential Current => CURRENT;

        public BasicCredential()
        {
            if (CURRENT == null)
            {
                var appSettings = ApplicationSetting.Current;
                User = appSettings.Startup.ApiSecuret.User;
                Password = appSettings.Startup.ApiSecuret.Password;

                CURRENT = this;
            }

            if(string.IsNullOrEmpty(AUTH_BASE64))
            {
                byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes($"{User}:{Password}");
                AUTH_BASE64 = $"Basic {Convert.ToBase64String(toEncodeAsBytes)}";
            }
        }

        public bool Validate(string authentication)
            => AUTH_BASE64 == authentication;
    }
}
