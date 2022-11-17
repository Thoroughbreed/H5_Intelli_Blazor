using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using IntelliHouse2000.Models;
using IntelliHouse2000.Services.API;
using IntelliHouse2000.Services.Database;
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
        [Inject] private IHttpContextAccessor HttpContextAccessor { get; set; }
        public IEnumerable<LogMessage> CriticalLogs { get; set; }
        public IEnumerable<LogMessage> SystemLogs { get; set; }
        public IEnumerable<LogMessage> InfoLogs { get; set; } 
        public IEnumerable<LogMessageDTO> UserLogs { get; set; }


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
            UserLogs = DBService.GetLogs(3, LogType.user);
        }

        void Toaster() => ToastService.ShowInfo("Congtats ypu just used a Hotkey: F8", "HotKey");

        public void Dispose() => HotKeysContext.Dispose();
    }
}