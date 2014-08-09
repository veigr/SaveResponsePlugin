using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleViewer.Composition;
using Grabacr07.KanColleWrapper;

namespace SaveResponsePlugin
{
    [Export(typeof(IToolPlugin))]
    [ExportMetadata("Title", "SaveResponsePlugin")]
    [ExportMetadata("Description", "Response データを保存します。")]
    [ExportMetadata("Version", "1.0.1")]
    [ExportMetadata("Author", "@veigr")]
    public class SaveResponsePlugin : IToolPlugin
    {
        private readonly ToolViewModel _vm = new ToolViewModel
        {
            Writer = new ResponseFileWriter(KanColleClient.Current.Proxy)
        };

        public object GetToolView()
        {
            /// ログとか正直要らないんだけど、GetToolViewでnull返しても空っぽのタブが出来て不自然なので…
            return new ToolView { DataContext = _vm };
        }

        public string ToolName
        {
            // タブ名になる。名前長すぎるとタブ幅広がってヤバい。
            get { return "SaveResponse"; }
        }

        public object GetSettingsView()
        {
            // Viewerの方がまだ実装されてない
            return null;
        }
    }
}
