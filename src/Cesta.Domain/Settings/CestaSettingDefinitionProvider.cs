using Volo.Abp.Settings;

namespace Cesta.Settings;

public class CestaSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CestaSettings.MySetting1));
    }
}
