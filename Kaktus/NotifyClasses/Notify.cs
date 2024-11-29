using System;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Kaktus.NotifyClasses;

public static class Notify
{
    public static INotyfService notifyService { get; private set; }
    /// <summary>
    /// Конфигурация класс
    /// </summary>
    /// <param name="notifyService">Сервис для уведомлений</param>
    public static void Configure(INotyfService notifyService)
    {
        Notify.notifyService = notifyService;
    }

    /// <summary>
    /// выводит уведомление типа Success
    /// </summary>
    /// <param name="time">длительность показа в секундах</param>
    public static void ShowSuccess(string text, int time)
    {
        notifyService.Success(text, time);
    }
    /// <summary>
    /// выводит уведомление типа Error
    /// </summary>
    /// <param name="text">Текст уведомления</param>
    /// <param name="time">длительность показа в секундах</param>
    public static void ShowError(string text, int time)
    {
        notifyService.Error(text, time);
    }
    /// <summary>
    /// выводит уведомление типа Warning
    /// </summary>
    /// <param name="text">Текст уведомления</param>
    /// <param name="time">длительность показа в секундах</param>
    public static void ShowWarning(string text, int time)
    {
        notifyService.Warning(text, time);
    }
}
