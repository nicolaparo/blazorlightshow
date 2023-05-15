using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using NicolaParo.LightShow.App.Controls;
using NicolaParo.LightShow.Components;
using NicolaParo.LightShow.Services.Abstractions;
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
            services.AddBlazorWebViewDeveloperTools();
            services.AddSingleton<IDmxAdapter, DmxAdapter>();

            blazor.HostPage = @"wwwroot/index.html";
            blazor.Services = services.BuildServiceProvider();

            blazor.RootComponents.Add(new RootComponent(@"#app", typeof(ApplicationView), new Dictionary<string, object>()));
            blazor.Dock = DockStyle.Fill;

            blazor.BlazorWebViewInitialized = (s,e) =>
            {
                e.WebView.ZoomFactor = 1;
                e.WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
                e.WebView.CoreWebView2.Settings.IsPinchZoomEnabled = false;
            };

            FormClosing += (s, e) =>
            {
                MessageBox.Show("True WinForm App", "WinForm <3 Blazor");
            };

            Controls.Add(blazor);

            Size = new Size(1600, 1200);

        }

        
    }
}
