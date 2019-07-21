using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace bS.Sked2.Repository
{
    public sealed class SessionFactoryInitializer
    {
        private static readonly string[] fileNameScannerPattern = { "bS.Sked2.Model.dll", "bS.Sked2.Module.*.dll" };

        private static List<Assembly> GetORMMappingAssemblies(string[] foldersWhereLookingForDll, bool useCurrentdirectoryToo)
        {
            var currentDirectory = "";
            var candidateFiles = new List<string>();

            if (useCurrentdirectoryToo)
            {
                currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                //log.Trace($"Looking for dlls to map in directory: {currentDirectory}.");
                candidateFiles.AddRange(Directory.EnumerateFiles(currentDirectory, "*.dll", SearchOption.AllDirectories));
            }

            if (foldersWhereLookingForDll != null)
            {
                foreach (var folder in foldersWhereLookingForDll)
                {
                    candidateFiles.AddRange(Directory.EnumerateFiles(folder, "*.dll", SearchOption.AllDirectories));
                }
            }

            //log.Trace($"The following dlls wil be evaluated for dinamically mapping in ORM:\n{string.Join("\n", candidateFiles)}");

            var resultantAssemblies = new Dictionary<string, Assembly>();
            var allAssemblies = candidateFiles
                      .Where(filename => fileNameScannerPattern.Any(pattern => Regex.IsMatch(filename, pattern)))
                      .Select(Assembly.LoadFrom);

            foreach (var assembly in allAssemblies)
            {
                if (!resultantAssemblies.ContainsKey(assembly.FullName)) resultantAssemblies.Add(assembly.FullName, assembly);
                else resultantAssemblies[assembly.FullName] = assembly;
            }

            //log.Trace($"The following {resultantAssemblies.Count} assemblies will be loaded dinamically and mapped in ORM:\n{string.Join("\n", resultantAssemblies.Select(x => x.Key))}");
            return resultantAssemblies.Select(x => x.Value).ToList();
        }


        private static readonly Dictionary<string, ISessionFactory> FactoryDictionary;

        static SessionFactoryInitializer()
        {
            FactoryDictionary = new Dictionary<string, ISessionFactory>();
        }

        private static ISessionFactory CreateNewFactory(string connectionString, string[] foldersWereLookingForDll, string dbType)
        {
            ISessionFactory lResult = null;
            try
            {
                var MappingAssemblies = GetORMMappingAssemblies(foldersWereLookingForDll, true);

                switch (dbType.ToLower())
                {
                    case "mysql":
                        lResult = Fluently.Configure()
                            .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
                            .Mappings(m => MappingAssemblies.ForEach(a => m.FluentMappings.AddFromAssembly(a)))
                            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                            .BuildSessionFactory();
                        break;
                    case "sqlite":
                        lResult = Fluently.Configure()
                            .Database(SQLiteConfiguration.Standard.ConnectionString(connectionString))
                            .Mappings(m => MappingAssemblies.ForEach(a => m.FluentMappings.AddFromAssembly(a)))
                            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                            .BuildSessionFactory();
                        break;
                    case "sql2012":
                        lResult = Fluently.Configure()
                            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                            .Mappings(m => MappingAssemblies.ForEach(a => m.FluentMappings.AddFromAssembly(a)))
                            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                            .BuildSessionFactory();
                        break;
                    case "sql20008":
                        lResult = Fluently.Configure()
                            .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
                            .Mappings(m => MappingAssemblies.ForEach(a => m.FluentMappings.AddFromAssembly(a)))
                            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                            .BuildSessionFactory();
                        break;
                }

            }
            catch (Exception ex)
            {
                {
                    //log.Fatal($"Exception creating ORM SessionFactory: {ex.GetBaseException().Message}", ex);
                    throw;
                }
            }

            return lResult;
        }

        public static ISessionFactory GetFactory(string connectionString, string dbType, string[] foldersWereLookingForDll)
        {
            lock (typeof(SessionFactoryInitializer))
            {
                if (!FactoryDictionary.ContainsKey(connectionString))
                {
                    FactoryDictionary.Add(connectionString, CreateNewFactory(connectionString, foldersWereLookingForDll, dbType));
                    //log.Trace($"New 'Factory Session Builder' has been created for the Connection String: {connectionString}");
                }
                else
                {
                    //log.Trace($"Existing 'Factory Session Builder' has been found for the Connection String: {connectionString}");
                }

                return FactoryDictionary[connectionString];
            }
        }
    }
}
