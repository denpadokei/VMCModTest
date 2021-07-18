using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VMCModTest.Settings
{
    public class WebServiceController
    {
        public WebServiceController()
        {
            
        }
        private HttpListener _server;
        private static readonly string RootUri = "http://127.0.0.1:35552/VMCModTest/";
        private static readonly string IndexPath = "VMCModTest.Settings.Web.index.html";
        private static readonly string JsPath = "VMCModTest.Settings.Web.js.setting.js";
        private SemaphoreSlim _requestSemapho = new SemaphoreSlim(1, 1);
        private string _pageData;
        private string _jsData;

        public void Start()
        {
            if (string.IsNullOrEmpty(this._pageData)) {
                using (var indexSr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(IndexPath)))
                using (var jsSr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(JsPath))) {
                    this._pageData = indexSr.ReadToEnd();
                    this._jsData = jsSr.ReadToEnd();
                }

            }

            if (this._server != null) {
                return;
            }

            this._server = new HttpListener();
            this._server.Prefixes.Add(RootUri);
            this._server.Start();
            Task.Run(() =>
            {
                this._server.BeginGetContext(this.OnContext, null);
            });
        }

        public void Stop()
        {
            if (_server == null) {
                return;
            }
            this._server.Stop();
        }

        public void Launch()
        {
            Process.Start(RootUri);
        }

        private async void OnContext(IAsyncResult res)
        {
            var context = this._server.EndGetContext(res);
            this._server.BeginGetContext(this.OnContext, null);
            var req = context.Request;
            var resp = context.Response;
            await this._requestSemapho.WaitAsync();
            Debug.Log(req.RawUrl);
            try {
                byte[] data;
                if (req.RawUrl == "js/setting.js") {
                    data = Encoding.UTF8.GetBytes(this._jsData);
                    resp.ContentType = "text/javascript";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;
                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
                }
                else {
                    data = Encoding.UTF8.GetBytes(this._pageData);
                    resp.ContentType = "text/html";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;
                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
                }
            }
            catch (Exception e) {
                Debug.LogError(e);
            }
            finally {
                this._requestSemapho.Release();
            }
        }
    }
}
