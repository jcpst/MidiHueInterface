using MidiHueInterface.App.Interfaces;

namespace MidiHueInterface.App.Models;

public record ProgramChangeMessage(byte Channel, byte ProgramNumber) : IMessage;