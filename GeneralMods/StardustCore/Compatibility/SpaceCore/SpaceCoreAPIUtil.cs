using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;

namespace StardustCore.Compatibility.SpaceCore
{
    public static class SpaceCoreAPIUtil
    {

        public static SpaceCoreAPI GetMappedSpaceCoreAPI()
        {
            var spaceCore = ModCore.ModHelper.ModRegistry.GetApi<SpaceCoreAPI>("spacechase0.SpaceCore");
            return spaceCore;
        }

        public static void RegisterCustomEventCommand(string Command, Action<Event,GameLocation,GameTime,string[]> Method)
        {
            SpaceCoreAPI spaceCore = GetMappedSpaceCoreAPI();
            spaceCore.AddEventCommand(Command, Method.Method);
        }

        public static void RegisterTypeForSerializer(Type t)
        {
            SpaceCoreAPI spaceCoreAPI = GetMappedSpaceCoreAPI();
            spaceCoreAPI.RegisterSerializerType(t);
        }

        public static void RegisterTypesForMod(IMod modBase)
        {
            SpaceCoreAPI spaceCoreAPI = GetMappedSpaceCoreAPI();
            foreach (Type t in modBase.GetType().Assembly.GetTypes())
            {
                if (Attribute.GetCustomAttribute(t, typeof(XmlTypeAttribute)) != null)
                {
                    RegisterTypeForSerializer(t);
                }

            }
        }

    }
}
