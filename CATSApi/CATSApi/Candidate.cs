// 
// Candidate.cs
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
    public class Candidate
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }
        
        private string fullName = String.Empty;
        public string FullName
        {
            get { return this.fullName; }
        }
        
        private string location = String.Empty;
        public string Location
        {
            get { return this.location; }
        }
        
        private DateTime updated = DateTime.MinValue;
        public DateTime Updated
        {
            get { return this.updated; }
        }
        
        private int rating = -1;
        public int Rating
        {
            get { return this.rating; }
        }
        
        private string recentStatusExt = String.Empty;
        public string RecentStatusExt
        {
            get { return this.recentStatusExt; }
        }
        
        public Candidate(XmlNode xml)
        {
            Int32.TryParse(xml.SelectSingleNode("item_id").InnerText, out this.id);         
            this.fullName = xml.SelectSingleNode("full_name").InnerText;
            this.location = xml.SelectSingleNode("location").InnerText;
            DateTime.TryParse(xml.SelectSingleNode("updated").InnerText, out this.updated);
            Int32.TryParse(xml.SelectSingleNode("rating").InnerText, out this.rating);          
            this.recentStatusExt = xml.SelectSingleNode("recent_status_ext").InnerText;
        }
    }
}

