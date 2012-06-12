// 
// JoborderApplication.cs
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
    public class JoborderApplication
    {
        private string name = String.Empty;
        public string Name
        {
            get { return this.name; }
        }
        
        private DateTime updated = DateTime.MinValue;
        public DateTime Updated
        {
            get { return this.updated; }
        }
        
        private string header = String.Empty;
        public string Header
        {
            get { return this.header; }
        }
        
        private List<JoborderApplicationQuestion> questions = new List<JoborderApplicationQuestion>();
        public List<JoborderApplicationQuestion> Questions
        {
            get { return this.questions; }
        }
        
        public JoborderApplication(XmlNode xml)
        {
            this.name = xml.Attributes["name"].Value;
            DateTime.TryParse(xml.Attributes["updated"].Value, out this.updated);
            this.header = xml.SelectSingleNode("header").InnerText;
            
            XmlNodeList questionNodes = xml.SelectNodes("questions/question");
            foreach (XmlNode questionNode in questionNodes)
            {
                JoborderApplicationQuestion question = new JoborderApplicationQuestion(questionNode);
                this.questions.Add(question);
            }
        }
    }
}

