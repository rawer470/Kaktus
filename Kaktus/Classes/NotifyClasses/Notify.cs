using System;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Kaktus.NotifyClasses;

public static class Notify
{
    public static INotyfService notifyService { get; private set; }

    public static async Task Configure(INotyfService notifyService)
    {
        Notify.notifyService = notifyService;
    }

    public static async Task ShowSuccess(string text, int time)
    {
        notifyService.Success(text, time);
    }
    public static async Task ShowError(string text, int time)
    {
        notifyService.Error(text, time);
    }
    public static async Task ShowWarning(string text, int time)
    {
        notifyService.Warning(text, time);
    }
}
