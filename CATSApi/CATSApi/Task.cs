// 
// Task.cs
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
    public class Task
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }
        
        private string description = String.Empty;
        public string Description
        {
            get { return this.description; }
        }
        
        private string priority = String.Empty;
        public string Priority
        {
            get { return this.priority; }
        }
        
        private string regarding = String.Empty;
        public string Regarding
        {
            get { return this.regarding; }
        }
        
        private string assignedBy = String.Empty;
        public string AssignedBy
        {
            get { return this.assignedBy; }
        }
        
        private string assignedTo = String.Empty;
        public string AssignedTo
        {
            get { return this.assignedTo; }
        }
        
        private DateTime due = DateTime.MinValue;
        public DateTime Due
        {
            get { return this.due; }
        }
        
        public Task(XmlNode xml)
        {
            Int32.TryParse(xml.SelectSingleNode("item_id").InnerText, out this.id);
            this.description = xml.SelectSingleNode("description").InnerText;
            this.priority = xml.SelectSingleNode("priority").InnerText;
            this.regarding = xml.SelectSingleNode("regarding").InnerText;
            this.assignedBy = xml.SelectSingleNode("assigned_by").InnerText;
            this.assignedTo = xml.SelectSingleNode("assigned_to").InnerText;
            DateTime.TryParse(xml.SelectSingleNode("due").InnerText, out this.due);         
        }
    }
}

