using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Grabacr07.KanColleWrapper;
using Fiddler;
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
            { return _Log; }
            set
            { 
                if (_Log == value)
                    return;
                _Log = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ResponseFileWriter(KanColleProxy proxy)
        {
            this.Log = new DispatcherCollection<string>(DispatcherHelper.UIDispatcher);

            //kcsとkcsapi以下を保存。キャッシュにある奴は保存されない。
            var kscSessionSource = proxy.SessionSource
                .Where(s => s.PathAndQuery.StartsWith("/kcs/")
                        || s.PathAndQuery.StartsWith("/kcsapi/"));

            kscSessionSource
                .Where(s => s.NeedDecode())
                .Where(s => s.utilDecodeResponse(true)) //chunkedはDecodeが必要っぽい
                .Subscribe(s => s.SaveResponseBody(s.GetSaveFilePath()));

            kscSessionSource
                .Where(s => !s.NeedDecode())
                .Subscribe(s => s.SaveResponseBody(s.GetSaveFilePath()));

            kscSessionSource
                .Subscribe(s => Log.Add(DateTimeOffset.Now.ToString("HH:mm:ss : ") + s.PathAndQuery));
        }
    }

    internal static class InnerExtensions
    {
        public static string GetSaveFilePath(this Session session)
        {
            //TODO: SettingsViewができたら外部設定出来るようにしたい
            return System.Environment.CurrentDirectory
            + "/ResponseData"
            + session.PathAndQuery.Split('?').First();
        }

        public static bool NeedDecode(this Session session)
        {
            return session.oResponse.headers
                    .Where(h => h.Name == "Transfer-Encoding")
                    .Any(h => h.Value == "chunked");
        }
    }
}
