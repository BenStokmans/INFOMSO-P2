using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using INFOMSO_P2.Commands;
using INFOMSO_P2.Exercises;
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
            OutputBox.Text = ex.Message;
            return;
        }

        OutputBox.Text = "Program executed successfully\n";
        var character = ViewModel.Scene.GetCharacter();
        var goalString = ViewModel.Scene.GoalPosition != null ? ViewModel.Scene.GoalPosition == character.Position ? " goal reached" : "goal not reached" : "";
        OutputBox.Text += $"End state {character} - {goalString}\n";

        List<IMetricsCalculator> metrics =
        [
            new DepthMetricCalculator(),
            new RepeatMetricCalculator(),
            new NumberOfCmdsMetricCalculator(),
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

        var selection = (ProgramSelection.SelectedItem as ComboBoxItem)?.Content.ToString();

        IProgramParser parser;
        switch (selection)
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
            var exercise = (ExerciseBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            ProgramBox.Text = parser.SourceCode(selection + "-" + exercise);
            return;
        }

        TopLevel? topLevel = GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Program File",
            AllowMultiple = false
        });

        if (files.Count != 1) return;
        ProgramBox.Text = parser.SourceCode(files[0].Path.AbsolutePath);
    }

    private void ChangeExerciseSelection(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;
        if (ViewModel is null) return;

        var selection = (ExerciseBox.SelectedItem as ComboBoxItem)?.Content.ToString();
        ViewModel.Exercise = selection switch
        {
            "Shape" => new ShapeExercise(),
            "Pathfinding" => new PathfindingExercise(),
            _ => throw new ArgumentOutOfRangeException("exercise", "Invalid exercise selected")
        };
        ViewModel.ResetScene();
        OutputCanvas.InvalidateVisual();
    }
}