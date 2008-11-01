//
// Flash Selenium - .NET Client
// 
// Date: 3 April 2008
// Paulo Caroli, Sachin Sudheendra
// http://code.google.com/p/flash-selenium
// -----------------------------------------
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
using System;
using Selenium;

namespace FlashSelenium
{
    public class FlashSelenium
    {
        private readonly string flashObjectId;
        private readonly ISelenium selenium;

        public FlashSelenium(ISelenium selenium, string flashObjectId)
        {
            this.selenium = selenium;
            this.flashObjectId = flashObjectId;
        }

        public void Start()
        {
            selenium.Start();
        }

        public void Stop()
        {
            selenium.Stop();
        }

        public void Open(string url)
        {
            selenium.Open(url);
        }

        public string Call(string functionName, params string[] parameters)
        {
            return selenium.GetEval(jsForFunction(functionName, parameters));
        }

        public void WaitForPageToLoad(string timeout)
        {
            selenium.WaitForPageToLoad(timeout);
        }


        //Custom Methods
        protected string jsForFunction(string functionName, params string[] parameters)
        {
            string browserPrefix = checkBrowserAndReturnJSPrefix();
            string functionArgs = "";
            if (parameters.Length == 0)
            {
                return browserPrefix + functionName + "();";
            }
            foreach (string str in parameters)
            {
                functionArgs = functionArgs + "'" + str + "',";
            }
            functionArgs = functionArgs.Substring(0, functionArgs.Length - 1);
            return browserPrefix + functionName + "(" + functionArgs + ");";
        }

        protected string checkBrowserAndReturnJSPrefix()
        {
            string appName = selenium.GetEval("navigator.userAgent");
            if (appName.Contains(BrowserConstants.FIREFOX3) || appName.Contains(BrowserConstants.IE))
            {
                return createJSPrefix_window_document();
            }
            return createJSPrefix_document();
        }

        private string createJSPrefix_document()
        {
            return "document['" + flashObjectId + "'].";
        }

        private string createJSPrefix_window_document()
        {
            return "window.document['" + flashObjectId + "'].";
        }

        // Standard Methods
        public string PercentLoaded()
        {
            return Call("PercentLoaded");
        }

        public string IsPlaying()
        {
            return Call("IsPlaying");
        }

        public string GetVariable(string key)
        {
            return Call("GetVariable", key);
        }

        public void SetVariable(string key, string value)
        {
            Call("SetVariable", key, value);
        }

        public void GotoFrame(int frameNumber)
        {
            Call("GotoFrame", frameNumber.ToString());
        }

        public void LoadMovie(int layerNumber, string url)
        {
            Call("LoadMovie", layerNumber.ToString(), url);
        }

        public void Pan(int x, int y, int mode)
        {
            Call("Pan", x.ToString(), y.ToString(), mode.ToString());
        }

        public void Play()
        {
            Call("Play");
        }

        public void Rewind()
        {
            Call("Rewind");
        }

        public void StopPlay()
        {
            Call("StopPlay");
        }

        public int TotalFrames()
        {
            return Convert.ToInt32(Call("TotalFrames"));
        }

        public void Zoom(int percent)
        {
            Call("Zoom", percent.ToString());
        }

        //Tell Target Methods
        public int TCurrentFrame(string target)
        {
            return Convert.ToInt32(Call("TCurrentFrame", target));
        }

        public void TCallFrame(string target, int frameNumber)
        {
            Call("TCallFrame", target, frameNumber.ToString());
        }

        public void TCallLabel(string target, string label)
        {
            Call("TCallLabel", target, label);
        }

        public string TCurrentLabel(string target)
        {
            return Call("TCurrentLabel", target);
        }

        public string TGetProperty(string target, int property)
        {
            return Call("TGetProperty", target, property.ToString());
        }

        public int TGetPropertyAsNumber(string target, int property)
        {
            return Convert.ToInt32(Call("TGetPropertyAsNumber", target, property.ToString()));
        }

        public void TGotoFrame(string target, int frameNumber)
        {
            Call("TGotoFrame", target, frameNumber.ToString());
        }

        public void TGotoLabel(string target, string label)
        {
            Call("TGotoLabel", target, label);
        }

        public void TPlay(string target)
        {
            Call("TPlay", target);
        }

        public void TSetProperty(string target, string property, string value)
        {
            Call("TSetProperty", target, property, value);
        }

        public void TStopPlay(string target)
        {
            Call("TStopPlay", target);
        }

        //Standard Events

        public void OnProgress(int percent)
        {
            Call("OnProgress", percent.ToString());
        }

        public void OnReadyStateChange(int state)
        {
            Call("OnReadyStateChange", state.ToString());
        }

        public void FSCommand(string command, string args)
        {
            Call("FSCommand", command, args);
        }
    }
}