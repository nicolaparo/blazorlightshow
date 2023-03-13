using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using NicolaParo.LightShow.App.Components;
using NicolaParo.LightShow.App.Controls;
using NicolaParo.LightShow.Services;
using NicolaParo.LightShow.Services.Dmx;
using System.ComponentModel;

namespace NicolaParo.LightShow.App
{
    [DesignerCategory("")]
    public class ApplicationForm : ImmersiveDarkModeForm
    {
        public ApplicationForm()
        {
            var blazor = new BlazorWebView();

            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddSingleton<IDmxAdapter, DmxAdapter>();

            blazor.HostPage = @"wwwroot/index.html";
            blazor.Services = services.BuildServiceProvider();

            blazor.RootComponents.Add(new RootComponent(@"#app", typeof(ApplicationView), new Dictionary<string, object>()));
            blazor.Dock = DockStyle.Fill;

            Controls.Add(blazor);

            Size = new Size(1600, 1200);

        }
    }
}