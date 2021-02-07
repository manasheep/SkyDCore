using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace SkyDCore.Settings
{
    /// <summary>
    /// 配置基类，该类实现了INotifyPropertyChanged接口
    /// </summary>
    public abstract class SettingsBase : INotifyPropertyChanged
    {
        public SettingsBase()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 使用此方法触发PropertyChanged事件。
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 使用此方法触发PropertyChanged事件。
        /// </summary>
        /// <param name="propertyNameArray">属性名称数组</param>
        protected virtual void OnPropertyChanged(params string[] propertyNameArray)
        {
            if (PropertyChanged != null)
            {
                foreach (var f in propertyNameArray)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(f));
                }
            }
        }

        /// <summary>
        /// 使用此方法触发PropertyChanged事件。
        /// </summary>
        ///<param name="sender">触发对象</param>
        ///<param name="e">参数</param>
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
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
        /// 默认配置文件保存路径
        /// </summary>
        public static string DefaultSaveFilePath => AppContext.BaseDirectory.AsPathString().Combine($"{GetApplicationFilePath().AsPathString().FileName}.SkyDSettings");

        /// <summary>
        /// 默认配置文件是否存在
        /// </summary>
        public static bool IsDefaultSaveFileExists => File.Exists(DefaultSaveFilePath);

        /// <summary>
        /// 获取Json字符串
        /// </summary>
        /// <param name="sortProperties">是否依据属性名进行排序</param>
        /// <returns>Json字符串</returns>
        public virtual string GetJson(bool sortProperties = true)
        {
            var settings = new JsonSerializerSettings()
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",   // 日期格式
                NullValueHandling = NullValueHandling.Include,  //包含空值
                Formatting = Formatting.Indented    //缩进
            };
            if (sortProperties)
            {
                settings.ContractResolver = new OrderedContractResolver();    //字段排序
            }
            return JsonConvert.SerializeObject(this, settings);
        }

        /// <summary>
        /// 保存配置到文件，使用Utf-8编码的Json存储。如果文件已存在，则会将旧文件加.backup后缀存为备份，如果备份文件也已存在则会先删除原先的备份文件
        /// </summary>
        /// <param name="filePath">文件路径，如果不存在，则使用DefaultSaveFilePath</param>
        public virtual void Save(string filePath = null)
        {
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

        /// <summary>
        /// 如果路径指向的文件已存在，则读取，否则会保存以创建之
        /// </summary>
        /// <param name="filePath">文件路径，如果不存在，则使用DefaultSaveFilePath</param>
        public virtual void LoadOrCreate(string filePath = null)
        {
            var path = filePath ?? DefaultSaveFilePath;
            if (File.Exists(path))
            {
                Load(path);
            }
            else
            {
                Save(path);
            }
        }
    }
}

