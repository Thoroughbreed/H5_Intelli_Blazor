using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;
using Toolbelt.Blazor.HotKeys;

namespace IntelliHouse2000.Pages
{
    public partial class ClimatePage
    {

        [Inject]
        private IToastService ToastService { get; set; }
        [Inject]
        private HotKeys HotKeys { get; set; }
        [Inject]
        private Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }


        HotKeysContext? HotKeysContext;
        I18nText.LanguageTable languageTable = new I18nText.LanguageTable();

        protected override async Task OnInitializedAsync()
        {
            languageTable = await I18nText.GetTextTableAsync<I18nText.LanguageTable>(this);

            this.HotKeysContext = this.HotKeys.CreateContext()
                .Add(ModKeys.None, Keys.F8, Toaster);
        }

        void Toaster() => ToastService.ShowInfo("Congtats ypu just used a Hotkey: F8", "HotKey");

        public void Dispose() => HotKeysContext.Dispose();


    }
}