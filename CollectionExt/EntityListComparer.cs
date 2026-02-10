using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.CollectionExt{

    /**
    * 
    **/
    public class EntityListComparer
    {
        /**
        *  新旧数据集比较，找出差集{New-Old}，方便去插入数据库
        **/
        public static List<TNew> FindToInsert<TOld, TNew>(List<TOld> oldList, Func<TOld, string> getOldKey, List<TNew> newList, Func<TNew, string> getNewKey)
        {
            if (oldList == null || oldList.Count == 0)
            {
                return newList;
            }

            var oldKeys = new HashSet<string>();
            foreach (var entty in oldList)
            {
                oldKeys.Add(getOldKey(entty));
            }

            var toInsert = new List<TNew>();
            foreach (var entity in newList)
            {
                if (oldKeys.Contains(getNewKey(entity)))
                {
                    continue;
                }
                toInsert.Add(entity);
            }
            return toInsert;
        }


        /**
        *  新旧数据集比较，交集和各自差集
        **/
        public EntityListChange<T> FindChange(List<T> newPOs, List<T> oldPOs, Func<T, int> getKey, Action<T, T> updateNewPO, Func<T, T, bool> equal)
        {
            var listChange = new EntityListChange<T>();
            
            if (oldPOs == null || oldPOs.Count == 0)
            {
                listChange.ToInsert = newPOs;
            }
            else
            {
                var newPOMap = newPOs.ToDictionary(getKey);
                var oldPOMap = oldPOs.ToDictionary(getKey);

                foreach (var newPO in newPOs)
                {
                    var key = getKey(newPO);
                    T oldPO;
                    if (oldPOMap.TryGetValue(key, out oldPO))
                    {
                        if (!equal(newPO, oldPO))
                        {
                            if (updateNewPO != null)
                            {
                                updateNewPO(newPO, oldPO);
                            }
                            listChange.ToUpdate.Add(newPO);
                        }
                    }
                    else
                    {
                        listChange.ToInsert.Add(newPO);
                    }
                }

                foreach (var oldPO in oldPOs)
                {
                    if (!newPOMap.ContainsKey(getKey(oldPO)))
                    {
                        listChange.ToDelete.Add(oldPO);
                    }
                }
            }

            return listChange;
        }
    }
}