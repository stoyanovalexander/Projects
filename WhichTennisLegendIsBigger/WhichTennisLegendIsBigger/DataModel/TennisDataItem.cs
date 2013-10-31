using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.Networking;
using Windows.Networking.Connectivity;

namespace WhichTennisLegendIsBigger.Data
{
     /// <summary>
    /// Generic item data model.
    /// </summary>
    public class TennisDataItem : TennisDataCommon
    {
        public TennisDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content, TennisDataGroup group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._content = content;
            this._group = group;
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private TennisDataGroup _group;
        public TennisDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }
}
