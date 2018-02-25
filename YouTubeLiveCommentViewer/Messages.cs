﻿using GalaSoft.MvvmLight.Messaging;
using SitePlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YouTubeLiveCommentViewer
{
    internal class MainViewCloseMessage : MessageBase { }

    class ShowOptionsViewMessage : MessageBase
    {
        public IEnumerable<IOptionsTabPage> Tabs { get; }
        public ShowOptionsViewMessage(IEnumerable<IOptionsTabPage> tabs)
        {
            Tabs = tabs;
        }
    }
    //class ShowUserViewMessage : MessageBase
    //{
    //    public UserViewModel Uvm { get; }
    //    public ShowUserViewMessage(UserViewModel uvm)
    //    {
    //        Uvm = uvm;
    //    }
    //}
    class SetPostCommentPanel : MessageBase
    {
        public UserControl Panel { get; }
        public SetPostCommentPanel(UserControl panel)
        {
            Panel = panel;
        }
    }
}