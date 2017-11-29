using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;

namespace ModernRonin.Terrarium.Client.Windows
{
    public static class AutofacExtensions
    {
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> EndingWith(
            this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> self,
            string postfix)
        {
            return self.Where(t => t.Name.EndsWith(postfix));
        }
    }
}