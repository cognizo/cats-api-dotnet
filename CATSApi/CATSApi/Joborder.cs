// 
// Joborder.cs
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
using System.Xml;

namespace CATS
{
    public class Joborder
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }
        
        private string title = String.Empty;
        public string Title
        {
            get { return this.title; }
        }
        
        private string status = String.Empty;
        public string Status
        {
            get { return this.status; }
        }
        
        private DateTime updated = DateTime.MinValue;
        public DateTime Updated
        {
            get { return this.updated; }
        }
        
        private int ageModify = -1;
        public int AgeModify
        {
            get { return this.ageModify; }
        }
        
        private int submitted = -1;
        public int Submitted
        {
            get { return this.submitted; }
        }
        
        private int pipeline = -1;
        public int Pipeline
        {
            get { return this.pipeline; }
        }
        
        private string pipelineReport = String.Empty;
        public string PipelineReport
        {
            get { return this.pipelineReport; }
        }
        
        public Joborder(XmlNode xml)
        {
            Int32.TryParse(xml.SelectSingleNode("item_id").InnerText, out this.id);
            this.title = xml.SelectSingleNode("title").InnerText;
            this.status = xml.SelectSingleNode("status").InnerText;
            DateTime.TryParse(xml.SelectSingleNode("updated").InnerText, out this.updated);
            Int32.TryParse(xml.SelectSingleNode("age_modify").InnerText, out this.ageModify);
            Int32.TryParse(xml.SelectSingleNode("submitted").InnerText, out this.submitted);
            Int32.TryParse(xml.SelectSingleNode("pipeline").InnerText, out this.pipeline);
            this.pipelineReport = xml.SelectSingleNode("pipeline_report").InnerText;
        }
    }
}

