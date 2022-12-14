using System.Security.Claims;
using System.Text;
using Blazored.Toast.Services;
using IntelliHouse2000.Helpers;
using IntelliHouse2000.Models;
using IntelliHouse2000.Models.Alarm;
using Microsoft.AspNetCore.Components;
using IntelliHouse2000.Services.Alarm;
using IntelliHouse2000.Services.Database;
using IntelliHouse2000.Services.MQTT;
using MQTTnet;
using Toolbelt.Blazor.HotKeys;

namespace IntelliHouse2000.Pages
{
    public partial class Index
    {
#pragma warning disable CS8618
        [Inject] public HotKeys HotKeys { get; set; }
        [Inject] private IMQTTService MQTTService { get; set; }
        [Inject] private IAlarmService AlarmService { get; set; }
        [Inject] private IToastService ToastService { get; set; }
        // ReSharper disable once InconsistentNaming
        [Inject] private Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }
        [Inject] private IDBService DBService { get; set; }
        [Inject] private IHttpContextAccessor HttpContextAccessor { get; set; }
#pragma warning restore CS8618

        
        HotKeysContext? HotKeysContext;
        I18nText.LanguageTable languageTable = new I18nText.LanguageTable();

        protected override async Task OnInitializedAsync()
        {
            languageTable = await I18nText.GetTextTableAsync<I18nText.LanguageTable>(this);
            this.HotKeysContext = this.HotKeys.CreateContext()
                .Add(ModKeys.Ctrl, Keys.H, Toaster);

            await MQTTService.Subscribe(Constants.MqttArmedTopic);
            await MQTTService.Subscribe(Constants.MqttCriticalAlarmLogs);
            MQTTService.MessageReceived += OnMQTTMessage;
            await LogUserLoginAsync();
        }

        void Toaster() => ToastService.ShowInfo($"{languageTable["ToasterHelp"]}", $"{languageTable["Help"]}");

        private async void OnMQTTMessage(object? sender, MqttApplicationMessageReceivedEventArgs e)
        {
            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            string topic = e.ApplicationMessage.Topic;

            switch (topic)
            {
                case Constants.MqttArmedTopic when int.TryParse(payload, out int armedStateValue):
                    switch ((ArmedState)armedStateValue)
                    {
                        case ArmedState.Disarmed:
                            ToastService.ShowInfo($"{languageTable["AlarmDisMessage"]}");
                            break;
                        case ArmedState.PartiallyArmed:
                            ToastService.ShowInfo($"{languageTable["AlarmPartMessage"]}");
                            break;
                        case ArmedState.FullyArmed:
                            ToastService.ShowInfo($"{languageTable["AlarmArmMessage"]}");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case Constants.MqttCriticalAlarmLogs:
                    ToastService.ShowError(payload.Replace("Alarm: ", ""), "Alarm");
                    break;
            }
        }
        public async Task SetArmedAsync(ArmedState state)
        {
            bool success = await AlarmService.SetArmed(state);
            var message = "";
            switch (success)
            {
                case false:
                    ToastService.ShowError("Could not connect to alarm system");
                    message = "User tried to do something, but the alarm system didn't respond in time";
                    break;
                case true:
                    message = $"Alarm is now {state}";
                    break;
            }

            await DBService.WriteLogAsync(new LogMessage
            {
                Client = HttpContextAccessor.HttpContext.User.Identity.Name,
                Message = message,
                Timestamp = DateTime.Now,
                Topic = "home/log/user"
            });
        }
        public async void Dispose()
        {
            await MQTTService.Unsubscribe(Constants.MqttArmedTopic);
            await MQTTService.Unsubscribe(Constants.MqttCriticalAlarmLogs);
        }

        private async Task LogUserLoginAsync()
        {
#pragma warning disable CS8601
#pragma warning disable CS8602
            if (HttpContextAccessor.HttpContext.User.Identity!.IsAuthenticated)
            {
                var role = HttpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
                await DBService.WriteLogAsync(new LogMessage
                {
                    Client = HttpContextAccessor.HttpContext.User.Identity.Name,
                    Message = $"User logged in with role {role.Value} from {HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4()}",
                    Timestamp = DateTime.Now,
                    Topic = "home/log/user"
                });
            }
#pragma warning restore CS8601
#pragma warning restore CS8602
        }
    }
}