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
            { return _Writer; }
            set
            { 
                if (_Writer == value)
                    return;
                _Writer = value;

                if (_Writer != null)
                {
                    this.CompositeDisposable.Add(new CollectionChangedWeakEventListener(
                        _Writer.Log,
                        (sender, e) => RaisePropertyChanged(() => this.LogText)));
                }
                RaisePropertyChanged();
            }
        }
        #endregion

        public string LogText
        {
            get { return string.Join(Environment.NewLine, Writer.Log) + Environment.NewLine; }
        }
    }
}
