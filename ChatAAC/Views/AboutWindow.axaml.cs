using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using ChatAAC.ViewModels;

namespace ChatAAC.Views;

public partial class AboutWindow : Window
{
    public AboutWindow()
    {
        InitializeComponent();

#if DEBUG
        this.AttachDevTools();
#endif

        // Subskrybuj zmianę DataContext, aby zarejestrować handler
        DataContextChanged += AboutWindow_DataContextChanged;
    }

    private void AboutWindow_DataContextChanged(object sender, EventArgs e)
    {
        var newDataContext = ((Control)sender).DataContext;

        if (newDataContext is AboutViewModel viewModel)
        {
            viewModel.CloseInteraction.RegisterHandler(async interaction =>
            {
                interaction.SetOutput(Unit.Default);
                await Dispatcher.UIThread.InvokeAsync(() => this.Close());
            });
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}