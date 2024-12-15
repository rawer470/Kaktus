using System;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Kaktus.NotifyClasses;

public static class Notify
{
    public static INotyfService notifyService { get; private set; }

    public static void Configure(INotyfService notifyService)
    {
        Notify.notifyService = notifyService;
    }

    public static void ShowSuccess(string text, int time)
    {
        notifyService.Success(text, time);
    }
    public static void ShowError(string text, int time)
    {
        notifyService.Error(text, time);
    }
    public static void ShowWarning(string text, int time)
    {
        notifyService.Warning(text, time);
    }
}
