using Screenshotter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenshotter.Uploaders.Forms
{
    public partial class frmImgurAuthorize : Form
    {
        public frmImgurAuthorize()
        {
            InitializeComponent();
        }

        private string ClientID, ClientSecret;
        public string PIN { get; private set; } = null;

        private void frmImgurAuthorize_Load(object sender, EventArgs e)
        {
            var secretsFile = Resources.imgurSecrets.Split('\n');
            ClientID = secretsFile[0].Trim();
            ClientSecret = secretsFile[1].Trim();

            webBrowser1.Navigate("https://api.imgur.com/oauth2/authorize?client_id=" + ClientID + "&response_type=pin&state=");
            webBrowser1.Navigated += WebBrowser1_Navigated;
        }

        private async void WebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientID) || string.IsNullOrWhiteSpace(ClientSecret))
                throw new ArgumentException("You have to provide your own client id and secret when compiling from source!");

            var regex = Regex.Match(e.Url.Query, "(?<=pin=).*");
            if (e.Url.Host == "api.imgur.com" && regex.Success)
            {
               // e.Cancel = true;

                PIN = regex.Value;

                var helper = new Helpers.Imgur();
                helper.ClientID = ClientID;
                helper.ClientSecret = ClientSecret;

                var tokens = await helper.GetTokens(PIN);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        
        /*object EvalJS(string js)
        {
            HtmlDocument doc = webBrowser1.Document;
            HtmlElement head = doc.GetElementsByTagName("head")[0];
            HtmlElement s = doc.CreateElement("script");

            s.Id = "csScript";
            s.SetAttribute("text", "function func() { var ret = " +
                js + "; document.getElementById('csScript').outerHTML=''; return ret; }");

            head.AppendChild(s);

            return webBrowser1.Document.InvokeScript("func");
        }*/
    }
}
