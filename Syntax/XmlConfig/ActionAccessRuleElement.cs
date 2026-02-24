using System;
using System.Configuration;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ActionAccessRuleElement : ConfigurationElement
    {
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }

        /// <summary>
        /// 日访问上限
        /// </summary>
        [ConfigurationProperty("upperLimit", IsRequired = true)]
        public string ConfigUpperLimit
        {
            get
            {
                return this["upperLimit"].ToString();
            }
            set
            {
                this["upperLimit"] = value;
            }
        }

        private long? _upperLimit = null;

        /// <summary>
        /// 日访问上限
        /// </summary>
        public long? UpperLimit
        {
            get
            {
                if (_upperLimit == null && !string.IsNullOrEmpty(ConfigUpperLimit))
                {
                    long value;
                    if (long.TryParse(ConfigUpperLimit, out value))
                    {
                        _upperLimit = value;
                    }
                }
                return _upperLimit;
            }
        }

        /// <summary>
        /// 访问标识
        /// </summary>
        [ConfigurationProperty("identity", IsRequired = false)]
        public string ConfigIdentity
        {
            get
            {
                return this["identity"].ToString();
            }
            set
            {
                this["identity"] = value;
            }
        }

        private string[] _identities;

        /// <summary>
        /// 访问标识
        /// </summary>
        public string[] Identities
        {
            get
            {
                if (_identities == null)
                {
                    if (!string.IsNullOrEmpty(ConfigIdentity))
                    {
                        _identities = ConfigIdentity.ToLower().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                }
                return _identities;
            }
            set
            {
                _identities = value;
            }
        }

        /// <summary>
        /// 访问拦截模式
        /// </summary>
        [ConfigurationProperty("mode", IsRequired = false)]
        public string ConfigWatchMode
        {
            get
            {
                return this["mode"].ToString();
            }
            set
            {
                this["mode"] = value;
            }
        }

        /// <summary>
        /// 访问拦截模式
        /// </summary>
        public WatchModeEnum WatchMode { get; set; }
    }
}
