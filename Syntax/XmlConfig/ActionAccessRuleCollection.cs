using System.Collections.Generic;
using System.Configuration;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ActionAccessRuleCollection : ConfigurationElementCollection
    {
        #region override
        protected override ConfigurationElement CreateNewElement()
        {
            return new ActionAccessRuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ActionAccessRuleElement).Name;
        }

        protected override string ElementName
        {
            get
            {
                return "action";
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
        private Dictionary<string, ActionAccessRuleElement> settings = null;
        public Dictionary<string, ActionAccessRuleElement> Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = new Dictionary<string, ActionAccessRuleElement>();
                    foreach (ActionAccessRuleElement item in this)
                    {
                        settings.Add(item.Name, item);
                    }
                }
                return settings;
            }
        }
        #endregion
    }
}
