using MidiHueInterface.App.Interfaces;

namespace MidiHueInterface.App.Services;

public class AutomationService(ILightBulbRepository lightBulbRepository)
    : IAutomationService
{
    private readonly ILightBulbRepository lightBulbRepository = lightBulbRepository;
}