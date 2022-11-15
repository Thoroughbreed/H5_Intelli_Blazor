using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;
using IntelliHouse2000.Models;
using IntelliHouse2000.Services.API;
using Toolbelt.Blazor.HotKeys;

namespace IntelliHouse2000.Pages
{
    public partial class LogPage
    {

        [Inject] private IToastService ToastService { get; set; }
        [Inject] private HotKeys HotKeys { get; set; }
        [Inject] private Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }
        private APIService _service;
        public List<LogMessage> CriticalLogs { get; set; } = new();
        public List<LogMessage> SystemLogs { get; set; } = new();
        public List<LogMessage> InfoLogs { get; set; } = new();


        HotKeysContext? HotKeysContext;
        I18nText.LanguageTable languageTable = new I18nText.LanguageTable();

        public LogPage(APIService service)
        {
            _service = service;
        }


        protected override async Task OnInitializedAsync()
        {
            languageTable = await I18nText.GetTextTableAsync<I18nText.LanguageTable>(this);

            this.HotKeysContext = this.HotKeys.CreateContext()
                .Add(ModKeys.None, Keys.F8, Toaster);
            
            CriticalLogs = await _service.GetCriticalLogsAsync();
            SystemLogs = await _service.GetSystemLogsAsync();
            InfoLogs = await _service.GetInfoLogsAsync();
        }

        void Toaster() => ToastService.ShowInfo("Congtats ypu just used a Hotkey: F8", "HotKey");

        public void Dispose() => HotKeysContext.Dispose();
    }
}