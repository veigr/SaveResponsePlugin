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
    [Export(typeof(IPlugin))]
    [Export(typeof(ITool))]
    [ExportMetadata("Guid", "CA904027-2B49-4AFB-B7A3-F185C9D09549")]
    [ExportMetadata("Title", "SaveResponsePlugin")]
    [ExportMetadata("Description", "Response データを保存します。")]
    [ExportMetadata("Version", "1.1.0")]
    [ExportMetadata("Author", "@veigr")]
    public class SaveResponsePlugin : IPlugin, ITool
    {
        private readonly ToolView v = new ToolView
        {
            DataContext = new ToolViewModel
            {
                Writer = new ResponseFileWriter(KanColleClient.Current.Proxy)
            }
        };

        public void Initialize() {}

        public string Name => "SaveResponse";

        public object View => this.v;
    }
}
