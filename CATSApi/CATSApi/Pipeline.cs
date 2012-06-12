// 
// Pipeline.cs
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
    public class Pipeline
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }
        
        private int rating = -1;
        public int Rating
        {
            get { return this.rating; }
        }
        
        private int candidateId = -1;
        public int CandidateId
        {
            get { return this.candidateId; }
        }
        
        private string name = String.Empty;
        public string Name
        {
            get { return this.name; }
        }
        
        private string lastActivity = String.Empty;
        public string LastActivity
        {
            get { return this.lastActivity; }
        }
        
        private string status = String.Empty;
        public string Status
        {
            get { return this.status; }
        }
        
        private string title = String.Empty;
        public string Title
        {
            get { return this.title; }
        }
        
        public Pipeline(XmlNode xml)
        {
            Int32.TryParse(xml.SelectSingleNode("item_id").InnerText, out this.id);
            Int32.TryParse(xml.SelectSingleNode("rating").InnerText, out this.rating);
            Int32.TryParse(xml.SelectSingleNode("candidate_id").InnerText, out this.candidateId);
            this.name = xml.SelectSingleNode("name").InnerText;
            this.lastActivity = xml.SelectSingleNode("last_activity").InnerText;
            this.status = xml.SelectSingleNode("status").InnerText;
            this.title = xml.SelectSingleNode("title").InnerText;
        }
    }
}

