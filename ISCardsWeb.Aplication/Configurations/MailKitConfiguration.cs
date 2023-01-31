﻿namespace ISCardsWeb.Application.Configurations
{
    public class MailKitConfiguration
    {
        public string Server { get; set; } = "";
        public int Port { get; set; }
        public string SenderName { get; set; } = "";
        public string SenderEmail { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
