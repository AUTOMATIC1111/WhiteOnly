using HarmonyLib;
using System.Reflection;
using Verse;

namespace WhiteOnly
{
    [StaticConstructorOnStartup]
    public class WhiteOnly
    {

        static WhiteOnly()
        {
            var harmony = new Harmony("com.github.automatic1111.whiteonly");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
