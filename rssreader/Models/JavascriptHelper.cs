using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rssreader.Models
{
    public class JavascriptHelper
    {
        public List<JavascriptHelperSection> Sections = new List<JavascriptHelperSection>();

        public enum Actions
        {
            Success,
            Warning
        };
        
        public JavascriptHelper AddSuccess(string message)
        {
            return AddAction(Actions.Success, nameof(message), message);
        }

        public JavascriptHelper AddWarning(string message)
        {
            return AddAction(Actions.Warning, nameof(message), message);
        }

        public JavascriptHelper AddAction(Actions action, string param = null, string value = null)
        {
            var parameters = new Dictionary<string, string>();
            if (param != null && value != null)
            {
                parameters.Add(param, value);
            }
            Sections.Add(new JavascriptHelperSection() { Action = action, Params = parameters });
            return this;
        }


    }

    public class JavascriptHelperSection
    {
        public JavascriptHelper.Actions Action { get; set; }

        private Dictionary<string, string> paramsDictionary;

        public Dictionary<string, string> Params
        {
            get
            {
                if (paramsDictionary == null)
                {
                    paramsDictionary = new Dictionary<string, string>();
                }
                return paramsDictionary;
            }
            set { paramsDictionary = value; }
        }
    }
}