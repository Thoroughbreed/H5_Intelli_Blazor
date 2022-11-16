using Blazored.Toast.Services;
using IntelliHouse2000.Models.Climate;
using IntelliHouse2000.Services.API;
using IntelliHouse2000.Services.MQTT;
using Microsoft.AspNetCore.Components;
using MQTTnet.Server;
using System;
using Toolbelt.Blazor.HotKeys;

namespace IntelliHouse2000.Pages
{
    public partial class ClimatePage
    {
        [Inject]
        public IToastService ToastService { get; set; }
        [Inject]
        public HotKeys HotKeys { get; set; }
        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }
        [Inject]
        public IAPIService APIService { get; set; }
        [Inject]
        public IMQTTService MQTTService { get; set; }

        #region Propertyes And variabels

        public DateTime? GrafTime { get; set; } = DateTime.Now;

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


            APIClimatesKitchen = await APIService.GetKitchenListAsync(GrafTime);
            APIClimatesLivingroom = await APIService.GetlivingroomListAsync(GrafTime);
            APIClimatesBedroom = await APIService.GetBedroomListAsync(GrafTime);
            Airqualities = await APIService.GetAirqualityListAsync();

            #endregion


        }

        void Toaster() => ToastService.ShowInfo("Congtats you just used a Hotkey: F8", "HotKey");

        public void Dispose() => HotKeysContext.Dispose();

        #region datepicker API

        async Task OnChangeKitchen(DateTime? value) 
        { 
            APIClimatesKitchen = await APIService.GetKitchenListAsync(value);
            await InvokeAsync(() => StateHasChanged());
        }
        async Task OnChangeLivingroom(DateTime? value)
        {
            APIClimatesLivingroom = await APIService.GetlivingroomListAsync(value);
            await InvokeAsync(() => StateHasChanged());
        }
        async Task OnChangeBedroom(DateTime? value)
        {
            APIClimatesBedroom = await APIService.GetBedroomListAsync(value);
            await InvokeAsync(() => StateHasChanged());
        }
        async Task OnChangeAirquality(DateTime? value)
        {
            Airqualities = await APIService.GetAirqualityListAsync();
            await InvokeAsync(() => StateHasChanged());
        }

        #endregion

        #region MQTT

        async void SetHumidKitchenAsync() 
        { 
            bool resonds = await MQTTService.Publish("home/climate/kitchen/sethumid", SetClimateKitchen.SetHumid.ToString(), false);
            if (resonds)
                ToastService.ShowSuccess($"{languageTable["ToasterSetHumidSucces"]}{SetClimateKitchen.SetHumid}", $"{languageTable["ToasterSucces"]}");
            else
                ToastService.ShowError($"{languageTable["ToasterSetHumidError"]}{SetClimateKitchen.SetHumid}", $"{languageTable["ToasterError"]}");
        }

        async void SetHumidLivingroomAsync()
        {
            bool resonds = await MQTTService.Publish("home/climate/livingroom/sethumid", SetClimateLivingroom.SetHumid.ToString(), false);
            if (resonds)
                ToastService.ShowSuccess($"{languageTable["ToasterSetHumidSucces"]}{SetClimateLivingroom.SetHumid}", $"{languageTable["ToasterSucces"]}");
            else
                ToastService.ShowError($"{languageTable["ToasterSetHumidError"]}{SetClimateLivingroom.SetHumid}", $"{languageTable["ToasterError"]}");
        }
        async void SetHumidBedroomAsync()
        {
            bool resonds = await MQTTService.Publish("home/climate/bedroom/sethumid", SetClimateBedroom.SetHumid.ToString(), false);
            if (resonds)
                ToastService.ShowSuccess($"{languageTable["ToasterSetHumidSucces"]}{SetClimateBedroom.SetHumid}", $"{languageTable["ToasterSucces"]}");
            else
                ToastService.ShowError($"{languageTable["ToasterSetHumidError"]}{SetClimateBedroom.SetHumid}", $"{languageTable["ToasterError"]}");
        }




        async void SetTempKitchenAsync()
        {
            bool resonds = await MQTTService.Publish("home/climate/kitchen/settemp", SetClimateKitchen.SetHumid.ToString(), false);
            if (resonds)
                ToastService.ShowSuccess($"{languageTable["ToasterSetTempSucces"]}{SetClimateKitchen.SetTemp}", $"{languageTable["ToasterSucces"]}");
            else
                ToastService.ShowError($"{languageTable["ToasterSetTempError"]}{SetClimateKitchen.SetTemp}", $"{languageTable["ToasterError"]}");
        }

        async void SetTempLivingroomAsync()
        {
            bool resonds = await MQTTService.Publish("home/climate/livingroom/settemp", SetClimateLivingroom.SetTemp.ToString(), false);
            if (resonds)
                ToastService.ShowSuccess($"{languageTable["ToasterSetTempSucces"]}{SetClimateLivingroom.SetTemp}", $"{languageTable["ToasterSucces"]}");
            else
                ToastService.ShowError($"{languageTable["ToasterSetTempError"]}{SetClimateLivingroom.SetTemp}", $"{languageTable["ToasterError"]}");
        }
        async void SetTempBedroomAsync()
        {
            bool resonds = await MQTTService.Publish("home/climate/bedroom/settemp", SetClimateBedroom.SetTemp.ToString(), false);
            if (resonds)
                ToastService.ShowSuccess($"{languageTable["ToasterSetTempSucces"]}{SetClimateBedroom.SetTemp}", $"{languageTable["ToasterSucces"]}");
            else
                ToastService.ShowError($"{languageTable["ToasterSetTempError"]}{SetClimateBedroom.SetTemp}", $"{languageTable["ToasterError"]}");
        }

        #endregion

    }
}