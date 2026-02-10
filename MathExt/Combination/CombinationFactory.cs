using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnet.common.MathExt

namespace dotnet.common.MathExt
{
    public class CombinationFactory
    {
        /**
        **/
        public static List<ResourceCombination> Create(Dictionary<long, List<Resource>> resourceGroupMap)
        {
            var count = GetCombinationCount(resourceGroupMap);
            int combinedCount = 1;
            Dictionary<long, List<ResourceCombination>> map = null;
            foreach (var groupID in resourceGroupMap.Keys)
            {
                var groupResources = resourceGroupMap[groupID];
                combinedCount *= groupResources.Count;
                if (map == null)
                {
                    map = new Dictionary<long, List<ResourceCombination>>();
                    foreach (var resource in groupResources)
                    {
                        var dailyDetails = new List<ResourceCombination>();
                        var toCombineCount = count / combinedCount;
                        for (int i = 0; i < toCombineCount; i++)
                        {
                            var selectedResource = Adapt(resource);
                            var dailyDetail = new ResourceCombination()
                            {
                                SelectedResources = new List<Resource>() { selectedResource }
                            };
                            dailyDetails.Add(dailyDetail);
                        }
                        map.Add(resource.ResourceID, dailyDetails);
                    }
                }
                else
                {
                    var nextMap = new Dictionary<long, List<ResourceCombination>>();
                    foreach (var resourceID in map.Keys)
                    {
                        var dailyDetails = map[resourceID];
                        int index = 0;
                        while (index < dailyDetails.Count)
                        {
                            foreach (var resource in groupResources)
                            {
                                var dailyDetail = dailyDetails[index++];
                                var selectedResource = Adapt(resource);
                                dailyDetail.SelectedResources.Add(selectedResource);
                                nextMap.AddItem(resource.GroupID, dailyDetail);
                            }
                        }
                    }
                }
            }

            var combinations = new List<ResourceCombination>();
            foreach (var resourceID in map.Keys)
            {
                combinations.AddRange(map[resourceID]);
            }
            return combinations;
        }

        private static int GetCombinationCount<TKey, TValue>(Dictionary<TKey, List<TValue>> resourceGroupMap)
        {
            int combinationCount = 1;
            foreach (var group in resourceGroupMap)
            {
                combinationCount *= group.Value.Count;
            }
            return combinationCount;
        }

        private static Resource Adapt(Resource resource)
        {
            var selectedResource = new Resource()
            {
                ResourceID = resource.ResourceID
            };
            return selectedResource;
        }
    }
}
