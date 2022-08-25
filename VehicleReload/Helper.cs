using Base.Core;
using Base.Defs;
using PhoenixPoint.Common.Core;
using System;
using System.Linq;


namespace VehicleReload
{
    internal class Helper
    {
        private static readonly DefRepository Repo = VehicleReloadMain.Repo;
        private static readonly SharedData Shared = VehicleReloadMain.Shared;

       
        public static T CreateDefFromClone<T>(T source, string guid, string name) where T : BaseDef
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            try
            {
                if (Repo.GetDef(guid) != null)
                {
                    if (!(Repo.GetDef(guid) is T tmp))
                    {
                        throw new TypeAccessException($"An item with the GUID <{guid}> has already been added to the Repo, but the type <{Repo.GetDef(guid).GetType().Name}> does not match <{typeof(T).Name}>!");
                    }
                    else
                    {
                        if (tmp != null)
                        {
                            return tmp;
                        }
                    }
                }
                T tmp2 = Repo.GetRuntimeDefs<T>(true).FirstOrDefault(rt => rt.Guid.Equals(guid));
                if (tmp2 != null)
                {
                    return tmp2;
                }
                Type type = null;
                string resultName = "";
                if (source != null)
                {
                    int start = source.name.IndexOf('[') + 1;
                    int end = source.name.IndexOf(']');
                    string toReplace = !name.Contains("[") && start > 0 && end > start ? source.name.Substring(start, end - start) : source.name;
                    resultName = source.name.Replace(toReplace, name);
                }
                else
                {
                    type = typeof(T);
                    resultName = name;
                }
                T result = (T)Repo.CreateRuntimeDef(
                    source,
                    type,
                    guid);
                result.name = resultName;
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }             
    }
}
