// 
// JoborderApplicationQuestion.cs
//  
// Author:
//       Graham Floyd <gfloyd@catsone.com>
// 
// Copyright (c) 2012 CATS Software, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Xml;

namespace CATS
{
    public class JoborderApplicationQuestion
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }
        
        private bool? required = null;
        public bool Required
        {
            get { return (bool)this.required; }
        }
        
        private string width = String.Empty;
        public string Width
        {
            get { return this.width; }
        }

        private int position = -1;
        public int Position
        {
            get { return this.position; }
        }
        
        private string type = String.Empty;
        public string Type
        {
            get { return this.type; }
        }
        
        private string key = String.Empty;
        public string Key
        {
            get { return this.key; }
        }
        
        private string title = String.Empty;
        public string Title
        {
            get { return this.title; }
        }
        
        private string comment = String.Empty;
        public string Comment
        {
            get { return this.comment; }
        }
        
        private Dictionary<int, string> answers = new Dictionary<int, string>();
        public Dictionary<int, string> Answers
        {
            get { return this.answers; }
        }
        
        public JoborderApplicationQuestion(XmlNode xml)
        {
            Int32.TryParse(xml.Attributes["id"].Value, out this.id);
            this.required = CATSApi.StringToBool(xml.Attributes["required"].Value);
            this.width = xml.Attributes["width"].Value;
            Int32.TryParse(xml.Attributes["position"].Value, out this.position);
            this.type = xml.Attributes["type"].Value;
            this.key = xml.Attributes["key"].Value;
            this.title = xml.SelectSingleNode("title").InnerText;
            this.comment = xml.SelectSingleNode("comment").InnerText;
            
            XmlNodeList answerNodes = xml.SelectNodes("answers/answer");
            foreach (XmlNode answerNode in answerNodes)
            {
                this.answers.Add(Convert.ToInt32(answerNode.Attributes["id"].Value), answerNode.InnerText);
            }                       
        }
    }
}

