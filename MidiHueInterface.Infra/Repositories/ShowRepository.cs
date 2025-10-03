using CsvHelper;
using CsvHelper.Configuration;
using MidiHueInterface.App.Interfaces;
using MidiHueInterface.App.Models;
using static System.Globalization.CultureInfo;

namespace MidiHueInterface.Infra.Repositories;

public class ShowRepository : IShowRepository
{
    private readonly string filePath = Path.Join(Directory.GetCurrentDirectory(), "presets.csv");
    private readonly FileSystemWatcher watcher;
    private readonly Lock @lock = new ();

    public Dictionary<byte, PresetConfig> Presets { get; private set; } = new ();


    public ShowRepository()
    {
        this.watcher = new FileSystemWatcher
        {
            Path = Directory.GetCurrentDirectory(),
            Filter = Path.GetFileName(filePath),
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName
        };

        this.watcher.Changed += (_, _) =>
        {
            Task.Delay(500).ContinueWith(_ =>
            {
                lock (@lock)
                {
                    this.LoadDataFromFile();
                }
            });
        };
        this.watcher.EnableRaisingEvents = true;
        
        LoadDataFromFile();
    }


    private void LoadDataFromFile()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                this.Presets = new ();
                return;
            }

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(InvariantCulture)
            {
                HasHeaderRecord = true
            });
            
            this.Presets = csv.GetRecords<PresetConfig>().ToDictionary(p => p.ProgramNumber, p => p);
            
            Console.WriteLine("Presets loaded.");
        }
        catch (Exception e)
        {
            // Do nothing.
            Console.WriteLine("Error loading presets.");
            Console.WriteLine(e.Message);
        }
    }
}