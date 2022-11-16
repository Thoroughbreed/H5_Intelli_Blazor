using Blazored.Toast.Services;
using IntelliHouse2000.Models.Climate;
using IntelliHouse2000.Services.API;
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

        [Inject]
        private IAPIService APIService { get; set; }

        #region Propertyes And variabels

        DateTime GrafTimeKitchen { get; set; } = DateTime.Now;
        DateTime GrafTimeLivingroom  { get; set; } = DateTime.Now;
        DateTime GrafTimeBedroom { get; set; } = DateTime.Now;
        DateTime GrafTimeAirquality { get; set; } = DateTime.Now;

        SetClimate SetClimateKitchen { get; set; } = new();
        APIClimate APIClimateKitchen { get; set; } = new();
        List<APIClimate> APIClimatesKitchen { get; set; } = new();

        SetClimate SetClimateLivingroom { get; set; } = new();
        APIClimate APIClimateLivingroom { get; set; } = new();
        List<APIClimate> APIClimatesLivingroom { get; set; } = new();

        SetClimate SetClimateBedroom { get; set; } = new();
        APIClimate APIClimateBedroom { get; set; } = new();
        List<APIClimate> APIClimatesBedroom { get; set; } = new();

        Airquality Airquality { get; set; } = new();
        List<Airquality> Airqualities { get; set; } = new();

        HotKeysContext? HotKeysContext;
        I18nText.LanguageTable languageTable = new I18nText.LanguageTable();

        #endregion

        protected override async Task OnInitializedAsync()
        {
            // get all data for services

            languageTable = await I18nText.GetTextTableAsync<I18nText.LanguageTable>(this);

            this.HotKeysContext = this.HotKeys.CreateContext()
                .Add(ModKeys.None, Keys.F8, Toaster);

            #region Get Data Form API

            APIClimateKitchen = await APIService.GetKitchenAsync();
            APIClimateLivingroom = await APIService.GetlivingroomAsync();
            APIClimateBedroom = await APIService.GetBedroomAsync();
            Airquality = await APIService.GetAirqualityAsync();
            

            APIClimatesKitchen = await APIService.GetKitchenListAsync(GrafTimeKitchen);
            APIClimatesLivingroom = await APIService.GetlivingroomListAsync(GrafTimeLivingroom);
            APIClimatesBedroom = await APIService.GetBedroomListAsync(GrafTimeBedroom);
            Airqualities = await APIService.GetAirqualityListAsync();

            #endregion

        }

        void Toaster() => ToastService.ShowInfo("Congtats ypu just used a Hotkey: F8", "HotKey");
        public void Dispose() => HotKeysContext.Dispose();

        async Task OnChangeKitchen() 
        { 
            APIClimatesKitchen = await APIService.GetKitchenListAsync(GrafTimeKitchen);
            await InvokeAsync(() => StateHasChanged());
        }
        async Task OnChangeLivingroom()
        {
            APIClimatesLivingroom = await APIService.GetlivingroomListAsync(GrafTimeLivingroom);
            await InvokeAsync(() => StateHasChanged());
        }
        async Task OnChangeBedroom()
        {
            APIClimatesBedroom = await APIService.GetBedroomListAsync(GrafTimeBedroom);
            await InvokeAsync(() => StateHasChanged());
        }
        async Task OnChangeAirquality()
        {
            Airqualities = await APIService.GetAirqualityListAsync();
            await InvokeAsync(() => StateHasChanged());
        } 

        // async void SetHumidAsync() => await // service
        // async void SetTempAsync() => await // service


    }
}