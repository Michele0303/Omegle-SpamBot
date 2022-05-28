using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Text.Json;

namespace Omegle_SpamBot.Omegle
{
    internal class Configuration
    {
        /// <summary> message to spam </summary>
        private string Message { get; set; }
        /// <summary> if true, the browser is headless </summary>
        private bool IsHeadless { get; set; }
        /// <summary> seconds of delay </summary>
        private int Delay { get; set; }


        /// <summary> reads data from bot.json </summary>
        protected void ReadConfiguration()
        {
            string json = File.ReadAllText("bot.json");
            using JsonDocument doc = JsonDocument.Parse(json, new JsonDocumentOptions() { AllowTrailingCommas = true });
            JsonElement root = doc.RootElement;

            Message = root.GetProperty("message").ToString();
            IsHeadless = root.GetProperty("headless").GetBoolean();
            Delay = root.GetProperty("delay").GetInt32();
        }

        /// <returns> returns the chrome options </returns>
        protected ChromeOptions GetChromeOptions()
        {
            var chromeOptions = new ChromeOptions { PageLoadStrategy = PageLoadStrategy.Normal };
            if (IsHeadless) 
                chromeOptions.AddArgument("--headless"); //hide

            return chromeOptions;
        }

        /// <returns> returns the javascript command with all configured parameters </returns>
        protected string GetJSCommand()
        {
            return $@"
function executeOmegle()
{{
  let btn = document.querySelector('.disconnectbtn')
  let messageBox = document.querySelector('.chatmsg')
  let sendBtn = document.querySelector('.sendbtn')
  btn.click()
  messageBox.innerHTML=`{Message}`;
  sendBtn.click()
}}
 
setInterval(executeOmegle,{Delay * 1000})";
        }
    }
}
