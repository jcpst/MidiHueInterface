using MidiHueInterface.App.Interfaces;

namespace MidiHueInterface.App.Mediation;

public record ControlChangeMessage(byte Channel, byte ControlChangeNumber, byte Value) : IMessage;