using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    /// <summary>
    /// 系统压缩工具路径
    /// </summary>
    public class WinrarPathElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = true)]
        public string Path
        {
            get
            {
                return this["path"].ToString();
            }
            set
            {
                this["path"] = value;
            }
        }
    }

    /// <summary>
    /// 压缩和解压的命令
    /// </summary>
    public class WinrarArgElement : ConfigurationElement
    {
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

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                return this["value"].ToString();
            }
            set
            {
                this["value"] = value;
            }
        }
    }

    /// <summary>
    /// 压缩和解压命令列表
    /// </summary>
    public class WinrarArgsCollection : ConfigurationElementCollection
    {
        #region override
        protected override ConfigurationElement CreateNewElement()
        {
            return new WinrarArgElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as WinrarArgElement).Name;
        }

        protected override string ElementName
        {
            get
            {
                return "winrarArg";//TableElement在配置中的名称
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }
        #endregion

        #region 节点元素集合
        private Dictionary<string, string> settings = null;
        public Dictionary<string, string> Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = new Dictionary<string, string>();
                    foreach (WinrarArgElement item in this)
                    {
                        settings.Add(item.Name, item.Value);
                    }
                }
                return settings;
            }
        }
        #endregion
    }

    /// <summary>
    /// 压缩文件名后缀
    /// </summary>
    public class WinrarNameSuffixElement : ConfigurationElement
    {
        [ConfigurationProperty("nameSuffix", IsRequired = true)]
        public string NameSuffix
        {
            get
            {
                return this["nameSuffix"].ToString();
            }
            set
            {
                this["nameSuffix"] = value;
            }
        }
    }

    /// <summary>
    /// 压缩工具节点
    /// </summary>
    public class WinrarSection : ConfigurationSection
    {
        [ConfigurationProperty("winrarPath", IsRequired = true)]
        public WinrarPathElement WinrarPath
        {
            get
            {
                return this["winrarPath"] as WinrarPathElement;
            }
        }

        [ConfigurationProperty("winrarArgs", IsRequired = true)]
        public WinrarArgsCollection WinrarArgs
        {
            get
            {
                return this["winrarArgs"] as WinrarArgsCollection;
            }
        }

        [ConfigurationProperty("winrarNameSuffix", IsRequired = true)]
        public WinrarNameSuffixElement WinrarNameSuffix
        {
            get
            {
                return this["winrarNameSuffix"] as WinrarNameSuffixElement;
            }
        }
    }
}
