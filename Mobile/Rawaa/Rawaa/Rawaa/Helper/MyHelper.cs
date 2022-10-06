using Rawaa.Models;
using Rawaa.Resources.Languages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rawaa.Helper
{
    public static class MyHelper
    {
        static string OrderPending = "#FFF8DC";
        static string OrderProcessing = "#E0FFFF";
        static string OrderRejected = "#FFF8DC";
        static string OrderCompleted = "#CEF09D";
        static string OrderCanceled = "#ffcccc";

        static string OrderPendingText = "#B8860B";
        static string OrderProcessingText = "#B8860B";
        static string OrderRejectedText = "#B8860B";
        static string OrderCompletedText = "#1C646D";
        static string OrderCanceledText = "#ff1a1a";
        public static void HandleStatuseName(ref Order item)
        {

            switch (item.OrderStatus)
            {
                case 1:
                    item.StatusName = LanguageResources.OrderPending;
                    item.StatuseBackgrounColore = OrderPending;
                    item.StatuseTextColore = OrderPendingText;
                    break;
                case 2:
                    item.StatusName = LanguageResources.OrderProcessing;
                    item.StatuseBackgrounColore = OrderProcessing;
                    item.StatuseTextColore = OrderProcessingText;
                    break;
                case 3:
                    item.StatusName = LanguageResources.OrderRejected;
                    item.StatuseBackgrounColore = OrderRejected;
                    item.StatuseTextColore = OrderRejectedText;
                    break;
                case 4:
                    item.StatusName = LanguageResources.OrderCompleted;
                    item.StatuseBackgrounColore = OrderCompleted;
                    item.StatuseTextColore = OrderCompletedText;
                    break;
                case 5:
                    item.StatusName = LanguageResources.OrderCanceled;
                    item.StatuseBackgrounColore = OrderCanceled;
                    item.StatuseTextColore = OrderCanceledText;
                    break;
            }
        }
    }
}
