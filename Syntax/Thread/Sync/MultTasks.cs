using CommonLib;
using I200_WechatAccountStatementService.PO;
using I200_WechatAccountStatementService.Repository;
using MySoft.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;
using I200_WechatAccountStatementService.Log.FileAppender;

namespace I200_WechatAccountStatementService
{
    /// <summary>
    /// 对账单发送服务
    /// </summary>
    public class AccountStatementService
    {
        public readonly string WechatNotifyAddress = ConfigurationManager.AppSettings["WechatNotifyAddress"];
        private readonly int MaxParallelTaskCount = int.Parse(ConfigurationManager.AppSettings["MaxParallelTaskCount"]);
        private readonly ConcurrentQueue<AccountEntity> accountQueue = new ConcurrentQueue<AccountEntity>();

        public bool NotifyWechat()
        {
            var accounts = AccountRepository.GetWechatBiddingAccounts();
            if (accounts != null && accounts.Any())
            {
                foreach (var account in accounts)
                {
                    accountQueue.Enqueue(account);
                }
                NotifyFromQueue();
            }
            return true;
        }

        private void NotifyFromQueue()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var tasks = new List<Task>();
            AccountEntity account;
            while (accountQueue.TryDequeue(out account))
            {
                var taskAccount = account;
                var task = Task.Run(() =>
                {
                    Notify(taskAccount);
                });
                tasks.Add(task);
                if (tasks.Count == MaxParallelTaskCount)
                {
                    Task.WaitAll(tasks.ToArray());
                    stopwatch.Stop();
                    LogPool.Write(new LogFileMessage()
                    {
                        MessageContent = $"并行推送 耗时 = {stopwatch.ElapsedMilliseconds}(ms)",
                        MessageGroup = "NotifyFromQueue",
                        MessageType = Log.FileAppender.LogType.Info,
                        Timestamp = DateTime.Now
                    });
                    stopwatch.Restart();
                    tasks.Clear();
                }
            }

            if (tasks.Count > 0)
            {
                Task.WaitAll(tasks.ToArray());
                stopwatch.Stop();
                LogPool.Write(new LogFileMessage()
                {
                    MessageContent = $"并行推送 耗时 = {stopwatch.ElapsedMilliseconds}(ms)",
                    MessageGroup = "NotifyFromQueue",
                    MessageType = Log.FileAppender.LogType.Info,
                    Timestamp = DateTime.Now
                });
            }
        }

        private void Notify(AccountEntity account)
        {
            try
            {
                var date = DateTime.Now.Date;
                var parameters = new Dictionary<string, string>() {
                        { "AccountId", account.ID.ToString()},
                        { "AccountName", account.CompanyName},
                        { "OpenId", account.weixin_openid},
                        { "BillStartDate", date.ToString()},
                        { "BillEndDate", date.AddDays(1).AddSeconds(-1).ToString()},
                        { "Type", string.Empty},
                        { "BillDateDesc", date.ToString("yyyy/MM/dd")},
                    };

                var result = CommonHelper.RestPost(WechatNotifyAddress, null, parameters);
            }
            catch (Exception ex)
            {
                SimpleLog.Instance.WriteLogForFile(account.ID + "发送微信对账单异常：", ex.Message);
            }
        }
    }
}
