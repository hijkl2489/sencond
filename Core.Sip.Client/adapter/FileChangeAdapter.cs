/*
  8.1有人想同时监控多种类型的文件，例如*.xml + *.config，发现Filter属性不支持这种设置（只能够设置一种）。这种情况下可以将Filter属性设成*.*，在事件里用if (e.FullPath.EndsWith(".xml") || e.FullPath.EndsWith(".config"))自己判断过滤一下。记得Filter属性的设置并不会减少进入缓冲区的事件通知，因此上 面的方法并不会带来多少性能损失。
   8.2. BufferSize
    Windows操作系统使用FileSystemWatcher创建的一个内存缓冲区通知程序文件的修改信息，如果在很短的时间内有非常多的文件修改，这 个缓冲区会溢出， 造成部分追踪丢失，并且FileSystemWatcher不会产生异常。加大InternalBufferSize属性值可以避免这 种情况。
    InternalBufferSize默认值是8K，可以设置的最小值是4K，增加或减小InternalBufferSize最好用4K的整数倍。每一 个事件通知需要使用16字节，并不包含文件名。InternalBufferSize的内存来自non-paged内存，注意这部分内存资源比较宝贵。
    使用NotifyFilter、IncludeSubdirectories属性减小trace范围，设置filter属性并不会影响进入缓冲区的事件通知，另外尽快的完成事件处理，也是避免缓冲区溢出造成事件丢失的一个措施。
    8.3. 隐藏文件也会监控
    8.4 有些系统中，FileSystemWatcher的事件里对长文件名使用8.3短文件名方式表示。
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Sip.Client.Adapter
{
    class FileChangeAdapter
    {
        // 文件监控类
        protected  FileSystemWatcher _fsw;
        public void init()
        {
            _fsw = new FileSystemWatcher();

            #region  属性设置
            //FileSystemWatcher：侦听文件系统更改通知，并在目录或目录中的文件发生更改时引发事件。
            //获取或设置要监视的目录的路径。
            _fsw.Path = "";
            //获取或设置要监视的更改类型。
            _fsw.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
             //获取或设置筛选字符串，用于确定在目录中监视哪些文件。
            //此处只能监控某一种文件，不能监控多种文件，但可以监控所有文件
            _fsw.Filter = "*.txt";
            //获取或设置一个值，该值指示是否监视指定路径中的子目录。
            _fsw.IncludeSubdirectories = true;
            #endregion

            #region  触发的事件

            //文件或目录创建时事件
            _fsw.Created += new FileSystemEventHandler(OnFileCreated);
            //文件或目录变更时事件
            _fsw.Changed += new FileSystemEventHandler(OnFileChanged);
            //文件或目录重命名时事件
            _fsw.Renamed += new RenamedEventHandler(OnFileRenamed);
            //文件或目录刪除时事件
            _fsw.Deleted += new FileSystemEventHandler(OnFileDeleted);
            #endregion

            //获取或设置一个值，该值指示是否启用此组件。
            _fsw.EnableRaisingEvents = true;
        }

        #region 触发事件的方法
        public void OnFileCreated(object sender, FileSystemEventArgs e)
        {

        }
        public void OnFileChanged(object sender, FileSystemEventArgs e)
        {

        }
        public void OnFileRenamed(object sender, FileSystemEventArgs e)
        {

        }
        public void OnFileDeleted(object sender, FileSystemEventArgs e)
        {

        }
      
        #endregion

    }
}
