﻿using Common;
using SitePlugin;
using SitePluginCommon;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace PeriscopeSitePlugin
{
    public class PeriscopeSiteContext : SiteContextBase
    {
        public override Guid Guid => new Guid("FB468FFA-D0E5-4423-968C-5B9E1D258730");

        public override string DisplayName => "Periscope";
        protected override SiteType SiteType => SiteType.Periscope;
        public override IOptionsTabPage TabPanel
        {
            get
            {
                var panel = new PeriscopeOptionsPanel();
                panel.SetViewModel(new PeriscopeSiteOptionsViewModel(_siteOptions));
                return new PeriscopeOptionsTabPage(DisplayName, panel);
            }
        }

        public override ICommentProvider CreateCommentProvider()
        {
            return new PeriscopeCommentProvider(_server, _logger, _options, _siteOptions, _userStoreManager)
            {
                SiteContextGuid = Guid,
            };
        }
        private PeriscopeSiteOptions _siteOptions;
        public override void LoadOptions(string path, IIo io)
        {
            _siteOptions = new PeriscopeSiteOptions();
            try
            {
                var s = io.ReadFile(path);

                _siteOptions.Deserialize(s);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.LogException(ex, "", $"path={path}");
            }
        }

        public override void SaveOptions(string path, IIo io)
        {
            try
            {
                var s = _siteOptions.Serialize();
                io.WriteFile(path, s);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.LogException(ex, "", $"path={path}");
            }
        }
        public override bool IsValidInput(string input)
        {
            var (_, liveId) = Tools.ExtractChannelNameAndLiveId(input);
            return !string.IsNullOrEmpty(liveId);
        }

        public override UserControl GetCommentPostPanel(ICommentProvider commentProvider)
        {
            return null;
        }
        private readonly ICommentOptions _options;
        private readonly IDataServer _server;
        private readonly ILogger _logger;
        public PeriscopeSiteContext(ICommentOptions options, IDataServer server, ILogger logger, IUserStoreManager userStoreManager)
            : base(options, userStoreManager, logger)
        {
            _options = options;
            _server = server;
            _logger = logger;
        }
    }
}
