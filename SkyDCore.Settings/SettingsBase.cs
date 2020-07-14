using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace SkyDCore.Settings
{
    /// <summary>
    /// 配置基类
    /// </summary>
    public abstract class SettingsBase
    {
        public SettingsBase()
        {
            Id = Guid.NewGuid();
            CreateTime = LastUpdateTime = DateTime.Now;
            Version = 1;

            BelongApplication = GetApplicationFilePath();
        }

        /// <summary>
        /// 获取应用文件路径
        /// </summary>
        /// <returns>应用文件路径</returns>
        protected static string GetApplicationFilePath()
        {
            var appfile = Process.GetCurrentProcess().MainModule.FileName;
            if (appfile.ToLower() == "dotnet.exe")
            {
                appfile = Assembly.GetEntryAssembly().Location;
            }
            return appfile;
        }

        /// <summary>
        /// 默认文件保存路径
        /// </summary>
        public static string DefaultSaveFilePath => AppContext.BaseDirectory.AsPathString().Combine($"{GetApplicationFilePath().AsPathString().FileName}.SkyDSettings");

        /// <summary>
        /// ID（首次创建时自动生成）
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 所属应用（首次创建时自动记录）
        /// </summary>
        public string BelongApplication { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后更新时间（每次保存时自动变化）
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 版本（每次保存时自动增加）
        /// </summary>
        public long Version { get; set; }

        /// <summary>
        /// 获取Json字符串
        /// </summary>
        /// <returns>Json字符串</returns>
        public virtual string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 保存配置到文件，使用Utf-8编码的Json存储。如果文件已存在，则会将旧文件加.backup后缀存为备份，如果备份文件也已存在则会先删除原先的备份文件
        /// </summary>
        /// <param name="filePath">文件路径，如果不存在，则使用DefaultSaveFilePath</param>
        public virtual void Save(string filePath = null)
        {
            Version++;
            LastUpdateTime = DateTime.Now;
            if (filePath == null)
            {
                filePath = DefaultSaveFilePath;
            }
            if (File.Exists(filePath))
            {
                var backupPath = filePath + ".backup";
                if (File.Exists(backupPath))
                {
                    File.Delete(backupPath);
                }
                File.Move(filePath, backupPath);
            }
            using (var sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.Write(GetJson());
            }
        }

        /// <summary>
        /// 读取配置自文件
        /// </summary>
        /// <typeparam name="T">配置文件类型</typeparam>
        /// <param name="filePath">文件路径，如果不存在，则使用DefaultSaveFilePath</param>
        /// <returns>配置文件对象</returns>
        public static T Load<T>(string filePath = null) where T : SettingsBase
        {
            if (filePath == null)
            {
                filePath = DefaultSaveFilePath;
            }
            using (var sr = new StreamReader(filePath, Encoding.UTF8))
            {
                return LoadByJson<T>(sr.ReadToEnd());
            }
        }

        /// <summary>
        /// 读取配置自Json字符串
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <returns>配置文件对象</returns>
        public static T LoadByJson<T>(string json) where T : SettingsBase
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 读取配置自文件，填充现有对象
        /// </summary>
        /// <typeparam name="T">配置文件类型</typeparam>
        /// <param name="obj">要填充的对象</param>
        /// <param name="filePath">文件路径，如果不存在，则使用DefaultSaveFilePath</param>
        public static void Load<T>(T obj, string filePath = null) where T : SettingsBase
        {
            if (filePath == null)
            {
                filePath = DefaultSaveFilePath;
            }
            using (var sr = new StreamReader(filePath, Encoding.UTF8))
            {
                LoadByJson(obj, sr.ReadToEnd());
            }
        }

        /// <summary>
        /// 读取配置自Json字符串，填充现有对象
        /// </summary>
        /// <param name="obj">要填充的对象</param>
        /// <param name="json">Json字符串</param>
        public static void LoadByJson<T>(T obj, string json) where T : SettingsBase
        {
            var serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            JsonConvert.PopulateObject(json, obj, serializerSettings);
        }

        /// <summary>
        /// 读取配置自文件，填充当前对象
        /// </summary>
        /// <param name="filePath">文件路径，如果不存在，则使用DefaultSaveFilePath</param>
        public virtual void Load(string filePath = null)
        {
            Load(this, filePath);
        }

        /// <summary>
        /// 读取配置自Json字符串，填充当前对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        public virtual void LoadByJson(string json)
        {
            LoadByJson(this, json);
        }
    }
}
