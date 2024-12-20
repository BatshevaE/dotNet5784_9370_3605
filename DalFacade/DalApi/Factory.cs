﻿namespace DalApi;
using static DalApi.Config;
using System.Reflection;
/// <summary>
/// The class contains a method called Get that produces/returns an initialization of the corresponding class DalList or DalXml,
/// according to what is written in the configuration file dal-config.xml which it knows how to read with the help of the Config class.
/// </summary>
public static class Factory
{
    /// <summary>
    /// the method get
    /// </summary>
    public static IDal Get
    {
        get
        {
            string dalType = s_dalName ?? throw new DalConfigException($"DAL name is not extracted from the configuration");
            DalImplementation dal = s_dalPackages[dalType] ?? throw new DalConfigException($"Package for {dalType} is not found in packages list in dal-config.xml");

            try { Assembly.Load(dal.Package ?? throw new DalConfigException($"Package {dal.Package} is null")); }
            catch (Exception ex) { throw new DalConfigException($"Failed to load {dal.Package}.dll package", ex); }

            Type type = Type.GetType($"{dal.Namespace}.{dal.Class}, {dal.Package}") ??
                throw new DalConfigException($"Class {dal.Namespace}.{dal.Class} was not found in {dal.Package}.dll");

            return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) as IDal ??
                throw new DalConfigException($"Class {dal.Class} is not a singleton or wrong property name for Instance");
        }
    }
}
