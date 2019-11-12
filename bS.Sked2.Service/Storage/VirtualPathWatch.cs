using System;
using bS.Sked2.Structure.Base.FileSystem;
using Zio;

namespace bS.Sked2.Service.Storage
{
    public class VirtualPathWatch : IVirtualPathWatch
    {
        EventHandler<string> RenameEventHandler;
        EventHandler<string> ChangeEventHandler;
        EventHandler<string> CreatedEventHandler;
        EventHandler<string> DeletedEventHandler;
        private readonly IFileSystemWatcher source;

        public VirtualPathWatch(IFileSystemWatcher source)
        {
            this.source = source;
        }

        public bool EnableRaisingEvents
        {
            set => source.EnableRaisingEvents = value;
        }
        public bool IncludeSubdirectories
        {
            set => source.IncludeSubdirectories = value;
        }

        public event EventHandler<string> Renamed
        {
            add
            {
                RenameEventHandler += value;
                if (source != null) source.Renamed += (object sender, FileRenamedEventArgs e) =>
                {
                    value.Invoke(sender, e.FullPath.ToAbsolute().ToString());
                };

            }
            remove
            {
                RenameEventHandler -= value;
                if (source != null) source.Renamed -= (object sender, FileRenamedEventArgs e) =>
                {

                };
            }
        }
        public event EventHandler<string> Changed
        {
            add
            {
                ChangeEventHandler += value;
                if (source != null) source.Changed += (object sender, FileChangedEventArgs e) =>
                {
                    value.Invoke(sender, e.FullPath.ToAbsolute().ToString());
                };

            }
            remove
            {
                ChangeEventHandler -= value;
                if (source != null) source.Changed -= (object sender, FileChangedEventArgs e) =>
                {

                };
            }
        }
        public event EventHandler<string> Created
        {
            add
            {
                CreatedEventHandler += value;
                if (source != null) source.Created += (object sender, FileChangedEventArgs e) =>
                {
                    value.Invoke(sender, e.FullPath.ToAbsolute().ToString());
                };

            }
            remove
            {
                CreatedEventHandler -= value;
                if (source != null) source.Created -= (object sender, FileChangedEventArgs e) =>
                {

                };
            }
        }
        public event EventHandler<string> Deleted
        {
            add
            {
                DeletedEventHandler += value;
                if (source != null) source.Deleted += (object sender, FileChangedEventArgs e) =>
                {
                    value.Invoke(sender, e.FullPath.ToAbsolute().ToString());
                };

            }
            remove
            {
                DeletedEventHandler -= value;
                if (source != null) source.Deleted -= (object sender, FileChangedEventArgs e) =>
                {

                };
            }
        }
    }
}
