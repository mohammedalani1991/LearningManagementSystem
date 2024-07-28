using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LearningManagementSystem.Services.General;
using DataEntity.Models.EfModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LearningManagementSystem.Services.Helpers
{
    public class DataBaseScriptsHelper
    {
        private const string ComponentName = "HandleDataBaseScripts";
        private const string UserInfo = "System";
        private const string DataBaseScriptsAssemplyName = "DataEntity";

        public static void HandleDataBaseScripts()
        {
            using (var db = new LearningManagementSystemContext())
            {
                Assembly assembly = Assembly.Load(DataBaseScriptsAssemplyName);

                // Step 1: Get the database scripts under DataEntity.DataBase.Scripts
                List<string> scriptsEmbeddedResourcesList = LoadDataBaseScripts(assembly);

                // Step 2: Filter the database scripts and get only the scripts that we want to excute.
                List<string> scriptsToExcute = GetDataBaseScriptstoExecute(scriptsEmbeddedResourcesList, db);

                // Step 3: Excute the targeted scripts which is came from step 2.
                ExecuteDataBaseScripts(assembly, scriptsToExcute, db);
            }
        }

        private static List<string> LoadDataBaseScripts(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new Exception("Can't load the target assembly that contains the database scripts. Please check the assembly name.");
            }

            string[] embeddedResources = assembly.GetManifestResourceNames();
            var scriptsEmbeddedResourcesList = new List<string>(embeddedResources).Where(r => r.StartsWith("DataEntity.Data.AutoExcutedScripts") && r.Contains(".sql")).OrderBy(s => s).ToList();

            if (scriptsEmbeddedResourcesList.Count <= 0)
            {
                //SystemLog log = new SystemLog
                //{
                //    Name = "The scripts embedded resources is null or empty skip the database scripts handler",
                //    CreatedOn = DateTime.Now,
                //    CreatedBy = UserInfo,
                //    Component = ComponentName
                //};
                //LogHelper.AddSystemLog(log);
                return new List<string>();
            }

            return scriptsEmbeddedResourcesList;
        }

        private static List<string> GetDataBaseScriptstoExecute(List<string> scriptsEmbeddedResourcesList, LearningManagementSystemContext db)
        {
            string lastScriptSetting = "System Last Script";
            var lastDatabaseScriptSetting = new SettingService(db).GetOrCreate(lastScriptSetting, string.Empty);
            if (lastDatabaseScriptSetting == null)
            {

                SystemLog log = new SystemLog();
                log.Name = "The last database script setting is null.";
                log.CreatedOn = DateTime.Now;
                log.CreatedBy = UserInfo;
                log.Component = ComponentName;
                LogHelper.AddSystemLog(log);

                LogHelper.AddSystemLog(log);
                throw new Exception("The last database script setting is null");
            }

            if (!string.IsNullOrWhiteSpace(lastDatabaseScriptSetting.Value) && !string.IsNullOrEmpty(lastDatabaseScriptSetting.Value))
            {
                foreach (string scriptName in scriptsEmbeddedResourcesList)
                {
                    db.DataBaseScripts.Add(new DataBaseScript
                    {
                        Name = scriptName,
                        CreatedOn = DateTime.Now,
                        CreatedBy = "System",
                        Status = 1,
                    });

                    if (scriptName.Contains(lastDatabaseScriptSetting.Value))
                    {
                        break;
                    }
                }

                new SettingService(db).SetSettingValue(lastScriptSetting, string.Empty);
                db.SaveChanges();
            }

            List<DataBaseScript> dataBaseScripts = db.DataBaseScripts.ToList();
            var scriptsToExcuteList = scriptsEmbeddedResourcesList.Where(all => !dataBaseScripts.Any(sub => sub.Name.Contains(all))).ToList();

            return scriptsToExcuteList;
        }

        private static void ExecuteDataBaseScripts(Assembly assembly, List<string> scriptsEmbeddedResourcesList, LearningManagementSystemContext db)
        {
            foreach (var embeddedResourceName in scriptsEmbeddedResourcesList)
            {
                using (Stream stream = assembly.GetManifestResourceStream(embeddedResourceName))
                using (var reader = new StreamReader(stream))
                {
                    using (IDbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            string result = reader.ReadToEnd();
                            //var resultdatabase = db.Database.ExecuteSqlRaw(result);

                            db.DataBaseScripts.Add(new DataBaseScript
                            {
                                Name = embeddedResourceName,
                                CreatedOn = DateTime.Now,
                                CreatedBy = "System",
                                Status = 1,
                            });

                            db.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            LogHelper.LogException(UserInfo, e, ComponentName);
                            dbContextTransaction.Rollback();
                            throw new Exception("Can't start the application. An error occured while attempting to run the database scripts.");
                        }
                    }
                }
            }
        }
    }
}
