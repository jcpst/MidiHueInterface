using MidiHueInterface.App.Interfaces;

namespace MidiHueInterface.App.Mediation;

public record ProgramChangeMessage(byte Channel, byte ProgramNumber) : IMessage;