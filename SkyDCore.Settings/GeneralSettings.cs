using System;
using System.Collections.Generic;
using System.Text;

namespace SkyDCore.Settings
{
    /// <summary>
    /// 通用配置类，继承于SettingsBase
    /// </summary>
    public abstract class GeneralSettings : SettingsBase
    {
        public GeneralSettings()
        {
            Id = Guid.NewGuid();
            CreateTime = LastUpdateTime = DateTime.Now;
            Version = 1;

            BelongApplication = GetApplicationFilePath();
        }

        /// <summary>
        /// ID（首次创建时自动生成）。该属性支持INotifyPropertyChanged接口，在值更改时会自动调用本类或基类的OnPropertyChanged方法，继而触发PropertyChanged事件。
        /// </summary>
        public Guid Id
        {
            get { return _Id; }
            set
            {
                if (_Id == value) return;
                _Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private Guid _Id;

        /// <summary>
        /// 所属应用（首次创建时自动记录）。该属性支持INotifyPropertyChanged接口，在值更改时会自动调用本类或基类的OnPropertyChanged方法，继而触发PropertyChanged事件。
        /// </summary>
        public string BelongApplication
        {
            get { return _BelongApplication; }
            set
            {
                if (_BelongApplication == value) return;
                _BelongApplication = value;
                OnPropertyChanged(nameof(BelongApplication));
            }
        }
        private string _BelongApplication;

        /// <summary>
        /// 创建时间。该属性支持INotifyPropertyChanged接口，在值更改时会自动调用本类或基类的OnPropertyChanged方法，继而触发PropertyChanged事件。
        /// </summary>
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set
            {
                if (_CreateTime == value) return;
                _CreateTime = value;
                OnPropertyChanged(nameof(CreateTime));
            }
        }
        private DateTime _CreateTime;

        /// <summary>
        /// 最后更新时间（每次保存时自动变化）。该属性支持INotifyPropertyChanged接口，在值更改时会自动调用本类或基类的OnPropertyChanged方法，继而触发PropertyChanged事件。
        /// </summary>
        public DateTime LastUpdateTime
        {
            get { return _LastUpdateTime; }
            set
            {
                if (_LastUpdateTime == value) return;
                _LastUpdateTime = value;
                OnPropertyChanged(nameof(LastUpdateTime));
            }
        }
        private DateTime _LastUpdateTime;

        /// <summary>
        /// 版本（每次保存时自动增加）。该属性支持INotifyPropertyChanged接口，在值更改时会自动调用本类或基类的OnPropertyChanged方法，继而触发PropertyChanged事件。
        /// </summary>
        public long Version
        {
            get { return _Version; }
            set
            {
                if (_Version == value) return;
                _Version = value;
                OnPropertyChanged(nameof(Version));
            }
        }
        private long _Version;

        public override void Save(string filePath = null)
        {
            Version++;
            LastUpdateTime = DateTime.Now;
            base.Save(filePath);
        }
    }
}
