using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    /// <summary>
    /// web服务器站点信息
    /// </summary>
    public class WebServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("station", IsRequired = true)]
        public string Station
        {
            get
            {
                return this["station"].ToString();
            }
            set
            {
                this["station"] = value;
            }
        }

        [ConfigurationProperty("servicePath", IsRequired = true)]
        public string ServicePath
        {
            get
            {
                return this["servicePath"].ToString();
            }
            set
            {
                this["servicePath"] = value;
            }
        }

        [ConfigurationProperty("backupPath", IsRequired = true)]
        public string BackupPath
        {
            get
            {
                return this["backupPath"].ToString();
            }
            set
            {
                this["backupPath"] = value;
            }
        }

        [ConfigurationProperty("deployPackPath", IsRequired = true)]
        public string DeployPackPath
        {
            get
            {
                return this["deployPackPath"].ToString();
            }
            set
            {
                this["deployPackPath"] = value;
            }
        }

        [ConfigurationProperty("group", IsRequired = true)]
        public string Group
        {
            get
            {
                return this["group"].ToString();
            }
            set
            {
                this["group"] = value;
            }
        }
    }

    /// <summary>
    /// 应用和程序池名称
    /// </summary>
    public class ApplicationElement : ConfigurationElement
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

        [ConfigurationProperty("appPool", IsRequired = true)]
        public string AppPool
        {
            get
            {
                return this["appPool"].ToString();
            }
            set
            {
                this["appPool"] = value;
            }
        }
    }

    /// <summary>
    /// 压缩时要排除的项
    /// </summary>
    public class ExceptItemsElement : ConfigurationElement
    {
        [ConfigurationProperty("items", IsRequired = true)]
        public string Items
        {
            get
            {
                return this["items"].ToString();
            }
            set
            {
                this["items"] = value;
            }
        }
    }

    /// <summary>
    /// 上传的部署包名称和路径
    /// </summary>
    public class DeployPackElement : ConfigurationElement
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
    /// Web站点集合
    /// </summary>
    public class WebServicesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new WebServiceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as WebServiceElement).Station;
        }

        protected override string ElementName
        {
            get
            {
                return "webService";
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        private Dictionary<string, WebServiceElement> settings = null;
        public Dictionary<string, WebServiceElement> Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = new Dictionary<string, WebServiceElement>();
                    foreach (WebServiceElement item in this)
                    {
                        settings.Add(item.Station, item);
                    }
                }
                return settings;
            }
        }
    }

    /// <summary>
    /// 服务器列表节点
    /// </summary>
    public class ServicesSection : ConfigurationSection
    {
        [ConfigurationProperty("application", IsRequired = true)]
        public ApplicationElement Application
        {
            get
            {
                return this["application"] as ApplicationElement;
            }
        }

        [ConfigurationProperty("exceptItems", IsRequired = true)]
        public ExceptItemsElement ExceptItems
        {
            get
            {
                return this["exceptItems"] as ExceptItemsElement;
            }
        }

        [ConfigurationProperty("deployPack", IsRequired = true)]
        public DeployPackElement deployPack
        {
            get
            {
                return this["deployPack"] as DeployPackElement;
            }
        }

        [ConfigurationProperty("webServices", IsRequired = true)]
        public WebServicesCollection WebServices
        {
            get
            {
                return this["webServices"] as WebServicesCollection;
            }
        }
    }



}
