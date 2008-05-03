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
using Selenium;

namespace Selenium
{
    public class FlashSelenium
    {
        private readonly ISelenium selenium;
        private readonly string flashObjectId;

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
            else
            {
                foreach (string str in parameters)
                {
                    functionArgs = functionArgs + "'" + str + "',";
                }
                functionArgs = functionArgs.Substring(0, functionArgs.Length - 1);
                return browserPrefix + functionName + "(" + functionArgs + ");";
            }
        }

        protected string checkBrowserAndReturnJSPrefix()
        {
            string indexOfMicrosoft = selenium.GetEval("navigator.appName.indexOf(\"Microsoft Internet\")");
            if (!indexOfMicrosoft.Equals("-1"))
            {
                return createJSPrefix_window_document();
            }
            else
            {
                return createJSPrefix_document();
            }
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

        public void TotalFrames()
        {
            Call("TotalFrames");
        }

        public void Zoom(int percent)
        {
            Call("Zoom", percent.ToString());
        }
    }
}