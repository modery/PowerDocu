using System;
using System.Collections.Generic;

namespace PowerDocu.Common
{
    public static class NotificationHelper
    {
        private static readonly List<NotificationReceiverBase> NotificationReceivers = new List<NotificationReceiverBase>();

        public static void SendNotification(string notification)
        {
            foreach (NotificationReceiverBase notificationReceiver in NotificationReceivers)
            {
                notificationReceiver.Notify(notification);
            }
        }

        public static void AddNotificationReceiver(NotificationReceiverBase notificationReceiver)
        {
            NotificationReceivers.Add(notificationReceiver);
        }
    }

    public abstract class NotificationReceiverBase
    {
        public abstract void Notify(string notification);
    }
    
    public class ConsoleNotificationReceiver : NotificationReceiverBase
    {
        public override void Notify(string notification)
        {
            Console.WriteLine(notification);
        }
    }
}