using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Grabacr07.KanColleWrapper;
using Nekoxy;
using Livet;

namespace SaveResponsePlugin
{
    internal class ResponseFileWriter : NotificationObject
    {
        #region Log変更通知プロパティ
        private DispatcherCollection<string> _Log;

        public DispatcherCollection<string> Log
        {
            get
            { return this._Log; }
            set
            { 
                if (this._Log == value)
                    return;
                this._Log = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion

        public ResponseFileWriter(KanColleProxy proxy)
        {
            this.Log = new DispatcherCollection<string>(DispatcherHelper.UIDispatcher);

            //kcsとkcsapi以下を保存。キャッシュにある奴は保存されない。
            var kscSessionSource = proxy.SessionSource
                .Where(s => s.Request.PathAndQuery.StartsWith("/kcs/")
                        || s.Request.PathAndQuery.StartsWith("/kcs2/")
                        || s.Request.PathAndQuery.StartsWith("/kcsapi/"));

            kscSessionSource.Subscribe(s => s.SaveResponseBody(s.GetSaveFilePath()));

            kscSessionSource
                .Subscribe(s => Log.Add(DateTimeOffset.Now.ToString("HH:mm:ss : ") + s.Request.PathAndQuery));
        }
    }

    internal static class InnerExtensions
    {
        public static string GetSaveFilePath(this Session session)
        {
            return System.Environment.CurrentDirectory
                   + "/ResponseData"
                   + session.Request.PathAndQuery.Split('?').First();
        }

        private static readonly object lockObj = new object();

        public static void SaveResponseBody(this Session session, string filePath)
        {
            lock (lockObj)
            {
                var dir = Directory.GetParent(filePath);
                if (File.Exists(dir.FullName))
                    File.Delete(dir.FullName);
                if (!dir.Exists)
                    dir.Create();
                File.WriteAllBytes(filePath, session.Response.Body);
            }
        }
    }
}
