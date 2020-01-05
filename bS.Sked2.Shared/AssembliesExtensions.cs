using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace bS.Sked2.Shared
{
    public static class AssembliesExtensions
    {
        /// <summary>
        /// Gets the assemblies that contains classes implementing specified interface from one or more folders recursively (it look in folder and sub folders).
        /// </summary>
        /// <param name="folders">The folders.</param>
        /// <param name="fileNameScannerPattern">The file name scanner pattern. This is used to filter file name to load.<em>For example: 'bs.model.*.dll' matchs with all files taht start with 'bs.model.' and ends with '.dll'.</em> It supports regular expressions.</param>
        /// <param name="implementedInterface">The interface that has to be implemented in some class in assembly to load.</param>
        /// <returns>An IEnumerable<Assembly> of assembly that implements the specified interface loaded from specified folders.</returns>
        public static IEnumerable<Assembly> GetAssembliesFromFolders(string[] folders, string[] fileNameScannerPattern, Type implementedInterface, ref string warnings)
        {
            var candidateFiles = new List<string>();
            IEnumerable<string> dllsToLoad;
            var resultantAssemblies = new Dictionary<string, Assembly>();

            // For every folder
            if (folders != null)
            {
                foreach (var folder in folders)
                {
                    // add candidated file
                    candidateFiles.AddRange(Directory.EnumerateFiles(folder, "*.dll", SearchOption.AllDirectories));
                }
            }

            // If desired filter the candidated file via specified patterns using regular expressions
            if (fileNameScannerPattern != null)
            {
                dllsToLoad = candidateFiles
                      .Where(filename => fileNameScannerPattern.Any(pattern => Regex.IsMatch(filename, pattern)));
            }
            else dllsToLoad = candidateFiles;

            // Load all the assemblies
            var allAssemblies = dllsToLoad
                    .Select(Assembly.LoadFrom);

            // Filter the only assemblies that implements specified interface in some class
            var desiredAssemblies = (from a in allAssemblies
                                     from t in a.GetTypes()
                                     where implementedInterface.IsAssignableFrom(t) && t.IsClass
                                     select a).Distinct();

            // Avoid to load same assemblies more than one time
            foreach (var assembly in desiredAssemblies)
            {
                if (!resultantAssemblies.ContainsKey(assembly.FullName))
                {
                    resultantAssemblies.Add(assembly.FullName, assembly);
                }
                else
                {
                    // warnings
                    warnings += $"Assembly '{assembly.FullName}' has been loaded yet (Path: '{assembly.Location}').\n";
                }
            }

            return resultantAssemblies.Select(x => x.Value);
        }

        public static IEnumerable<Type> GetTypeFromAssembly(Assembly assembly, Type implementedInterface)
        {
            var types = assembly.GetTypes()
                .Where(p => implementedInterface.IsAssignableFrom(p));
            return types;
        }

        /// <summary>
        /// Gets the types implementing interface or base class.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface or base class that types implement.</typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesImplementingInterface<TInterface>()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.FullName.Contains("Microsoft"));
            var types = assemblies.SelectMany(a => a.GetTypes())
                .Where(a => typeof(TInterface).IsAssignableFrom(a));
            return types;
        }
    }
}