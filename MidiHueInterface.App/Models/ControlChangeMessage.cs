using MidiHueInterface.App.Interfaces;

namespace MidiHueInterface.App.Models;

public record ControlChangeMessage(byte Channel, byte ControlChangeNumber, byte Value) : IMessage;