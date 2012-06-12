// 
// Application.cs
//  
// Author:
//       gfloyd Graham Floyd <gfloyd@catsone.com>
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
    public class Application
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }
        
        private DateTime created = DateTime.MinValue;
        public DateTime Created
        {
            get { return this.created; }
        }
        
        private int joborderId = -1;
        public int JoborderId
        {
            get { return this.joborderId; }
        }
        
        private string title = String.Empty;
        public string Title
        {
            get { return this.title; }
        }
        
        private Dictionary<string, string> answers = new Dictionary<string, string>();
        public Dictionary<string, string> Answers
        {
            get { return this.answers; }
        }
        
        public Application(XmlNode xml)
        {
            Int32.TryParse(xml.Attributes["application_id"].Value, out this.id);
            DateTime.TryParse(xml.Attributes["created"].Value, out this.created);
            Int32.TryParse(xml.Attributes["joborder_id"].Value, out this.joborderId);
            this.title = xml.Attributes["title"].Value;
            
            XmlNodeList answers = xml.SelectNodes("answer");
            foreach (XmlNode answer in answers)
            {
                this.answers.Add(answer.Attributes["question"].Value, answer.InnerText);
            }
        }
    }
}

