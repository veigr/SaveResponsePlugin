using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.EventListeners.WeakEvents;
using Grabacr07.KanColleWrapper;

namespace SaveResponsePlugin
{
    internal class ToolViewModel : ViewModel
    {

        #region Writer変更通知プロパティ
        private ResponseFileWriter _Writer;

        public ResponseFileWriter Writer
        {
            get
            { return this._Writer; }
            set
            { 
                if (this._Writer == value)
                    return;
                this._Writer = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion
    }
}
