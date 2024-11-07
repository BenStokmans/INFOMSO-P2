using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using INFOMSO_P2.Commands;
using INFOMSO_P2.Gui.ViewModels;
using INFOMSO_P2.Metrics;

namespace INFOMSO_P2.Gui;

public partial class MainWindow : Window
{
    public MainWindowViewModel? ViewModel => DataContext as MainWindowViewModel;

    public MainWindow()
    {
        InitializeComponent();
    }


    private void RunProgram(object? sender, RoutedEventArgs e)
    {
        if (ViewModel is null) return;
        ViewModel.ResetScene();

        var parser = new StringProgramParser();
        Commands.Program program;
        try
        {
            program = parser.Parse(ProgramBox.Text);
            program.Run(ViewModel.Scene);
        }
        catch (Exception ex)
        {
            OutputBox.Text = ex.ToString();
            return;
        }

        OutputBox.Text = "Program executed successfully\n";
        OutputBox.Text += $"End state {ViewModel.Scene.GetCharacter()}\n";

        List<IMetricsCalculator> metrics =
        [
            new DepthMetricCalculator(),
            new RepeatMetricCalculator()
        ];

        OutputBox.Text += "Metrics:\n";
        foreach (IMetricsCalculator metric in metrics)
            OutputBox.Text += $"{metric.CalculateMetrics(program)}\n";

        OutputCanvas.InvalidateVisual();
    }

    private async void ChangeProgramSelection(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;
        if (ViewModel is null) return;

        IProgramParser parser;
        switch (e.AddedItems[0])
        {
            case "Basic":
                parser = new HardCodedProgramParser();
                break;
            case "Advanced":
                parser = new HardCodedProgramParser();
                break;
            case "Expert":
                parser = new HardCodedProgramParser();
                break;
            default:
                parser = new FileProgramParser();
                break;
        }

        if (parser is HardCodedProgramParser)
        {
            ProgramBox.Text = parser.SourceCode(e.AddedItems[0]?.ToString());
            return;
        }

        var topLevel = GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Program File",
            AllowMultiple = false
        });

        if (files.Count != 1) return;
        ProgramBox.Text = parser.SourceCode(files[0].Path.AbsolutePath);
    }
}