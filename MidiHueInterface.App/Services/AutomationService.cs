using MidiHueInterface.App.Interfaces;

namespace MidiHueInterface.App.Services;

public class AutomationService(ILightBulbRepository lightBulbRepository, IMidiEventRepository midiEventRepository)
    : IAutomationService
{
    private readonly ILightBulbRepository lightBulbRepository = lightBulbRepository;
    private readonly IMidiEventRepository midiEventRepository = midiEventRepository;
}