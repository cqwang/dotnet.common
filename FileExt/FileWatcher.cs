
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.FileExt
{
    public class FileWatcher
    {
        private FileSystemWatcher _watcher;
        private Action<string> _action;

        public FileWatcher(string path, string filter, Action<string> action)
        {
            this._watcher = new FileSystemWatcher(path, filter);
            this._watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size;
            this._watcher.IncludeSubdirectories = true;

            this._watcher.Created += new FileSystemEventHandler(this.OnCreated);
            this._watcher.Deleted += new FileSystemEventHandler(OnDeleted);
            this._watcher.Changed += new FileSystemEventHandler(OnChanged);
            this._watcher.Renamed += new RenamedEventHandler(OnRenamed);

            this._action = action;
        }

        /// <summary>
        /// 是否启用监听
        /// </summary>
        public bool EnableRaisingEvents
        {
            get
            {
                return this._watcher.EnableRaisingEvents;
            }
            set
            {
                this._watcher.EnableRaisingEvents = value;
            }
        }


        public void OnCreated(object source, FileSystemEventArgs args)
        {
            string message = string.Format("File {0} Created", args.FullPath);
            this._action(message);
        }

        public void OnDeleted(object source, FileSystemEventArgs args)
        {
            string message = string.Format("File {0} Deleted", args.FullPath);
            this._action(message);
        }

        public void OnChanged(object source, FileSystemEventArgs args)
        {
            string message = string.Format("File {0} {1}", args.FullPath, args.ChangeType.ToString());
            this._action(message);
        }

        public void OnRenamed(object source, RenamedEventArgs args)
        {
            string message = string.Format("File {0} is renamed to {1}", args.OldFullPath, args.FullPath);
            this._action(message);
        }

    }
}
