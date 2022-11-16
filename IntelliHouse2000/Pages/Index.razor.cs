using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using IntelliHouse2000.Services.Alarm;

namespace IntelliHouse2000.Pages
{
    public partial class Index
    {
        [Inject]
        private IAlarmService AlarmService { get; set; }
        [Inject]
        private IToastService ToastService { get; set; }
        [Inject]
        private Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }


        I18nText.LanguageTable languageTable = new I18nText.LanguageTable();

        protected override async Task OnInitializedAsync()
        {
            languageTable = await I18nText.GetTextTableAsync<I18nText.LanguageTable>(this);
        }

        public async Task SetArmedAsync(ArmedState state)
        {
            bool success = await AlarmService.SetArmed(state);
            if (success) ToastService.ShowSuccess("Request send");
            else ToastService.ShowError("Could not send request");
        }
    }
}