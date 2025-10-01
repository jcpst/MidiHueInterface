using MidiHueInterface.App.Models;

namespace MidiHueInterface.App.Interfaces;

public interface IShowRepository
{
    Dictionary<byte, PresetConfig> Presets { get; }
}