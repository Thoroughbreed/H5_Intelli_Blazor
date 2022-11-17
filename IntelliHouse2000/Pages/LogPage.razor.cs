using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;
using IntelliHouse2000.Models;
using IntelliHouse2000.Services.API;
using IntelliHouse2000.Services.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Toolbelt.Blazor.HotKeys;

namespace IntelliHouse2000.Pages
{
    public partial class LogPage
    {

        [Inject] private IToastService ToastService { get; set; }
        [Inject] private HotKeys HotKeys { get; set; }
        [Inject] private Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }
        [Inject] private IAPIService Service { get; set; }
        [Inject] private IDBService DBService { get; set; }
        public List<LogMessage> CriticalLogs { get; set; } = new();
        public List<LogMessage> SystemLogs { get; set; } = new();
        public List<LogMessage> InfoLogs { get; set; } = new();
        public List<LogMessage> UserLogs { get; set; } = new();


        HotKeysContext? HotKeysContext;
        I18nText.LanguageTable languageTable = new I18nText.LanguageTable();
        
        protected override async Task OnInitializedAsync()
        {
            languageTable = await I18nText.GetTextTableAsync<I18nText.LanguageTable>(this);

            this.HotKeysContext = this.HotKeys.CreateContext()
                .Add(ModKeys.None, Keys.F8, Toaster);
            
            CriticalLogs = await Service.GetCriticalLogsAsync();
            SystemLogs = await Service.GetSystemLogsAsync();
            InfoLogs = await Service.GetInfoLogsAsync();
            UserLogs = await DBService.GetLogs(3, LogType.user).ToListAsync();
            await DBService.WriteLogAsync(new LogMessage
            {
                Client = "Test",
                Message = "Test message",
                Timestamp = DateTime.Now,
                Topic = "home/log/user"
            });
        }

        void Toaster() => ToastService.ShowInfo("Congtats ypu just used a Hotkey: F8", "HotKey");

        public void Dispose() => HotKeysContext.Dispose();
    }
}