using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using INFOMSO_P3.Commands;
using INFOMSO_P3.Exercises;
using INFOMSO_P3.Game;
using INFOMSO_P3.Gui.ViewModels;
using INFOMSO_P3.Metrics;

// ReSharper disable UnusedParameter.Local

namespace INFOMSO_P3.Gui;

public partial class MainWindow : Window
{
    private MainWindowViewModel? ViewModel => DataContext as MainWindowViewModel;

    private readonly List<IMetricsCalculator> _metrics =
    [
        new DepthMetricCalculator(),
        new RepeatMetricCalculator(),
        new NumberOfCmdsMetricCalculator()
    ];

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
            program = parser.Parse(ProgramBox.Text ?? string.Empty);
            program.Run(ViewModel.Scene);
        }
        catch (Exception ex)
        {
            OutputBox.Text = ex.Message;
            return;
        }

        OutputBox.Text = "Program executed successfully\n";
        Character character = ViewModel.Scene.GetCharacter();
        string goalString = ViewModel.Scene.GoalPosition != null ? ViewModel.Scene.GoalPosition == character.Position ? " goal reached" : "goal not reached" : "";
        OutputBox.Text += $"End state {character} - {goalString}\n";

        OutputBox.Text += ViewModel.Exercise.IsCompleted(ViewModel.Scene) ? "Exercise completed\n" : "Exercise not completed\n";

        OutputBox.Text += "\nMetrics:\n";
        foreach (IMetricsCalculator metric in _metrics)
            OutputBox.Text += $"{metric.CalculateMetrics(program)}\n";

        OutputCanvas.InvalidateVisual();
    }

    private async void ChangeProgramSelection(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;
        if (ViewModel is null) return;

        var selection = (ProgramSelection.SelectedItem as ComboBoxItem)?.Content?.ToString();

        IProgramParser parser = selection switch
        {
            "Basic" => new HardCodedProgramParser(),
            "Advanced" => new HardCodedProgramParser(),
            "Expert" => new HardCodedProgramParser(),
            _ => new FileProgramParser()
        };

        if (parser is HardCodedProgramParser)
        {
            var exercise = (ExerciseBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (exercise == "From file...") return;

            try
            {
                ProgramBox.Text = parser.SourceCode(selection + "-" + exercise);
            }
            catch
            {
                // ignored
            }

            return;
        }

        TopLevel? topLevel = GetTopLevel(this);
        var files = await topLevel?.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Program File",
            AllowMultiple = false
        })!;

        if (files.Count != 1) return;
        ProgramBox.Text = parser.SourceCode(files[0].Path.AbsolutePath);
    }

    private async void ChangeExerciseSelection(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;
        if (ViewModel is null) return;

        var selection = (ExerciseBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

        var source = "";
        if (selection == "From file...")
        {
            TopLevel? topLevel = GetTopLevel(this);
            var files = await topLevel?.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open Exercise File",
                AllowMultiple = false
            })!;

            if (files.Count != 1) return;
            source = files[0].Path.AbsolutePath;
        }

        ViewModel.Exercise = selection switch
        {
            "Shape" => new ShapeExercise(),
            "Pathfinding" => new PathfindingExercise(),
            "From file.." => new FileExcercise(source),
            _ => throw new Exception("Invalid exercise selected")
        };
        ViewModel.ResetScene();
        OutputCanvas.InvalidateVisual();
    }
}